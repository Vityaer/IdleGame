using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Requirement{
	[Header("Info")]
	[SerializeField] protected int ID;
	public TypeRequirement type = TypeRequirement.GetLevel;
	public string description;
	[SerializeField]protected List<RequirementStage> stages = new List<RequirementStage>();
	public ProgressType progressType;

	protected BigDigit progress = new BigDigit(0, 0);
	protected int currentStage = 0;

	public int CountStage{get => stages.Count;}
	public BigDigit Progress{get => progress;}
	public int CurrentStage{get => currentStage;}
	bool isComplete = false;
	public void AddProgress(BigDigit amount){
		if(isComplete == false){
			switch(progressType){
				case ProgressType.StorageAmount:
					progress.Add(amount);
					break;
				case ProgressType.MaxAmount:
					if(progress > amount)
						progress = amount;
					break;
				case ProgressType.CurrentAmount:
					progress = amount;
					break;	
			}
			if(currentStage == (CountStage - 1)){
				if(CheckCount()){
					progress = new BigDigit(GetRequireCount().Count, GetRequireCount().E10);	
					isComplete = true;
				}
			}	
		}
	}
	public bool CheckCount(){return progress.CheckCount(GetRequireCount()); }
	public BigDigit GetRequireCount(){ return stages[currentStage].RequireCount; }
	public void GetReward(){PlayerScript.Instance.AddReward(stages[currentStage].reward); if(currentStage < CountStage) currentStage++; }
	public Reward GetRewardInfo(){return stages[currentStage].reward.Clone();}
}
public enum TypeRequirement{
	GetLevel,
	DoneChapter,
	DoneMission,
	GetHeroes,
	GetHeroesWithLevel,
	GetHeroesWithRating,
	GetHeroesCount,
	SimpleSpin,
	SpecialSpin,
	SynthesItem,
	SynthesCount,
	BuyItem,
	SpendResource,
	BuyItemCount,
	DestroyHeroCount,
	CountWin,
	CountDefeat,
	CountPointsOnSimpleArena,
	CountPointsOnTournament,
	RaceHireCount,
	SimpleHireCount,
	SpecialHireCount
}
public enum TypeReward{
	Resource,
	Item,
	Hero,
	Splinter
}
public enum ProgressType{
	StorageAmount,
	MaxAmount,
	CurrentAmount
}
[System.Serializable]
public class RequirementStage{
	[Header("Requirement")]
	[SerializeField] private BigDigit requireCount;
	[Header("Reward")]
	public Reward reward;
	public BigDigit RequireCount{ get =>  requireCount;}
}


///EveryDayTask
[System.Serializable]
public class EveryDayTask : Requirement{
	public void ClearProgress(){
		base.currentStage = 0;
		base.progress = new BigDigit(0, 0);
	}
}