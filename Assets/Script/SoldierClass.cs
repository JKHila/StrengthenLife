using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierClass{
	public string name;
	public int level;
	public int attackPoint;
	public int healthPoint;
	public int rarity;
	public Sprite sprite;
	public SoldierClass(string name, int level,int attackPoint,int healthPoint,Sprite sprite,int rarity){
		this.name = name;
		this.level = level;
		this.attackPoint = attackPoint;
		this.healthPoint = healthPoint;
		this.sprite = sprite;
		this.rarity =  rarity;

	}
}
