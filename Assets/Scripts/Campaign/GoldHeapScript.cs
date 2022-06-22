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
	private Vector2 startSize;
	void Awake(){
    	startSize = imageGoldRectTransform.sizeDelta;
	}
	void Start(){
        Timer = TimerScript.Timer;
	}
	private void CalculateReward(){
		calculatedReward = autoReward.GetCaculateReward(CalculateTact());
		previousDateTime = DateTime.Now;
	}
	public void SetNewReward(AutoReward newAutoReward){
		if(newAutoReward != null){
			this.autoReward = newAutoReward;
		}
	} 
	public void OnClickHeap(){
		CalculateReward();
 		MessageControllerScript.Instance.OpenWin(calculatedReward);
		OffGoldHeap();
		PlayerScript.Instance.player.PlayerGame.Date = previousDateTime;

	}
	public void OnOpenSheet(){
		CheckSprite();
	}
	public void OnCloseSheet(){
        Timer.StopTimer(timerChangeSprite);
	}
	private void CheckSprite(){
    	int tact = CalculateTact();
    	Sprite sprite = listSpriteGoldHeap.GetSprite(tact);
    	if(sprite != null){
    		imageHeap.sprite = sprite;
    		imageGoldRectTransform.DOSizeDelta(startSize, 0.25f, true);
    	}
    	imageHeap.enabled = (sprite != null);
    	timerChangeSprite = Timer.StartTimer(5f, CheckSprite);

	}
	private int CalculateTact(){
		int tact = 0;
		previousDateTime = PlayerScript.Instance.player.PlayerGame.Date;
		DateTime localDate = DateTime.Now;
		if(previousDateTime != null){
			TimeSpan interval = localDate - previousDateTime;
			tact = (int) (interval.TotalSeconds)/5;
		}
		tact = Math.Min(tact, 8640);
		if(tact < 0) tact = 0;
		return tact;
	}
	void OffGoldHeap(){
		imageGoldRectTransform.DOSizeDelta(Vector2.zero, 0.25f, true).OnComplete(OffSprite);
	}
	void OffSprite(){
		imageHeap.enabled = false;
	}
	GameTimer timerChangeSprite;
    TimerScript Timer;
    [SerializeField] private ListSpriteDependFromCount listSpriteGoldHeap = new ListSpriteDependFromCount();
}
