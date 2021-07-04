using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike{
	public TypeStrike type = TypeStrike.Physical;
	public float bonusNum = 0f;
	public float bonusPercent = 100f;
	public TypeNumber typeNumber;
	public float baseAttack;
	
	public void AddBonus(float bonus,  TypeNumber typeNumber = TypeNumber.Num){
		if(typeNumber == TypeNumber.Num){
			this.bonusNum     += bonus;
		}else{
			this.bonusPercent += bonus;
		}
	}
	public float GetDamage(){
		float result = 0f;
		result = (baseAttack * bonusPercent/100f) + bonusNum;
		if(result < 0) Debug.Log("negative damage");
		return Mathf.Floor(result);
	}
	public Strike(float baseAttack, TypeNumber typeNumber = TypeNumber.Num, TypeStrike typeStrike = TypeStrike.Physical){
		this.baseAttack = baseAttack;
		this.typeNumber = typeNumber;
		this.type       = typeStrike;
	}
	public override string ToString(){
		return GetDamage().ToString();
	}
}

public enum TypeStrike{
	Physical,
	Critical,
	Magical,
	Clean,
	Electrical,
	Fiery,
	Hew,
	Plosive,
	Elemental,
	Holy,
	Dark,
	Poison
}