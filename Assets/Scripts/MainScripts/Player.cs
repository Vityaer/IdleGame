using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Player{
	[SerializeField] private string name;

	[SerializeField] private int level = 1;

	[SerializeField] private Resource exp;

	public int IDGuild;
	public Sprite avatar;
	public int IDServer;

	[SerializeField] private int vipLevel;

	[SerializeField] private Game playerGame;

	public string Name{get => name;}
	public Resource Exp{get => exp;}
	public int VipLevel{get => level;}
	public Game PlayerGame{get => playerGame; set => playerGame = value;}
	public int Level{get => level;}

//API
	public void AddExp(Resource newExp){
		this.exp.AddResource(exp); 
	}

//Observers
	private Action<BigDigit> observerOnLevelUP;
	public void RegisterOnLevelUP(Action<BigDigit> d){observerOnLevelUP += d;}
	private void OnLevelUP(){ if(observerOnLevelUP != null) observerOnLevelUP(new BigDigit(Level, 0)); }	 
}
