using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class ButtonCostScript : MonoBehaviour{
	public TextMeshProUGUI textCost;
	public Button btn;
	public Image imgRes;
	public Resource res;
	public Action<int> delBuyMatter;
	public int countBuy = 1;
	private bool disable = false;
	void Start(){
		if(res.Count > 0){
			UpdateInfo();
		}
	}
	public void UpdateCost(Resource res, Action<int> d){
		delBuyMatter  = d;
		this.res      = res;
	    UpdateInfo(); 
	}
	public void RegisterOnBuy(Action<int> d){
		delBuyMatter = d;
		UpdateInfo();
	}
	public void UpdateCostWithoutInfo(Resource res, Action<int> d){
		delBuyMatter  = d;
		this.res      = res;
		CheckResource( res );
	}
	private void UpdateInfo(){
		if(disable == false){
			if(res.Count > 0){
				textCost.text = res.ToString();
				imgRes.enabled = true;
				imgRes.sprite = res.Image;
				PlayerScript.Instance.RegisterOnChangeResource( CheckResource, res.Name );
			}else{
				textCost.text = "Бесплатно";
				imgRes.enabled = false;
			}
			CheckResource( res );
		}
	}
	public void Buy(){
		if((delBuyMatter != null) && (disable == false)){
			SubstractResource();
			delBuyMatter(countBuy);
		}
	}

	public void CheckResource(Resource res){
		if(disable == false) btn.interactable = PlayerScript.Instance.CheckResource( this.res );
	}
	public void Disable(){
		disable          = true;
		PlayerScript.Instance.UnRegisterOnChangeResource( CheckResource, res.Name );
		btn.interactable = false;	
	}
	public void EnableButton(){
		disable          = false;
		PlayerScript.Instance.RegisterOnChangeResource( CheckResource, res.Name );
	}
	private void SubstractResource(){
		PlayerScript.Instance.SubtractResource(res);
	}
}
