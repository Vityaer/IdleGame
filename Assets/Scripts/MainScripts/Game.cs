using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ObjectSave;
[System.Serializable]
public class Game{

	[SerializeField]
	private int campaignMissionNumComplete;
	public int CampaignMissionNumComplete{get => campaignMissionNumComplete; set => campaignMissionNumComplete = value;}

	public ListResource StoreResources;

	[SerializeField]
	private string date;//date record
	public DateTime Date{get => GetDate(date); set => date = value.ToString();}
	[SerializeField]
	private List<MineSave> datePreviousMine = new List<MineSave>();

	public string strDate {get => date;}
	public List<Task> listTasks = new List<Task>(); 
	public Game(){
		Date = DateTime.Now;
	}
	public void CreateGame(Game game){
		StoreResources = game.StoreResources;
		campaignMissionNumComplete = game.CampaignMissionNumComplete; 
		datePreviousMine = game.datePreviousMine;
		date = game.strDate;
		listTasks = game.listTasks;
	}
	private DateTime GetDate(string strDate){
		if(campaignMissionNumComplete == 0) 
	    	if(PlayerScript.Instance != null) PlayerScript.Instance.AddResource(new Resource(TypeResource.SimpleHireCard, 5, 0));
		DateTime convertedDate = new DateTime();
	    	try {
	        	convertedDate = Convert.ToDateTime(strDate);
	    	}
	    	catch (FormatException) {
	    		convertedDate = DateTime.Now; 
	    		Debug.Log("wrong format");
	    	}
	    return convertedDate;  
	}
	public void PrepareForSave(){
		listTasks = TaskGiverScript.tasks;
	}


//API mines
	public string GetPreviousDateTimeForMine(int ID){
		MineSave mineSave = datePreviousMine.Find(x => x.ID == ID);
		return  (mineSave != null) ? mineSave.previousDateTime : DateTime.Now.ToString();
	}
	public void SaveMine(Mine mine){
		MineSave mineSave = datePreviousMine.Find(x => x.ID == mine.ID);
		if(mineSave != null) {
			mineSave.ChangeInfo(mine);
		}else{
			datePreviousMine.Add(new MineSave(mine));
		}	
	}
	public Mine LoadMine(int ID){
		Debug.Log("load mine...");
		Mine result = new Mine(ID);  
		MineSave mineSave = datePreviousMine.Find(x => x.ID == ID);
		if(mineSave != null){
			Debug.Log(mineSave.Level.ToString());
			result.SetData(mineSave.Level, FunctionHelp.StringToDateTime(mineSave.previousDateTime));
		}else{
			result.SetData(0, DateTime.Now);
		}
		return result;	
	}	
}
