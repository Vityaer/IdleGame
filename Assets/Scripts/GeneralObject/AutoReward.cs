using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[System.Serializable]
public class AutoReward{
	[OdinSerialize] public ListResource resources = new ListResource(new Resource(TypeResource.Gold), new Resource(TypeResource.ContinuumStone), new Resource(TypeResource.Exp));  
	[OdinSerialize] public List<PosibleRewardObject<Resource>> listPosibleResources = new List<PosibleRewardObject<Resource>>();
	[OdinSerialize] public List<PosibleRewardObject<Item>> listPosibleItems = new List<PosibleRewardObject<Item>>();
	[OdinSerialize] public List<PosibleRewardObject<Splinter>> listPosibleSplinters = new List<PosibleRewardObject<Splinter>>();

	public Reward GetCaculateReward(int countTact){
		Reward result = new Reward();
		for(int i = 0; i < listPosibleResources.Count; i++) result.GetListResource.List.Add(listPosibleResources[i].subject);
        for(int i = 0; i < listPosibleItems.Count; i++) result.GetItems.Add(listPosibleItems[i].subject);
        for(int i = 0; i < listPosibleSplinters.Count; i++) result.GetSplinters.Add(listPosibleSplinters[i].subject);
		return result;
	}
	public Reward GetShowReward(){
		Reward result = new Reward();
		for(int i = 0; i < listPosibleResources.Count; i++) result.GetListResource.List.Add(listPosibleResources[i].subject);
        for(int i = 0; i < listPosibleItems.Count; i++) result.GetItems.Add(listPosibleItems[i].subject);
        for(int i = 0; i < listPosibleSplinters.Count; i++) result.GetSplinters.Add(listPosibleSplinters[i].subject);
		return result;
	}
}
