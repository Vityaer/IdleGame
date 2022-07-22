using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinLosePanelScript : MonoBehaviour{

	public Transform listReward;
	public GameObject panelReward;
	private Reward reward;
	public RewardUIControllerScript rewardController;
	public void OpenPanel(Reward reward, string message){
		int k = 0;
		this.reward = reward;
		rewardController.ShowAllReward(reward);
		panelReward.SetActive(true);
	}

	public void ClosePanel(){
		PlayerScript.Instance.AddReward(reward);
 		panelReward.SetActive(false);
 		WarTableControllerScript.Instance.FinishMission(); 
	}
}
