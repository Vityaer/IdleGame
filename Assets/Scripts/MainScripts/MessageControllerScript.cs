using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AndroidPlugin;

public class MessageControllerScript : MonoBehaviour{

	private static MessageControllerScript instance;
	public  static MessageControllerScript Instance{get => instance;}
	private Canvas canvas;
	void Awake(){
		canvas = GetComponent<Canvas>();
		instance = this;
	}
	public WinLosePanelScript winPanelScript, losePanelScript;
	public void OpenWin(Reward reward, string message = ""){
		if(reward != null){
			PlayerScript.Instance.AddReward(reward);
			winPanelScript.OpenPanel(reward, message);
			canvas.enabled = true;
		}
	}
	public void OpenLose(Reward reward, string message = ""){
		if(reward != null){
			PlayerScript.Instance.AddReward(reward);
			losePanelScript.OpenPanel(reward, message);
			canvas.enabled = true;
		}
	}

	public void AddMessage(string newMessage, bool isLong = false){
		Debug.Log(newMessage);
		AndroidPlugin.PluginControllerScript.ToastPlugin.Show(newMessage, isLong: isLong);
	}
}
