using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class Building : SerializedMonoBehaviour{

	protected static Canvas canvasBuilding;
	[SerializeField] protected GameObject building;

	void Start(){
		if(canvasBuilding == null)
			canvasBuilding = GameObject.Find("Buildings").GetComponent<Canvas>();
		OnStart();	
		if(building != null){
			canvasBuilding.enabled = false;
			building.SetActive(false); 
		}
	}
	public virtual void Open(){
		if(building != null){
			MenuControllerScript.Instance.CloseMainPage();
			MenuControllerScript.Instance.canvasCity.Close();
			canvasBuilding.enabled = true;
			building.SetActive(true); 
		}
		OpenPage();
	}
	public virtual void Close(){
		ClosePage();
		if(building != null){
			MenuControllerScript.Instance.OpenMainPage();
			MenuControllerScript.Instance.canvasCity.Open();
			canvasBuilding.enabled = false;
			building.SetActive(false); 
		}
	}
	virtual protected void OnStart(){}
	virtual protected void OpenPage(){}
	virtual protected void ClosePage(){}
}
