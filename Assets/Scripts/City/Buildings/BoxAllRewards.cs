using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAllRewards : Building{
    public RewardUIControllerScript rewardController;
    public void ShowAll(Reward reward){
    	rewardController.ShowAllReward(reward);
    	Open();
    }
    public override void Close(){
        ClosePage();
        if(building != null){ CanvasBuildingsUI.Instance.CloseBuilding(building);  }
    }
    private static BoxAllRewards instance;
    public static BoxAllRewards Instance{ get => instance;}
    void Awake(){
    	instance = this;
    }
}
