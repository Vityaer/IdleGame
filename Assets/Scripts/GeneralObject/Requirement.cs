using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RequirementTasks", menuName = "Custom ScriptableObject/RequirementTasks", order = 59)]
[System.Serializable]
public class ListRequirements : ScriptableObject{
	public List<Requirement> listRequirement = new List<Requirement>();
}
[System.Serializable]
public class Requirement{
	public TypeRequirement type = TypeRequirement.GetLevel;
	public string description;
	public int requireInt;
	public Resource requireRes;
	public ScriptableObject requireObject;
	public Resource reward;
	public void GetReward(){
		PlayerScript.Instance.AddResource(reward);
	}
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
	CountSpecialHaring

}
