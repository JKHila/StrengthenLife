using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class ItemList : MonoBehaviour
{
    public static List<ItemClass> itemList = new List<ItemClass>();
	
	private static ItemList instance;
	public static ItemList getInstance{
		get{
			if(instance == null){
				instance = FindObjectOfType<ItemList>() as ItemList;
			}
			return instance;
		}
	}
	
    public Sprite[] sowrdSprites;
    public GameObject[] itemFrames;
    //public GameObject contents;
	
    void Awake()
    {
        InitItemFrame();   
        sowrdSprites = Resources.LoadAll<Sprite>("item/sword");
        //DebugItemList();
    }
    public void loadItemList()
    {
        ItemClass item;
        int numOfItem = PlayerPrefs.GetInt("NumOfItem");
        for (int i = 0; i < numOfItem; i++)
        {
            item = JsonUtility.FromJson<ItemClass>(PlayerPrefs.GetString("ItemList" + i));
            itemList.Add(item);
        }
    }
    public void InitItemFrame()
    {
        int initPosX = -272;
        int initposY = 1700;
        int i = 0;
        for (int j = 0; j < PlayerStatus.getInstance.myItem.Count; j++)
        {
			itemFrames[j].SetActive(true);
            itemFrames[j].transform.localScale = new Vector3(1, 1, 1);
            itemFrames[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(initPosX + 179 * (j%4), initposY - 282 * i);
            if (j % 4 == 3) { 
				i++;
			}
        }
    }
	public void SetItemInfo(){
		int i = 0;
		foreach(ItemClass key in PlayerStatus.getInstance.myItem){
			//print(key.code+" "+key.level+" "+key.rank+" "+key.rarity+" "+key.name);
			itemFrames[i].transform.FindChild("itemImg").GetComponent<Image>().sprite = GetSprite(key.code);
            //itemFrames[i].transform.FindChild("itemImg").GetComponent<Image>().sprite = Resources.Load<Sprite>("faceSet2_0");
			itemFrames[i].transform.FindChild("itemInfo").FindChild("levelImg").FindChild("Text").GetComponent<Text>().text = key.level.ToString();
			itemFrames[i].transform.FindChild("itemInfo").FindChild("rankImg").FindChild("Text").GetComponent<Text>().text = key.rank.ToString();
			itemFrames[i].transform.FindChild("nameText").GetComponent<Text>().text = "<color="+GetColor(key.rarity)+">+"+key.strengthen+" "+key.name+"</color>";
			i++;
		}
	}
    public Sprite GetSprite(string code){
        string kind = code.Substring(0,1);
        string num = code.Substring(1,3);
        //print(num);
        Sprite sp=null;
        if(kind=="1"){
            sp = sowrdSprites[System.Convert.ToInt32(num)];
        }
        return sp;
    }
	public string GetColor(int rare){
		string str="";
		switch(rare){
			case 0: str="white";break;
			case 1: str="#00cc44";break;
			case 2: str="#3399ff";break;
			case 3: str="#e6ac00";break;
		}
		return str;
	}
    public string GetString(string str){
        int length = str.Length;
        if((length-1)/3>0){
            for(int i = 0;i<(length-1)/3;i++){
                str=str.Insert(length-(3*(i+1)),",");
            }
        }
        return str;
    }
	public static void DebugItemList()
    {
        foreach (ItemClass key in itemList)
        {
            Debug.Log(key.rank +" "+ key.name +" "+ key.attackPoint +" "+ key.code);
        }
    }
}
