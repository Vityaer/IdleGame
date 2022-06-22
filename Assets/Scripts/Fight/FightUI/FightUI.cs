using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUI : MonoBehaviour{

	public FigthDirectionAttackUI SelectDirection;
	private static FightUI instance;
	public static FightUI Instance{get => instance;}
	void Awake(){
		instance = this;
	}
}