using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	private static PlayerStatus instance;
	public static PlayerStatus getInstance{
		get{
			if(instance == null){
				instance = FindObjectOfType<PlayerStatus>() as PlayerStatus;
			}
			return instance;
		}
	}
	public int level;
	public int itemSeq=0;
	public int[] Karma = new int[5];
	public List<ItemClass> myItem = new List<ItemClass>();
	
	
	// Use this for initialization
	void Start () {
		
	}	
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AddItem(ItemClass item){
		if(myItem.Count < 50){
			item.seq = itemSeq++;
			myItem.Insert(0,item);
			print("added item"+item.name+item.rarity);
		}else{
			Debug.Log("too many item");
		}
	}
	//0.획득 순 1.attack 2.rank 3.level 4.rarity 5.strengthen 6.name
	public void SortItem(int n){
		switch(n){
			case 0: myItem.Sort(SeqCompare);break;
			case 1: myItem.Sort(AttackCompare);break;
			case 2: myItem.Sort(RankCompare);break;
			case 3: myItem.Sort(LevelCompare);break;
			case 4: myItem.Sort(RarityCompare);break;
			case 5: myItem.Sort(StrengthenCompare);break;
			case 6: myItem.Sort(NameCompare);break;
		}
	}
	public int SeqCompare(ItemClass x, ItemClass y)
	{
		return x.seq.CompareTo(y.seq)*-1;
	}
	public int AttackCompare(ItemClass x, ItemClass y)
	{
		return x.attackPoint.CompareTo(y.attackPoint) * -1;
	}
	public int RankCompare(ItemClass x, ItemClass y)
	{
		return x.rank.CompareTo(y.rank) * -1;
	}
	public int LevelCompare(ItemClass x, ItemClass y)
	{
		return x.level.CompareTo(y.level) * -1;
	}
	public int RarityCompare(ItemClass x, ItemClass y)
	{
		return x.rarity.CompareTo(y.rarity) * -1;
	}
	public int NameCompare(ItemClass x, ItemClass y)
	{
		return x.name.CompareTo(y.name);
	}
	public int StrengthenCompare(ItemClass x, ItemClass y)
	{
		return x.strengthen.CompareTo(y.strengthen) * -1;
	}
	
	
}
