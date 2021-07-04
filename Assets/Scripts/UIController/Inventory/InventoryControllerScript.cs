using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InventoryControllerScript : MonoBehaviour{

	private Canvas canvasInventory;
	private TypeItem typeItems;
	public Transform grid;
	public CellItemHeroScript cellItem      = null;
	private ItemController selectItem = null;
	[Header("Panel Inventory")]
	public GameObject panelInventory;
	private bool isOpenInventory = false;
	public GameObject panelController;
	[Header("Info panel")]
	public PanelInfoItemScript panelInfoItem;

	public Inventory inventory = new Inventory();
	private List<InventoryCellControllerScript> cells = new List<InventoryCellControllerScript>();
	private void GetCells(){
		foreach(Transform cell in grid){
			cells.Add(cell.GetComponent<InventoryCellControllerScript>());
		}
	}
	private List<VisualAPI> listForVisual = new List<VisualAPI>();
	private void FillGrid(List<VisualAPI> list){
		int i = 0;
		for(i = 0; i < list.Count; i++){
			cells[i].SetItem(list[i]);
		}
		for(i = list.Count; i < cells.Count; i++){
			cells[i].Clear();
		}
	}

	private void FillGrid(List<ItemController> filterItems){
		int i = 0;
		for(i = 0; i < filterItems.Count; i++){
			cells[i].SetItem(filterItems[i]);
		}
		for(i = filterItems.Count; i < cells.Count; i++){
			cells[i].Clear();
		}
	}
//API
	//Invenotory
	public int HowManyThisItems(Item item){
		int result = 0;
		ItemController workItem = inventory.items.Find(x => (x.item.ID == item.ID));
		if(workItem != null) {result = workItem.Amount;}

		return result;
	}
	public bool CheckItems(Item item, int count = 1){
		bool result = false;
		ItemController workItem = inventory.items.Find(x => (x.item.ID == item.ID));
		if(workItem != null)
			if(workItem.Amount >= count) result = true;
		return result;
	}
	public void RemoveItems(Item item, int count = 1){
		ItemController workItem = inventory.items.Find(x => (x?.item.ID == item.ID));
		if(workItem != null){
			workItem.DecreaseAmount(count);
			if(workItem.Amount == 0) inventory.items.Remove(workItem);
		}
	}
	public void AddItem(Item item){
		inventory.Add(new ItemController(item, 1));
	}
	public void AddItem(ItemController itemController){
		inventory.Add(itemController);
	}
	public void AddItems(List<ItemController> Items){
		inventory.Add(Items);
	}
	public void AddSplinter(SplinterController splinterController){
		inventory.Add(splinterController);
	}
	public void AddSplinters(List<SplinterController> splinters){
		inventory.Add(splinters);
	}
	public void SelectItem(){
		if(cellItem != null){
			cellItem.SetItem(selectItem.item);
			selectItem.DecreaseAmount( 1 );
			if(selectItem.Amount <= 0){
				inventory.items.Remove(selectItem);
			}
			cellItem = null;
			selectItem = null;
		}
		CloseAll();
	}
	public void DropItem(){
		panelInfoItem.Close();
		inventory.items.Remove(selectItem);
		inventory.GetAll(listForVisual);
		FillGrid(listForVisual);
	}
	public void DropSplinter(SplinterController splinter){
		panelInfoItem.Close();
		inventory.splinters.Remove(splinter);
		inventory.GetAll(listForVisual);
		FillGrid(listForVisual);
	}
	public void Open(){
		OpenAllItem();
		canvasInventory.enabled = true;
		panelInventory.SetActive(true);
		isOpenInventory = true;
	}
	public void OpenAllItem(){
		inventory.GetAll(listForVisual);
		FillGrid(listForVisual);
		panelController.SetActive(true);
	}
	public void Open(string str){
		TypeItem curType = (TypeItem) Enum.Parse(typeof(TypeItem), str); 
		Open(curType, cellItem: null);
	}
	public void Open(TypeItem typeItems, CellItemHeroScript cellItem = null){
		this.cellItem = cellItem;
		inventory.GetItemAtType(typeItems, listForVisual);
		FillGrid(listForVisual);
		canvasInventory.enabled = true;
		panelController.SetActive( (this.cellItem == null) ? true : false );
		panelInventory.SetActive(true);
		isOpenInventory = true;
	}
	public void OpenInfoItem(ItemController itemController){
		canvasInventory.enabled = true;
		selectItem = itemController;
		panelInfoItem.OpenInfoAboutItem(itemController.item, this.cellItem);
	}
	public void OpenInfoItem(Resource res){
		canvasInventory.enabled = true;
		panelInfoItem.OpenInfoAboutItem(res);
	}
	public void OpenInfoItem(Item item, TypeItem typeItems, CellItemHeroScript cellItem){
		canvasInventory.enabled = true;
		this.cellItem = cellItem;
		panelInfoItem.OpenInfoAboutItem(item, this.cellItem, onHero: true);
	}
	SplinterController selectSplinter;
	public void OpenInfoItem(SplinterController splinterController){
		canvasInventory.enabled = true;
		selectSplinter = splinterController;
		panelInfoItem.OpenInfoAboutItem(splinterController);
	}
	public void Close(){
		panelInfoItem.Close();
		if(isOpenInventory){
			panelInventory.SetActive(false);
			isOpenInventory = false;
		}
		canvasInventory.enabled = false;
		cellItem = null;
	}
	public void CloseAll(){
		panelInfoItem.Close();
		panelInventory.SetActive(false);
		isOpenInventory = false;
		canvasInventory.enabled = false;
	}
	private static InventoryControllerScript instance;
	public static InventoryControllerScript Instance{get => instance;}
	void Awake(){
		canvasInventory = GetComponent<Canvas>();
		instance = this;
		GetCells();
	}
	void Start(){
		LoadInformation();
		if(inventory != null){
			foreach (ItemController itemController in inventory.items) {
				itemController?.item?.SetBonus();
			}
		}
	}

	void OnApplicationPause(bool pauseStatus){
		#if UNITY_ANDROID && !UNITY_EDITOR
		SaveLoadControllerScript.SaveInventory(inventory);
		#endif
	}
	void OnDestroy(){
		SaveLoadControllerScript.SaveInventory(inventory);
	}
	void LoadInformation(){
		inventory = SaveLoadControllerScript.LoadInventory();
	}
}
