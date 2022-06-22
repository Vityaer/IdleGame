using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
[System.Serializable]
public class PosibleRewardObject{
	[SerializeField]protected float posibility = 100f;
	public virtual BaseObject GetShowSubject(){
		return new BaseObject();
	}
}




[System.Serializable]
public class PosibleRewardObject<T>: PosibleRewardObject where T: BaseObject{
	[OdinSerialize] public T subject;
    public override BaseObject GetShowSubject(){
		BaseObject result  = new BaseObject();
		switch(subject){
			case Resource reward:
				result = new Resource(reward.Name);
				break;
			case Item reward:
				result = new Item(reward.ID);
				break;
			case Splinter reward:
				result = new Splinter(reward.ID);
				break;		
		}
		return result;
	}
}
