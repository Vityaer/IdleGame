using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAllRewards : Building{
    public RewardUIControllerScript rewardController;
    public void ShowAll(CalculatedReward reward){
    	rewardController.SetReward(reward.Clone());
    	Open();
    }
    public override void Close(){
        ClosePage();
        if(building != null){
            canvasBuilding.enabled = false;
            building.SetActive(false); 
        }
    }
    private static BoxAllRewards instance;
    public static BoxAllRewards Instance{ get => instance;}
    void Start(){
        Debug.Log(gameObject.name);
    	instance = this;
    }
}
