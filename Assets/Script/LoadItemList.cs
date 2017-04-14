using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LoadItemList : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {//인터넷 연결 확인
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://strengthenlife-3852d.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            ReadVersion();
        }

        //ReadList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ReadVersion()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Version").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("failed");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Local Ver:" + PlayerPrefs.GetFloat("DataVersion"));
                float serverVer = System.Convert.ToSingle(snapshot.Value);
                Debug.Log("Server Ver:" + serverVer);

                if (PlayerPrefs.GetFloat("DataVersion").Equals(serverVer))
                {
                    print("No Update DataBase");
                    for (int i = 0; i < PlayerPrefs.GetInt("NumOfItem"); i++)
                    {
                        ItemList.itemList.Add(JsonUtility.FromJson<ItemClass>(PlayerPrefs.GetString("ItemList" + i)));
                    }
					//ItemList.DebugItemList();
                }
                else
                {
                    print("Update DataBase");
                    PlayerPrefs.SetFloat("DataVersion", (float)serverVer);
                    ReadItemList();
                    ReadPersonNameList();
                }
            }
        });
    }
    ItemClass item;
    void ReadPersonNameList()
    {
        FirebaseDatabase.DefaultInstance.GetReference("PersonName").GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               Debug.Log("failed");
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               PlayerPrefs.SetInt("NumOfName", System.Convert.ToInt32(snapshot.ChildrenCount));
               for (int i = 0; i < snapshot.ChildrenCount; i++)
               {
                   PlayerPrefs.SetString("PersonName" + i, snapshot.Child(i.ToString()).Child("NAME").Value.ToString());
               }
           }
           /*for (int i = 0; i < PlayerPrefs.GetInt("NumOfName"); i++)
           {
               Debug.Log(PlayerPrefs.GetString("PersonName" + i));
           }*/
       });
    }
    void ReadItemList()
    {
        FirebaseDatabase.DefaultInstance.GetReference("ItemList").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("failed");
            }
            else if (task.IsCompleted)
            {
                print("Read Database");
                DataSnapshot snapshot = task.Result;
                PlayerPrefs.SetInt("NumOfItem", System.Convert.ToInt32(snapshot.ChildrenCount));
                for (int i = 0; i < snapshot.ChildrenCount; i++)
                {
                    item = new ItemClass();
                    item.rank = System.Convert.ToInt32(snapshot.Child(i.ToString()).Child("RANK").Value);
                    item.attakPoint = System.Convert.ToInt32(snapshot.Child(i.ToString()).Child("ATTAK").Value);
                    item.name = snapshot.Child(i.ToString()).Child("NAME").Value.ToString();
                    ItemList.itemList.Add(item);

                    string jsonText = JsonUtility.ToJson(item);
                    PlayerPrefs.SetString("ItemList" + i, jsonText);
                    //Debug.Log(snapshot.Child(i.ToString()).Child("ATTAK").Value);
                }
                /*foreach (ItemClass key in ItemList.itemList)
                {
                    Debug.Log(key.name + key.attakPoint + key.rank);
                }*/
            }
        });
    }
}
