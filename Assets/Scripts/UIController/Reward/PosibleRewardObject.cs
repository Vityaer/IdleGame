using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
[System.Serializable]
public class PosibleRewardObject{
	[SerializeField]protected float posibility = 100f;
	public virtual BaseObject GetShowSubject(){
		return new BaseObject();
	}
	public float Posibility{get => posibility;}
}




[System.Serializable]
public class PosibleRewardResource: PosibleRewardObject{
	[OdinSerialize] public TypeResource subject = TypeResource.Gold;
    public override BaseObject GetShowSubject(){ return (BaseObject) GetResource; }
	private Resource res = null;
	public Resource GetResource{get{if(res == null) res = new Resource(subject); return res; }}
}
[System.Serializable]
public class PosibleRewardItem: PosibleRewardObject{
	public ItemName ID = ItemName.Stick;
    public override BaseObject GetShowSubject(){ return (BaseObject)GetItem; }
	private Item item = null;
	public Item GetItem{ get{ if(item == null) item =  new Item(Convert.ToInt32(ID), 1);  return item; } }
}
[System.Serializable]
public class PosibleRewardSplinter: PosibleRewardObject{
	public SplinterName ID = SplinterName.OneStarPeople;
    public override BaseObject GetShowSubject(){ return (BaseObject) GetSplinter; }
	private Splinter splinter = null;
	public Splinter GetSplinter{get{ if(splinter == null){Debug.Log(ID.ToString());Debug.Log((int)ID); splinter = new Splinter(Convert.ToInt32(ID), 1);} return splinter;}}
	public SplinterName GetID{get => ID;}

}
public enum ItemName{
	Stick = 101,
	Pole = 102,
	RustySword = 103,
	OrdinarySword = 104,
	WizardStuff = 105,
	Axe = 106,
	Mace = 107,
	hammer = 108,
	PupilBoot = 201,
	PeasantBoot = 202,
	MilitiamanBoot = 203

}
public enum SplinterName{
	OneStarPeople = 11,
	OneStarElf = 12
}