using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : ICloneable{
	[SerializeField]
	protected string name;
	public string Name{get => name; set => name = value;}

	public TypeLocation location;
	[Header("Enemy")]
	[SerializeField]
	private List<MissionEnemy> _listEnemy = new List<MissionEnemy>();
	public List<MissionEnemy> listEnemy{get => _listEnemy;}

	[Header("Win reward")]
	[SerializeField]
	protected Reward winReward;
	public Reward WinReward{get => winReward;}
	public bool isRrouletteWinReward;

	[Header("Auto fight reward")]
	[SerializeField]
	private Reward autoFightReward;
	public Reward AutoFightReward{get => autoFightReward;}

	[Header("Defeat reward")]
	[SerializeField]
	protected Reward defeatReward;
	public Reward DefeatReward{get => defeatReward;}
	public bool isRrouletteDefeatReward;

	public bool saveResult = false;

	public object Clone(){
        return new Mission  { 	Name = this.Name,
        							 	_listEnemy = this.listEnemy,
        							 	winReward     = (Reward) this.WinReward.Clone(),
        							 	autoFightReward  = (Reward) this.autoFightReward.Clone(),
        							 	defeatReward     = (Reward) this.defeatReward.Clone(),
        								saveResult      = this.saveResult,
        								isRrouletteDefeatReward = this.isRrouletteDefeatReward,
        								isRrouletteWinReward = this.isRrouletteWinReward,
        								location = this.location
        							};				
    }
}