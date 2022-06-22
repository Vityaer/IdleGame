using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : BaseObject{
	[SerializeField] protected int amount;
	public int Amount{get => amount;}
	[SerializeField] protected string name = string.Empty;
	public string Name{get => name;}
	[SerializeField] protected int id;
	public int ID{get => id;}
	[SerializeField] protected string description;
	public string Description{get => description;}
	[SerializeField] protected int rating;
	public int Rating{get => rating;}
	
}