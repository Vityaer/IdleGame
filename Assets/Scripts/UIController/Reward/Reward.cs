using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward  : ICloneable{
	[SerializeField]
	private List<RewardResource> listRewardResource = new List<RewardResource>();
	public List<RewardResource> ListRewardResource{get => listRewardResource;}

	[SerializeField]
	private List<RewardItem> listRewardItem = new List<RewardItem>();
	public List<RewardItem> ListRewardItem{get => listRewardItem;}

	[SerializeField]
	private List<RewardSplinter> listRewardSplinter = new List<RewardSplinter>();
	public List<RewardSplinter> ListRewardSplinter{get => listRewardSplinter;}

	//API
	public CalculatedReward GetCaculateReward(int count = 1){
		CalculatedReward result = new CalculatedReward();
		ListResource rewardResources = new ListResource();
		foreach(RewardResource res in listRewardResource)
			result.GetListResource.AddResource(res.GetReward(count));
		foreach(RewardItem rewardItem in listRewardItem)
			result.GetItems.Add(rewardItem.GetReward(count));
		foreach(RewardSplinter rewardSplinter in listRewardSplinter){
			result.GetSplinters.Add(rewardSplinter.GetReward(count));
		}	
		return result;
	}
	public int Count{get => (ListRewardResource.Count + ListRewardItem.Count + ListRewardSplinter.Count);}

	public object Clone(){
		List<RewardResource> _listRewardResource = new List<RewardResource>();
		List<RewardItem> _listRewardItem = new List<RewardItem>();
		List<RewardSplinter> _listRewardSplinter = new List<RewardSplinter>();

		this.listRewardResource.ForEach(x => {_listRewardResource.Add((RewardResource) x.Clone());}); 
		this.listRewardItem.ForEach(x => {_listRewardItem.Add((RewardItem) x.Clone());}); 
		this.listRewardSplinter.ForEach(x => {_listRewardSplinter.Add((RewardSplinter) x.Clone());}); 
        return new Reward  { 	
        							 	listRewardResource     = _listRewardResource,
        							 	listRewardItem         = _listRewardItem,
        							 	listRewardSplinter     = _listRewardSplinter
        							};				
    }
}


public enum TypeIssue{
	Necessarily = 0,
	Perhaps     = 1,
	Range       = 2
}