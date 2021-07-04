using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RewardItem : ICloneable{
    public int ID;
    private static ItemsList itemsList;
    private Item _item = null;
	public Item item{get{
			if(itemsList == null) itemsList = Resources.Load<ItemsList>("Items/ListItems");
			if((_item == null) || (_item.ID == 0)) _item = itemsList?.GetItem(ID);
			if((_item == null) || (_item.ID == 0)) Debug.Log(string.Concat("Item with ", ID.ToString(), " not founded"));
			if(_item == null) {_item = itemsList?.GetItem(101); MessageControllerScript.Instance.AddMessage("на этом уровня проблема с предметом, у него ID = 0, разрабочтик дурак");}
			return _item;
		}}
	public TypeIssue typeIssue;
	public float posibility = 100f;
	public int min, max, count;
	private ItemController itemController = null;
	private bool isCalculate = false; 
	public ItemController GetReward(int count = 1){
		int amount = 0;
		switch(typeIssue){
			case TypeIssue.Necessarily:
				amount = this.count;
				break;
			case TypeIssue.Perhaps:
				int loopCount = count / 200;
				for(int i = 0; i < loopCount; i++){
					if( UnityEngine.Random.Range(0f, 100f) < posibility){
						amount += 1;
					} 
				}
				amount = Math.Min(amount, this.count);
				break;
			case TypeIssue.Range:
				float rand = UnityEngine.Random.Range(0f, 100f);
				amount = (int) Mathf.Round(((max - min)*rand/100f + min)/100f);
				break;
			default:
				amount = count;
				break;			
		}
		if(amount > 0){
			itemController = new ItemController(item, amount);		
			isCalculate = true;
		}
		return itemController;
	}
	public object Clone(){
        return new RewardItem  { 	ID            = this.ID,
        							 	_item     = this._item,
        							 	typeIssue     = this.typeIssue,
        							 	posibility    = this.posibility,
        							 	min  = this.min,
        							 	max  = this.max,
        							 	count  = this.count
        							};				
    }
}