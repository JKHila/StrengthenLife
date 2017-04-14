using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HireSoldier : MonoBehaviour {
	int baseAttack = 100;
	int baseHealth = 1000;
	int minError = -20;
	int maxError = 20;
	int playerLevel = 10;
	public GameObject soldierHirePanel;
	Sprite[] sprites;
	Sprite[] sprites2;
	SoldierClass[] soldiers = new SoldierClass[4];
	// Use this for initialization
	void Start () {
		//sprites = Resources.LoadAll<Sprite>("faceSet");
		sprites = Resources.LoadAll<Sprite>("faceSet2");
		MakeSoldier();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeSoldier(){
		int rand,error,level=0,spriteNum;
		int numOfName = PlayerPrefs.GetInt("NumOfName");
		string name;
		int attack = 0, defence = 0, fee = 0;
		for(int i = 0;i<4;i++){
			rand = Random.Range(0,numOfName);
			name = PlayerPrefs.GetString("PersonName"+rand);
			
			level = Random.Range(playerLevel-3,playerLevel+1);
			if(level<1){level = 1;}
			error = Random.Range(minError,maxError);
			attack = (baseAttack + error) * level;
			error = Random.Range(minError,maxError);
			defence = (baseHealth + error) * level;

			fee = (attack+defence/10)*10*level;
			spriteNum = Random.Range(0,sprites.Length);

			soldiers[i] = new SoldierClass(name,level,attack,defence,sprites[spriteNum],0);
			SetSoldierFrame(i,name,level,attack,defence,fee,sprites[spriteNum]);
			//Debug.Log(name+","+level+","+attack+","+defence+","+fee);
		}
	}
	void SetSoldierFrame(int num,string name,int level,int attack,int health,int fee,Sprite sprite){
		Transform soldierFrame = soldierHirePanel.transform.FindChild("soldierFrame"+(num+1));
		
		soldierFrame.FindChild("soldierInfo").GetComponent<Text>().text = 
		string.Format("Name:{0}\nLevel:{1}\t\thp:{2}\n\t\t\t\tattack:{3}",name,level,health,attack);
		
		soldierFrame.FindChild("Button").Find("Text").GetComponent<Text>().text=
		string.Format("{0}\n고용",fee);

		
		soldierFrame.FindChild("soldierFace").GetComponent<Image>().sprite = sprite;
	}
}
