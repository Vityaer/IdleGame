using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeBonus{
		HP = 0,
		Attack = 1,
		Armor = 2,
		Initiative = 3,
		CriticalDamage = 4,
		DefendCriticalDamage = 5,
		Evasion = 6,
		MagicDamage = 7,
		DefendMagicDamage = 8,

	}

[System.Serializable]
public class Bonus{
	[SerializeField]
	private TypeBonus name;
	public TypeBonus Name{get => name; set => name = value;}

	[SerializeField]
	private float count;
	public float Count{get => count; set => count = value;} 
}
