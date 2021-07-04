using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class MineControllerScript : MonoBehaviour{
	private Mine mine;
	[SerializeField] private Image image;
	[SerializeField] private int ID;
	[SerializeField] private ListResource listRes;
	[Header("UI")]
	[SerializeField] Text textLevel;
	void Start(){
		mine = PlayerScript.Instance.LoadMine(ID);	
		mine.listRes = listRes;
		UpdateUI();
	}
	public void UpdateLevel(){
		mine.LevelUP();
		UpdateUI();
	}
	public void OpenPanelInfo(){
		PanelInfoMine.Instance.SetData(mine);
	}
	private void UpdateUI(){
		textLevel.text = string.Concat("Уровень ", mine.level.ToString());  
	}
}
