using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ResourceObjectCost : MonoBehaviour{
    public Image image;
    public Text textAmount;
	private Resource costResource;
	private Resource storeResource; 

	public void SetInfo(Resource res){
		this.costResource = res;
		CheckResource();
		image.sprite = this.costResource.Image;
	}
	public bool CheckResource(){
		storeResource = PlayerScript.Instance.GetResource(costResource.Name);
		bool flag     = PlayerScript.Instance.CheckResource(costResource); 
		string result = flag ? "<color=black>" : "<color=red>";
		result = string.Concat(result, costResource.ToString(), "</color>/", storeResource.ToString());
		textAmount.text = result;
		OnCheckResource(flag);
		return flag;
	}
	private Action<bool> observerCanBuy;
	public void RegisterOnCanBuy(Action<bool> d){observerCanBuy += d;} 
	public void UnregisterOnCanBuy(Action<bool> d){observerCanBuy -= d;} 
	private void OnCheckResource(bool check){
		if(observerCanBuy != null)
			observerCanBuy(check);
	}
}
