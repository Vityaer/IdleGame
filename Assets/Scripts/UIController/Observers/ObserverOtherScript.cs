using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverOtherScript : MonoBehaviour{
	public TypeObserverOther type;
	public bool         isMyabeBuy;

	[Header("UI")]
	public GameObject btnAddResource;
	public Image imageObserver;
	public Text textObserver;
	void Start(){
		RegisterOnChange();
		UpdateUI();
	}
	void RegisterOnChange(){
		switch(type){
			case TypeObserverOther.CountHeroes:
				PlayerScript.Instance.RegisterOnChangeCountHeroes(UpdateUI);
				break;
		}
	}
    public void UpdateUI(){
    	switch(type){
    		case TypeObserverOther.CountHeroes:
    			textObserver.text = string.Concat(PlayerScript.Instance.GetCurrentCountHeroes.ToString(), "/", PlayerScript.Instance.GetMaxCountHeroes.ToString());
	    		break;
    	}
	}
	public void OpenPanelForBuyResource(){
		// MarketProduct<Resource> product = null;
		// product = MarketResourceScript.Instance.GetProductFromTypeResource(resource.Name);
		// if(product != null)
		// 	PanelBuyResourceScript.StandartPanelBuyResource.Open(
		// 		product.subject, product.cost
		// 		);
	}
}
public enum TypeObserverOther{
	CountHeroes
}
