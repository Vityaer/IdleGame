using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectSave;
using System;
public class CampaignScript : MonoBehaviour{
	private Canvas canvasCampaign;
	private MissionControllerScript infoMission;
	[SerializeField] private List<MissionControllerScript> missionControllers = new List<MissionControllerScript>();
	private CampaignMission mission;
	private int currentMission;
	public Transform missions;
	public CampaignChapter chapter;
	public GameObject background;
	
	private const string NAME_RECORD_NUM_CURRENT_MISSION = "CurrentMission"; 
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
		campaingSaveObject.SetRecordInt(NAME_RECORD_NUM_CURRENT_MISSION, currentMission + 1);
		PlayerScript.Instance.SaveGame();
	}
	public void OpenMission(int num){
		currentMission = num;
		missionControllers[num].OpenMission();
	}
	public void OnResultFight(FightResult result){
		if(result == FightResult.Win){
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
		WarTableControllerScript.Instance.OpenMission(infoMission.mission, this.OnOpenCloseMission);
		FightControllerScript.Instance.RegisterOnFightResult(OnResultFight);
	}
	private void OnOpenCloseMission(bool isOpen){
		if(isOpen == false){ Open(); }else{ Close(); }
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
	void Start(){
		PlayerScript.Instance.RegisterOnLoadGame(OnLoadGame);
	}
	void OnLoadGame(){
		campaingSaveObject = PlayerScript.GetCitySave.mainCampaignBuilding;
		currentMission = campaingSaveObject.GetRecordInt(NAME_RECORD_NUM_CURRENT_MISSION);
		OpenChapter( ChapterControllerScript.Instance.GetCampaignChapter(currentMission) );
		OpenMission(currentMission);
	}
	private BuildingWithFightTeams campaingSaveObject;
   	public DateTime GetAutoFightPreviousDate{get => campaingSaveObject.GetRecordDate("AutoFight");}
}
