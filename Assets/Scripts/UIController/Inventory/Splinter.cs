using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Splinter : Item{
	public TypeSplinter typeSplinter;
	public RaceSpliter race;
	public int requireAmount;
	public ScriptableObject objectReward;
	public Rare rare = Rare.C;	
	public override Sprite sprite {
		get{ 
			if(image == null){
				if(objectReward == null) SetReward();
				switch(typeSplinter){
					case TypeSplinter.Hero:
						image = ((InfoHero) objectReward).generalInfo.ImageHero;
						break;
				}
			}
			return image;
		}
	}
	private void SetReward(){
		switch(typeSplinter){
			case TypeSplinter.Hero:
				objectReward = (InfoHero) Resources.Load<InfoHero>( string.Concat("ScriptableObjects/HeroesData/", ID) ); 
				break;
		}
	}
	public int GetReward(int curAmount){
		int result = 0;
		if(curAmount >= requireAmount){
			switch(typeSplinter){
				case TypeSplinter.Hero:
					InfoHero hero = new InfoHero((InfoHero) objectReward);
					hero.generalInfo.Name = hero.generalInfo.Name + " №" + Random.Range(0, 1000).ToString();
					MessageControllerScript.Instance.AddMessage("Новый герой! Это - " + hero.generalInfo.Name);
					PlayerScript.Instance.AddHero(hero);
					result = requireAmount;
					break;
			}
		}
		return result;
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
    