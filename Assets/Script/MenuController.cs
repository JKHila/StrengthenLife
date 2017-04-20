using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	
	// UI
	public GameObject debuggingPanel;
	public InputField input;
	public GameObject itemPanel;
	public GameObject mainItemFrame;
	public Button[] arrow;

	//controller
	public ItemList itemListController;

	public ItemClass selectedItem;
	public int selectedItemNum;
	private int sortNum = 0;
	void Start(){
		ItemList.getInstance.loadItemList();
		MakeItem("10001");
		MakeItem("10002");
		MakeItem("10003");
		PlayerStatus.getInstance.SortItem(sortNum);
		SetMainPanel(0);
	}
	// Update is called once per frame
	void Update () {
		
	}
	//itemPanel;
	//0.seq 1.attack 2.rank 3.level 4.rarity 5.strengthen 6.name
	string[] sortName ={"획득 순", "능력치 순","랭크 순","레벨 순","희귀도 순","강화도 순","이름 순"};
	public void OnSortBtnDown(){
		if(sortNum == 6){
			sortNum = 0;
		}else{
			sortNum++;
		}
		itemPanel.transform.FindChild("menuPanel").FindChild("arrangeBtn").FindChild("Text").GetComponent<Text>().text = sortName[sortNum];
		PlayerStatus.getInstance.SortItem(sortNum);
		itemListController.SetItemInfo();
	}
	
	//mainPanel
	public void SetMainPanel(int n){
		selectedItemNum = n;
		selectedItem = PlayerStatus.getInstance.myItem[n];
		PlayerStatus.getInstance.SortItem(sortNum);
		mainItemFrame.transform.FindChild("itemImg").GetComponent<Image>().sprite = ItemList.getInstance.GetSprite(selectedItem.code);
		mainItemFrame.transform.FindChild("itemName").GetComponent<Text>().text = "<color="+ItemList.getInstance.GetColor(selectedItem.rarity)+">+"+selectedItem.strengthen+" "+selectedItem.name+"</color>";
		mainItemFrame.transform.FindChild("itemExplain").GetComponent<Text>().text = selectedItem.explain;
		mainItemFrame.transform.FindChild("itemRank").FindChild("Text").GetComponent<Text>().text = selectedItem.rank.ToString();
		mainItemFrame.transform.FindChild("itemlevel").FindChild("Text").GetComponent<Text>().text = selectedItem.level.ToString();
		mainItemFrame.transform.FindChild("statusInfoText").GetComponent<Text>().text = ItemList.getInstance.GetString(selectedItem.attackPoint.ToString())+" ATK";//selectedItem.attakPoint.ToString();
		SetArrowActive();
	}
	public void OnArrowBtnDown(int n){
		selectedItemNum += n;
		SetMainPanel(selectedItemNum);
	}
	public void SetArrowActive(){
		if(selectedItemNum == 0){
			arrow[0].interactable = false;
			arrow[1].interactable = true;
		}else if(selectedItemNum == PlayerStatus.getInstance.myItem.Count-1){
			arrow[0].interactable = true;
			arrow[1].interactable = false;
		}else{
			arrow[0].interactable = true;
			arrow[1].interactable = true;
		}
	}
	public void OnStrengthenBtnDown(){
		selectedItem.StrengthenItem();
		SetMainPanel(selectedItemNum);
	}
	public void OnCloseBtnDown(){
		if(itemPanel.activeSelf){
			itemPanel.SetActive(false);
		}
	}
	//bottomPanel
	public void OnItemPanelBtnDown(){
		PlayerStatus.getInstance.SortItem(sortNum);
		itemListController.SetItemInfo();
		itemListController.InitItemFrame();
		itemPanel.transform.FindChild("menuPanel").FindChild("numOfItemText").FindChild("Text").GetComponent<Text>().text = PlayerStatus.getInstance.myItem.Count+" / 50";
		itemPanel.SetActive(true);
	}
	//debugging
	public void OnDebuggingBtnDown(){
		debuggingPanel.SetActive(true);
	}
	
	public void ParseDebuggingText(){
		string[] str = input.text.Split(" "[0]);
		
		switch(str[0]){
			case "getitem": MakeItem(str[1]); break;
			case "loadDB": print("Update DataBase");
                    LoadItemList.getInstance.ReadDB();
                    break;
			case "givemoney": break;
			case "giveruby": break;
			case "delete": PlayerPrefs.DeleteAll(); break;
			case "showitemlist": ItemList.DebugItemList();break;
			case "showmyitemlist": foreach(ItemClass key in PlayerStatus.getInstance.myItem){print(key.code+" "+key.seq);}break;
		}
	}
	public void MakeItem(string str){
		
		ItemClass item = new ItemClass();
		string itemCode = str.Substring(0,4);
		//print(itemCode);
		item = ItemList.itemList.Find(f=>f.code == itemCode).Clone();
		item.rarity = System.Convert.ToInt32(str.Substring(4));
		item.attackPoint = item.baseAttackPoint*(item.rarity+1);

		//print(item.name+" "+item.rarity);
		PlayerStatus.getInstance.AddItem(item);
		SetArrowActive();
	}
	
}
