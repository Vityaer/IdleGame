using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityControllerScript : MonoBehaviour{

	public SliderCityScript sliderCity;

	[Header("UI")]
	private Canvas canvasCity;
	public FooterButtonScript btnOpenClose;
	public GameObject background;
	[Header("UI Button")]
	[SerializeField] Canvas canvasButtonsUI; 
	void Awake(){
		canvasCity = GetComponent<Canvas>();
		btnOpenClose.RegisterOnChange(Change);
	}
	void Change(bool isOpen){
		if(isOpen){ Open(); }else{ Close(); }
	}
	public void Open(){
		canvasCity.enabled = true;
		sliderCity.enabled = true;
		canvasButtonsUI.enabled = true;
		BackGroundControllerScript.Instance.OpenBackground(background);
	}
	public void Close(){
		canvasCity.enabled = false;
		sliderCity.enabled = false;	
		canvasButtonsUI.enabled = false;
	}
}
