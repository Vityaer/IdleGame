using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemController : VisualAPI, ICloneable{
	private Item _item;
	public Item item{get => _item; set => _item = value;} 
	protected int amount;
	public int Amount{get => amount;}
	protected InventoryCellControllerScript cell = null;
	public InventoryCellControllerScript cellInvenroty{get => cell; set => cell = value;}
	public void ClickOnItem(){
		InventoryControllerScript.Instance.OpenInfoItem(this);
	}

	public ItemController(Item item, int amount){
		this.item = item;
		this.amount = amount;
	}
	public ItemController(){}
	protected ThingUIScript UI;
	public void SetUI(ThingUIScript UI){
		this.UI = UI;
		UpdateUI();
	}
	public void UpdateUI(){
		if(item != null)
			UI?.UpdateUI(item.sprite, Rare.C, Amount);
	}
	public VisualAPI GetVisual(){
		return (this as VisualAPI);
	}
	public void ClearUI(){
		this.UI = null;
	}
	public void DecreaseAmount(int count){
		amount -= count;
	}
	public void IncreaseAmount(int count){
		amount += count;
	}
	public object Clone(){
        return new ItemController  { 	_item         = this._item,
        							 	amount     = this.amount
        							};				
    }
}
public interface VisualAPI{
	VisualAPI GetVisual();
	void ClearUI();
	void SetUI(ThingUIScript UI);
	void UpdateUI();
	void ClickOnItem();
}