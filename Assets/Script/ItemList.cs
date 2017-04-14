using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemList : MonoBehaviour{
	public static List<ItemClass> itemList = new List<ItemClass>();

	public GameObject[] itemFrames;
	//public GameObject contents;

	void Start () {
		initItemFrame();
	}
	public void initItemFrame(){
		int initPosX = -286;
		int initposY = 645;
		for(int i = 0;i<10;i++){
			for(int j = 0;j<5;j++){
				//GameObject tp = Instantiate(itemFrame) as GameObject;
				//itemFrames[j+(i*5)].transform.SetParent(contents.transform);
				itemFrames[j+(i*5)].transform.localScale = new Vector3(1,1,1);
				itemFrames[j+(i*5)].GetComponent<RectTransform>().anchoredPosition = new Vector2(initPosX+142*j,initposY-142*i);
			}
		}
	}
	public static void DebugItemList(){
		foreach(ItemClass key in itemList){
			Debug.Log(key.rank+key.name+key.attakPoint);
		}
	}
}
