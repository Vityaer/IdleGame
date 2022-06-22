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
		this.visual = visual;
	}
	public void SetItem(VisualAPI visual){
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
		switch(rewardObject){
			case PosibleRewardObject<Resource> reward:
				UIItem.UpdateUI(reward.subject.Image, Rare.R);
				break;
			case PosibleRewardObject<Item> reward:
				UIItem.UpdateUI(reward.subject.Image, reward.subject.GetRare);
				break;
			case PosibleRewardObject<Splinter> reward:
				UIItem.UpdateUI(reward.subject.Image, reward.subject.GetRare);
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

