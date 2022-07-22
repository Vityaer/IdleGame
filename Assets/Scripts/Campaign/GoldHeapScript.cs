using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using HelpFuction;
using DG.Tweening;

public class GoldHeapScript : MonoBehaviour{
	[SerializeField] private AutoReward autoReward;
	[SerializeField] private Reward calculatedReward;
	public DateTime previousDateTime;
	[SerializeField] private RectTransform imageGoldRectTransform;
	[SerializeField] private Image imageHeap;
	private Vector2 startSize = Vector2.one;
	void Start(){ Timer = TimerScript.Timer; }

	public void SetNewReward(AutoReward newAutoReward){ 
		if(newAutoReward != null)
			this.autoReward = newAutoReward;
	} 
	public void OnClickHeap(){
		previousDateTime = CampaignScript.Instance.GetAutoFightPreviousDate;
		CalculateReward();
		OffGoldHeap();
 		MessageControllerScript.Instance.OpenWin(calculatedReward);
		previousDateTime = DateTime.Now;
	}
	private void CalculateReward(){ calculatedReward = autoReward.GetCaculateReward(FunctionHelp.CalculateCountTact(previousDateTime)); }
	public void OnOpenSheet(){ CheckSprite(); }
	public void OnCloseSheet(){ Timer.StopTimer(timerChangeSprite); }
	private void CheckSprite(){
    	int tact = FunctionHelp.CalculateCountTact(previousDateTime);
    	imageHeap.sprite = listSpriteGoldHeap.GetSprite(tact);
		imageGoldRectTransform.DOSizeDelta(startSize, 0.25f, true);
    	timerChangeSprite = Timer.StartTimer(5f, CheckSprite);
	}
	void OffGoldHeap(){ imageGoldRectTransform.DOSizeDelta(Vector2.zero, 0.25f, true).OnComplete(OffSprite); }
	void OffSprite(){ imageHeap.enabled = false; }
	GameTimer timerChangeSprite;
    TimerScript Timer;
    [SerializeField] private ListSpriteDependFromCount listSpriteGoldHeap = new ListSpriteDependFromCount();
}
