using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum TypeItem{
		Weapon,
		Shield,
		Helmet,
		Mittens,
		Armor,
		Boots,
		Amulet,
		Artifact,
		Stone,
		Resource,
		Splinter}

public enum SetItems{
	Rubbish,
	Pupil,
	Peasant,
	Militiaman,
	Monk,
	Warrior,
	Feller,
	Soldier,
	Minotaur,
	Demon,
	Druid,
	Obedient,
	Devil,
	Destiny,
	Archangel,
	Titan,
	God}

[System.Serializable]
public class Item{
	[SerializeField]
	protected string name;
	public string Name{get => name; set => name = value;}

	public string description;

	public int ID;
	private static Sprite[] spriteAtlas;   
	protected Sprite image;
	public virtual Sprite sprite{ get{ 	if(image == null) {
									if(spriteAtlas == null) spriteAtlas = Resources.LoadAll<Sprite>("Items/Items");
										for(int i=0; i < spriteAtlas.Length; i++){
											if(Name.Equals(spriteAtlas[i].name)){
												image = spriteAtlas[i];
												break;
											}
										}
									}
									return image;
								}}
	
	[SerializeField]
	private TypeItem type;
	public TypeItem Type{get => type;}

	[SerializeField]
	protected int rating;
	public int Rating{get => rating; set => rating = value;}
	
	[SerializeField]
	public SetItems set;
	public SetItems Set{get => set;}

	public Dictionary<string, float> bonuses = new Dictionary<string, float>();
	
	[SerializeField]
	public List<Bonus> ListBonuses;
//API
	public void SetBonus(){
		bonuses.Clear();
		for(int i=0; i < ListBonuses.Count; i++) {
			if(!bonuses.ContainsKey( ListBonuses[i].Name.ToString() ) ){
				bonuses.Add(ListBonuses[i].Name.ToString(), ListBonuses[i].Count);
			}else{
				bonuses[ListBonuses[i].Name.ToString()] += ListBonuses[i].Count;
			}
		}
	}
	public bool IsNull(){
		return (sprite == null);
	}
	public string GetTextBonuses(){
		string result = "";
		foreach (KeyValuePair<string, float> keyValue in bonuses){
			if(keyValue.Value != 0){
				result = string.Concat(result, GetText(keyValue.Value, keyValue.Key));
			}
		}
		return result;

		
	}
	private string GetText(float bonus, string who){
		string result = "";
		result = string.Concat(result, (bonus > 0) ? "<color=green>+" : "<color=red>", Math.Round(bonus, 1).ToString(), "</color> ", who,"\n");
		return result;
	}
	public float GetBonus(string typeBonus){
		if(bonuses.Count == 0) SetBonus();
		float result = 0;
		if(bonuses.ContainsKey(typeBonus)){
			result = bonuses[typeBonus];
		}
		return result;
	}

	public void UpdateUI(){
		
	}

}
