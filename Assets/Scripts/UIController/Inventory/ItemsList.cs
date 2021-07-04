﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Custom ScriptableObject/Item", order = 53)]
[System.Serializable]
public class ItemsList : ScriptableObject{
	[Header("Forge")]
	[Header("Weapons")]
	[SerializeField]
	private List<Item> weapons = new List<Item>();
	[Header("Shield")]
	[SerializeField]
	private List<Item> shields = new List<Item>();
	[Header("Helmet")]
	[SerializeField]
	private List<Item> helmets = new List<Item>();
	[Header("Mittens")]
	[SerializeField]
	private List<Item> mittens = new List<Item>();
	[Header("Armors")]
	[SerializeField]
	private List<Item> armors = new List<Item>();
	[Header("Boots")]
	[SerializeField]
	private List<Item> boots = new List<Item>();
	[Header("Amulet")]
	[SerializeField]
	private List<Item> amulets = new List<Item>();
	public Item GetItem(int ID){
		Item result = GetItemFromList( ID );
		if(result == null) Debug.Log("не нашли такого предмета "+ ID.ToString()); 
		return result;
	}
	private Item GetItemFromList(int ID){
		Item result = null;
		int numList = ID / 100;
		switch(numList){
			case 1:
				result = weapons.Find(x => (x.ID == ID));
				break;
			case 2:
				result = boots.Find(x => (x.ID == ID));
				break;
			case 3:
				result = armors.Find(x => (x.ID == ID));
				break;
			case 4:
				result = helmets.Find(x => (x.ID == ID));
				break;
			case 5:
				result = shields.Find(x => (x.ID == ID));
				break;
			case 6:
				result = amulets.Find(x => (x.ID == ID));
				break;
			case 7:
				result = mittens.Find(x => (x.ID == ID));
				break;						
		}
		return result;
	}
}
