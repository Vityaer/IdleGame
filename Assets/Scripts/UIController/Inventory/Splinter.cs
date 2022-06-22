using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Splinter : PlayerObject{
	public TypeSplinter typeSplinter;
	public RaceSpliter race;
	[SerializeField] private int requireAmount;
	public ScriptableObject objectReward;
	public Rare rare = Rare.C;	

	public Rare GetRare{get => rare;}
	public int RequireAmount{
		get{ 
			if(requireAmount <= 0) requireAmount = CalculateRequire();
			return requireAmount;
		}
	}	
	private int CalculateRequire(){return (20 + ((int)this.rare * 10));}
	public override Sprite Image {
		get{ 
			if(sprite == null){
				if(objectReward == null) SetReward();
				switch(typeSplinter){
					case TypeSplinter.Hero:
						sprite = ((InfoHero) objectReward).generalInfo.ImageHero;
						break;
				}
			}
			return sprite;
		}
	}
	private void SetReward(){
		switch(typeSplinter){
			case TypeSplinter.Hero:
				objectReward = (InfoHero) Resources.Load<InfoHero>( string.Concat("ScriptableObjects/HeroesData/", ID) ); 
				break;
		}
	}
	public void GetReward(int countReward){
		for(int i = 0; i < countReward; i++){
			if(amount >= requireAmount){
				switch(typeSplinter){
					case TypeSplinter.Hero:
						InfoHero hero = new InfoHero((InfoHero) objectReward);
						hero.generalInfo.Name = hero.generalInfo.Name + " №" + Random.Range(0, 1000).ToString();
						MessageControllerScript.Instance.AddMessage("Новый герой! Это - " + hero.generalInfo.Name);
						PlayerScript.Instance.AddHero(hero);
						break;
				}
				amount -= requireAmount;
			}
		}
	}
	public void SetAmount(int amount){
		this.amount = amount;
		requireAmount = CalculateRequire();
	}
	public void SetAmountRequire(){this.amount = RequireAmount;}
	public void AddAmount(int count){
		this.amount += count;
	}


//Constructors
	public Splinter(int ID, int count = 0){
		if(ID >= 1000){
			typeSplinter = TypeSplinter.Hero;
			this.objectReward = TavernScript.Instance.GetListHeroes.Find(x => x.generalInfo.idHero == ID);
			this.rare = (objectReward as InfoHero).generalInfo.rare;
			requireAmount = CalculateRequire();
			amount = count > 0 ? count : requireAmount;
		}
	}
	public override BaseObject Clone(){return new Splinter(this.ID);}
//Visual API
	public override void ClickOnItem(){ InventoryControllerScript.Instance.OpenInfoItem(this); }
	public override void SetUI(ThingUIScript UI){
		this.UI = UI;
		UpdateUI();
	}
	public override void UpdateUI(){
		UI?.UpdateUI(Image, rare, GetTextAmount());
	}		
}

public enum TypeSplinter{
		Hero,
		Artifact,
		Costume,
		Other}

public enum RaceSpliter{
	People,
	Elf,
    Undead,
    Mechanic,
    Inquisition,
    Demon,
    God,
    RandomAll,
    RandomWithoutGod}
    