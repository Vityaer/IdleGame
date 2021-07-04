using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionControllerScript : MonoBehaviour{
	[Header("UI")]
	public Text textNameMission;
	public Text textAutoRewardGold;
	public Text textAutoRewardExperience;
	public Text textAutoRewardStone;
	public Image backgoundMission;
	public GameObject infoFotter;
	public GameObject blockPanel;
	public GameObject imageAutoFight;
	public GameObject btnGoFight;
	public Text textBtnGoFight;
	public RewardUIControllerScript rewardController;
 	[Header("Contollers")]
	public StatusMission statusMission;
	public Mission mission;
	public LocationControllerScript locationController;
	private int numMission;

	
//API
	public void SetMission(Mission mission, int numMission){
		this.mission    = (Mission) mission.Clone();
		this.numMission = numMission; 
		if(locationController   == null) locationController = GameObject.Find("LocationFight").GetComponent<LocationControllerScript>();
    	backgoundMission.sprite = locationController.GetBackgroundForMission(this.mission.location);
    	textNameMission.text    = numMission.ToString();
    	if( (int) statusMission < 1 ){
    		blockPanel.SetActive(true);
    		infoFotter.SetActive(false);
    		btnGoFight.SetActive(false);
    	}else{
			UpdateUI();
			UpdateAutoRewardUI();
    	}
	}
	public void ClickOnMission(){
		if(statusMission != StatusMission.Complete){
			if(statusMission == StatusMission.Open){
				CampaignScript.Instance.SelectMission(this, PlayerScript.Instance.GetListHeroes);
			}
		}else{
			if(statusMission != StatusMission.InAutoFight){
				AutoFightScript.Instance.SelectMissionAutoFight(this);
			}
		}		
	}
	public void UpdateAutoRewardUI(){
		if((statusMission == StatusMission.Complete) || (statusMission == StatusMission.InAutoFight)){
			infoFotter.SetActive(true);
			textAutoRewardGold.text       = string.Concat(this.mission.AutoFightReward.ListRewardResource.Find(x => x.GetRes.Name == TypeResource.Gold).GetRes.ToString(), "/ 5sec");
			textAutoRewardExperience.text = string.Concat(this.mission.AutoFightReward.ListRewardResource.Find(x => x.GetRes.Name == TypeResource.Exp).GetRes.ToString(), "/ 5sec");
			textAutoRewardStone.text      = string.Concat(this.mission.AutoFightReward.ListRewardResource.Find(x => x.GetRes.Name == TypeResource.ContinuumStone).GetRes.ToString(), "/ 5sec");
		}

	}
	public void StartAutoFight(){
		statusMission = StatusMission.InAutoFight;
		imageAutoFight.SetActive(true);
		btnGoFight.SetActive(false);
	}
	public void StopAutoFight(){
		statusMission = StatusMission.Complete;
		imageAutoFight.SetActive(false);
		btnGoFight.SetActive(true);
	}
	public void MissionWin(){
		statusMission = StatusMission.InAutoFight;
		AutoFightScript.Instance.SelectMissionAutoFight(this);
		UpdateAutoRewardUI();
		UpdateUI();
		StartAutoFight();
		CampaignScript.Instance.OpenNextMission();
	}
	public void CompletedMission(){
		statusMission = StatusMission.Complete;
		UpdateUI();
		UpdateAutoRewardUI();
	}
	public void OpenMission(){
		statusMission = StatusMission.Open;
		blockPanel.SetActive(false);
		UpdateUI();
	}
    private void UpdateUI(){
    	switch(statusMission){
			case StatusMission.NotOpen:
				rewardController.CloseReward();
				break;
    		case StatusMission.Open:
		    	rewardController.SetReward(this.mission.WinReward.GetCaculateReward());
				rewardController.OpenReward();
				textBtnGoFight.text = "Вызвать";
				break;
			case StatusMission.Complete:
		    	rewardController.SetReward(this.mission.AutoFightReward.GetCaculateReward());
				rewardController.OpenReward();
				textBtnGoFight.text = "Авто";
				break;
			case StatusMission.InAutoFight:
				rewardController.SetReward(this.mission.AutoFightReward.GetCaculateReward());
				rewardController.OpenReward();	
				textBtnGoFight.text = "Авто";
				break;	
    	}
    	int status = (int) statusMission;
		btnGoFight.SetActive( (status == 1) || (status == 2) );
		blockPanel.SetActive(false);
    }
}

public enum StatusMission{
	NotOpen     = 0,
	Open        = 1,
	Complete    = 2,
	InAutoFight = 3 
}