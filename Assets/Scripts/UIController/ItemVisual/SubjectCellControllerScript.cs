using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectCellControllerScript : MonoBehaviour{

	[Header("Info")]
	public ThingUIScript UIItem;
	private VisualAPI visual;
	private Resource res;
	private ItemController item;
	private SplinterController splinter;
	private void SetVisual(VisualAPI visual){
		CheckCell();
		this.visual = visual;
	}
	public void SetItem(VisualAPI visual){
		CheckCell();
		this.visual = visual;
		visual.SetUI(UIItem);
		visual.UpdateUI();
	}
	public void SetItem(Resource res){
		CheckCell();
		SetVisual(res.GetVisual());
		this.res = res;
 		res.SetUI(UIItem);
		res.UpdateUI();
	}
	public void SetItem(ItemController item){
		CheckCell();
		if(item != null) {
			SetVisual(item.GetVisual());
			this.item = item;
			item.SetUI(UIItem);
			item.UpdateUI();
		}
	}
	public void SetItem(SplinterController splinter){
		CheckCell();
		if(splinter != null){
			SetVisual(splinter.GetVisual());
			this.splinter = splinter;
			splinter.SetUI(UIItem);
			splinter.UpdateUI();
		}
	}
	public void SetItem(Item item){
		if(item != null){
			SetVisual(item.GetVisual());
	 		item.SetUI(UIItem);
			item.UpdateUI();
		}
	}
	public void SetItem(PosibleRewardObject rewardObject){
		CheckCell();
		Debug.Log("posible object");
		switch(rewardObject){
			case PosibleRewardResource reward:
				UIItem.UpdateUI(reward.GetResource.Image, Rare.R, string.Empty);
				break;
			case PosibleRewardItem reward:
				UIItem.UpdateUI(reward.GetItem.Image, reward.GetItem.GetRare);
				break;
			case PosibleRewardSplinter reward:
				UIItem.UpdateUI(reward.GetSplinter.Image, reward.GetSplinter.GetRare);
				break;		
		}
	}
	public void Clear(){
		res?.ClearUI();
		item?.ClearUI();
		splinter?.ClearUI();
		UIItem.Clear();
		visual = null;
	}

	public void ClickOnItem(){visual?.ClickOnItem(); }
	public void OffCell(){
		Clear();
		gameObject.SetActive(false);
	}
	private void CheckCell(){
		if(gameObject.activeSelf == false) gameObject.SetActive(true);
	}
}

