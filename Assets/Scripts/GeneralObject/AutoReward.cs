using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[System.Serializable]
public class AutoReward{
	[OdinSerialize] public ListResource resources = new ListResource(new Resource(TypeResource.Gold), new Resource(TypeResource.ContinuumStone), new Resource(TypeResource.Exp));  
	[OdinSerialize] public List<PosibleRewardResource> listPosibleResources = new List<PosibleRewardResource>();
	[OdinSerialize] public List<PosibleRewardItem> listPosibleItems = new List<PosibleRewardItem>();
	[OdinSerialize] public List<PosibleRewardSplinter> listPosibleSplinters = new List<PosibleRewardSplinter>();

	public Reward GetCaculateReward(int countTact){
		Reward reward = new Reward();
		foreach(Resource res in resources.List) reward.GetListResource.List.Add( ((Resource)res.Clone()) * countTact);
		GetPosibleResource(reward, countTact);
		GetPosibleItem(reward, countTact);
		GetPosibleSplinter(reward, countTact);
		return reward;
	}
	int result = 0;
	float countMaxPosible = 0f;
	private void GetPosibleResource(Reward reward, int countTact){
		Debug.Log("resources");
		for(int i = 0; i < listPosibleResources.Count; i++){
			countMaxPosible =  UnityEngine.Random.Range(0.2f, 10f) * countTact * listPosibleResources[i].Posibility / 10000;
			Debug.Log(countMaxPosible);
			result = (int) Mathf.Round(UnityEngine.Random.Range(0f, countMaxPosible));
			Debug.Log(result);
			if(result > 0){
				reward.GetListResource.List.Add(listPosibleResources[i].GetResource * result);
			}
		}			
	}
	private void GetPosibleItem(Reward reward, int countTact){
		Debug.Log("items");
		for(int i = 0; i < listPosibleItems.Count; i++){
			countMaxPosible =  UnityEngine.Random.Range(0.2f, 10f) * countTact * listPosibleItems[i].Posibility / 20000;
			Debug.Log(countMaxPosible);
			result = (int) Mathf.Round(UnityEngine.Random.Range(0f, countMaxPosible));
			Debug.Log(result);
			if(result > 0){
				reward.AddItem(listPosibleItems[i].GetItem * result);
			}
		}			
	}

	private void GetPosibleSplinter(Reward reward, int countTact){
		Debug.Log("splinters");
		for(int i = 0; i < listPosibleSplinters.Count; i++){
			countMaxPosible =  UnityEngine.Random.Range(0.2f, 10f) * countTact * listPosibleSplinters[i].Posibility / 30000;
			Debug.Log(countMaxPosible);
			result = (int) Mathf.Round(UnityEngine.Random.Range(0f, countMaxPosible));
			Debug.Log(result);
			if(result > 0){
				reward.AddSplinter(listPosibleSplinters[i].GetSplinter * result);
			}
		}			
	}




	public Reward GetShowReward(){
		Reward result = new Reward();
		for(int i = 0; i < listPosibleResources.Count; i++) result.GetListResource.List.Add(listPosibleResources[i].GetResource);
        for(int i = 0; i < listPosibleItems.Count;     i++) result.AddItem(listPosibleItems[i].GetItem);
        for(int i = 0; i < listPosibleSplinters.Count; i++){ result.AddSplinter(listPosibleSplinters[i].GetSplinter);} 
		return result;
	}
}
