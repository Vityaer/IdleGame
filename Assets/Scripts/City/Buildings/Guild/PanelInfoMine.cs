using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoMine : MonoBehaviour{
	[SerializeField] private Image image;
	[SerializeField] private Text textNameMine;
	[SerializeField] private Text textNamePanel;
	[SerializeField] private Text textLevelMine;
	[SerializeField] private ButtonCostScript buttonCost;
	private Mine mine;
	public void SetData(Mine mine){
		this.mine = mine;
		UpdateUI();
	}
	private void LevelUP(int count){
		mine.LevelUP();
		UpdateUI();
	}
	private void UpdateUI(){
		if(mine.FirstResource == null) {Debug.Log("нет русурсов");}else{Debug.Log("ресурсы есть");}
		image.sprite = mine.FirstResource.sprite;
		textNameMine.text = mine.FirstResource.Name.ToString();
		textLevelMine.text = string.Concat("Уровень ", mine.level.ToString()); 
		buttonCost.UpdateCost(new Resource(TypeResource.Gold, 10f * mine.level, 0), LevelUP);
	}
	public void GetUpResource(){
		mine.GetResources();
	}
	private static PanelInfoMine instance;
	public static PanelInfoMine Instance{get => instance;}
	void Awake(){
		instance = this;
	}
}
