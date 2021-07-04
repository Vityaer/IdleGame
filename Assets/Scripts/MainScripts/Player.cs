using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player{
	[SerializeField]
	private string name;
	public string Name{get => name;}

	[SerializeField]
	private int level = 1;
	public int Level{get => level;}

	[SerializeField]
	private Resource exp;
	public Resource Exp{get => exp;}

	public int IDGuild;
	public Sprite avatar;
	public int IDServer;

	[SerializeField]
	private int vipLevel;
	public int VipLevel{get => level;}

	[SerializeField]
	private Game playerGame;
	public Game PlayerGame{get => playerGame; set => playerGame = value;}

}
