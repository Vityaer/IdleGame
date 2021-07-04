using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product{
	public TypeProduct typeProduct;
	public ScriptableObject rewardSplinter;
	public int ID;
	public Resource rewardResource;
	private Item item;
	public Item GetItem{get => item;}
	private static ItemsList itemsList;
	public Sprite GetImage(){
		Sprite result = null;
		switch(typeProduct){
			case TypeProduct.Resource:
				result = rewardResource.sprite;
				break;
			case TypeProduct.Item:
				if(itemsList == null )itemsList = Resources.Load<ItemsList>("Items/ListItems"); 
				item   = itemsList.GetItem(ID);		
				result = item.sprite;	
				break;
			case TypeProduct.Hero:
				result = (rewardSplinter as InfoHero).generalInfo.ImageHero;	
				break;
			case TypeProduct.Splinter:
			break;			
		}
		return result;
	}
	public string GetCount(){
		string result = "";
		switch(typeProduct){
			case TypeProduct.Resource:
				result = rewardResource.ToString();
				break;
			case TypeProduct.Item:	
				result = "";
				break;
			case TypeProduct.Splinter:
				result = "1";
				break;
			case TypeProduct.Hero:
				result = "";
				break;		
		}
		return result;
	}
}

public enum TypeProduct{
	Splinter,
	Resource,
	Item,
	Hero
}