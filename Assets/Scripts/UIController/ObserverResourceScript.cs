using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverResourceScript : MonoBehaviour{
	[Header("General")]
	public TypeResource typeResource;
	public bool         isMyabeBuy;
	public int          cost;
	private Resource resource;

	[Header("UI")]
	public GameObject btnAddResource;
	public Image imageResource;
	public Text countResource;
	void Start(){
		resource             = new Resource(typeResource);
		imageResource.sprite = resource.sprite;
		btnAddResource.SetActive(isMyabeBuy);
		PlayerScript.Instance.RegisterOnChangeResource(UpdateUI, typeResource);
		UpdateUI(PlayerScript.Instance.GetResource(typeResource));
	}

	public void UpdateUI(Resource res){
		resource = res;
		countResource.text = resource.ToString();
	}
}
