using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignScript : MonoBehaviour{
	private Canvas canvasCampaign;
	private MissionControllerScript infoMission;
	private CampaignMission mission;
	private int currentMission;
	public Transform missions;
	public CampaignChapter chapter;
	public GameObject background;
//API
	public void OpenMissions(int numOpenMission){
		int current = 0;
		foreach(Transform child in missions){
			if(current < chapter.missions.Count)
				child.GetComponent<MissionControllerScript>().SetMission(chapter.missions[current], (chapter.numChapter * 20) + current + 1);
			if(current < numOpenMission){
				child.GetComponent<MissionControllerScript>().CompletedMission();
				if(current == (numOpenMission - 1))
					child.GetComponent<MissionControllerScript>().ClickOnMission();
			}
			current++;
		}
		OpenMission(numOpenMission);
	}
	public void OpenChapter(CampaignChapter chapter){
		this.chapter = chapter;
		OpenMissions(currentMission);
	}
	public void OpenNextMission(){
		OpenMission(currentMission + 1);
		PlayerScript.Instance.SaveGame();
	}
	public void OpenMission(int num){
		currentMission = num;
		missions.GetChild(num).GetComponent<MissionControllerScript>().OpenMission();
		PlayerScript.Instance.player.PlayerGame.CampaignMissionNumComplete = currentMission;
	}
	public void GetResult(bool win){
		if(win == true){
	 		MessageControllerScript.Instance.OpenWin(this.mission?.WinReward);
	 		if(infoMission != null){
	 			infoMission.MissionWin();
	 		}else{
	 			Debug.Log("это была не миссия компании");
	 		}
		}
 		
		if(mission == null){ WarTableControllerScript.Instance.FinishMission(); }
		infoMission = null;
		mission = null;
	}

	public void SelectMission(MissionControllerScript infoMission, List<InfoHero> listHeroes){
		this.infoMission = infoMission;
		this.mission = infoMission.mission;
		WarTableControllerScript.Instance.OpenMission(infoMission.mission, listHeroes);
		WarTableControllerScript.Instance.RegisterOnOpenCloseMission(this.OnOpenCloseMission);
		WarTableControllerScript.Instance.Open();
	}
	public void SelectMission(CampaignMission mission, List<InfoHero> listHeroes){
		WarTableControllerScript.Instance.OpenMission(mission, listHeroes);
		this.mission = mission;
	}
	private void OnOpenCloseMission(bool isOpen){
		if(!isOpen){ Open(); }else{ Close(); }
	}
	public void Open(){
		UnregisterOnOpenCloseMission();
		BackGroundControllerScript.Instance.OpenBackground(background);
		canvasCampaign.enabled = true;
		MenuControllerScript.Instance.CloseMainPage();
	}
	public void Close(){
		canvasCampaign.enabled = false;
		MenuControllerScript.Instance.OpenMainPage();
	}
	public void UnregisterOnOpenCloseMission(){
		WarTableControllerScript.Instance.UnregisterOnOpenCloseMission(this.OnOpenCloseMission);
	}
	private static CampaignScript instance;
	public  static CampaignScript Instance{get => instance;}
	void Awake(){
		instance = this;
		canvasCampaign = GetComponent<Canvas>();
	}
   
}
