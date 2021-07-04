using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Mine{
	public int ID;
	public ListResource listRes;
	public int level;
	[SerializeField] private float speed;
	public DateTime previousDateTime;
	public string strDate;
	private ListResource reward = new ListResource();
	public Resource FirstResource{get => listRes.List[0];}
	public void LevelUP(){
		level += 1;
		speed *= 1.01f;
		PlayerScript.Instance.SaveMine(this);
	}
	public void CalculateReward(){
    	previousDateTime = PlayerScript.Instance.GetPreviousDateTimeForMine(ID);
    	strDate = previousDateTime.ToString();
		DateTime localDate = DateTime.Now;
		if(previousDateTime != null){
			TimeSpan interval = localDate - previousDateTime;
			int tact = (int) (interval.TotalSeconds)/20;
			tact = Math.Min(tact, 200);
			Debug.Log(tact);
			reward = listRes * tact * (1 + speed/100);
		}
		previousDateTime = localDate;
	}
	public void GetResources(){
		if(level > 0){
			CalculateReward();
			PlayerScript.Instance.AddResource(reward);
			PlayerScript.Instance.SaveMine(this);
			reward.Clear();
		}
	}
	public Mine(int ID){
		this.ID = ID;
	}
	public void SetData(int level, DateTime previousDateTime){
		this.level = level;
		this.previousDateTime = previousDateTime;
	}
}