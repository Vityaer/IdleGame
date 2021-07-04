using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelPlayerScript : Building{
	[SerializeField] private Slider sliderLevel;
	[SerializeField] private Text nameText, levelText, IDGuildText, idText;
	[SerializeField] private CostLevelUp playerLevelList;
	[SerializeField] private Image avatar, outlineAvatar;
	private Player player;
	void Start(){
		player = PlayerScript.Instance.player;
		UpdateMainUI();
	}
	protected override void OpenPage(){
	
	}
	private void UpdateMainUI(){
		nameText.text = player.Name;
		levelText.text = player.Level.ToString();
		IDGuildText.text = player.IDGuild.ToString();
		avatar.sprite = player.avatar;
	}

}