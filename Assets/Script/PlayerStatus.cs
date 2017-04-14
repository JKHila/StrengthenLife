using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	public int level;
	public int[] Karma = new int[5];
	public ItemClass[] myItem = new ItemClass[100];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AddItem(ItemClass item){
		for(int i = 0;i<100;i++){
			if(myItem[i] == null){
				myItem[i] = item.Clone();
			}
		}
		Debug.Log("too many item");
	}
}
