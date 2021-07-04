using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonCostScript : MonoBehaviour{
	public Text textCost;
	public Button btn;
	public Image imgRes;
	public Resource res;
	public delegate void Del(int count);
	public Del delBuyMatter;
	private bool disable = false;
	void Start(){
		if(res.Count > 0){
			UpdateInfo();
		}
	}
	public void UpdateCost(Resource res, Del d){
		if(disable == false){
			delBuyMatter  = d;
			this.res      = res;
			UpdateInfo(); 
		}
	}
	private void UpdateInfo(){
		if(disable == false){
			textCost.text = (res.Count > 0) ? res.ToString() : "Бесплатно";
			imgRes.sprite = res.sprite;
			PlayerScript.Instance.RegisterOnChangeResource( CheckResource, res.Name );
			CheckResource( res );
		}
	}
	public void Buy(){
		if(delBuyMatter != null){
			SubstractResource();
			delBuyMatter(1);
		}
	}

	public void CheckResource(Resource res){
		if(disable == false) btn.interactable = PlayerScript.Instance.CheckResource( this.res );
	}
	public void Disable(){
		disable          = true;
		PlayerScript.Instance.UnRegisterOnChangeResource( CheckResource, res.Name );
		delBuyMatter     = null;
		btn.interactable = false;	
	}
	public void SubstractResource(){
		PlayerScript.Instance.SubtractResource(res);
	}
}
