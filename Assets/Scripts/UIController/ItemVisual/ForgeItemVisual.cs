using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForgeItemVisual : MonoBehaviour{

	[Header("Info")]
	public  TypeMatter matter;
	private ItemSynthesis thing;
	public ItemSynthesis Thing{get => thing;}
	private Item item;
	public ThingUIScript UIItem;
	public ResourceObjectCost resourceCost;
	public ForgeItemObjectCost forgeItemCost;

	public void SetItem(ItemSynthesis item){
		thing = item;
		if(thing.reward != null)
			UIItem.UpdateUI(thing.reward.sprite, Rare.C, item.reward.Rating);
	}
	public void SetItem(Item item){
		this.item = item;
		UIItem.UpdateUI(item.sprite, Rare.C, item.Rating);
	}
	public void SetItem(Item item, int amount){
		SetItem(item);
		forgeItemCost.SetInfo(item, amount);
	}
	public void SetResource(Resource res){
		UIItem.UpdateUI(res.sprite, Rare.C, 1);
		resourceCost.SetInfo(res);
	}

	public void SelectItem(){
		if(thing?.reward != null){
			if(matter == TypeMatter.Synthesis){
				ForgeScript.Instance.SelectItem(this, thing);
				UIItem.Select();
			}
		}
	}

}

public class Backlight{
	public GameObject backlight;
}
public enum TypeMatter{
	Synthesis,
	Info
}