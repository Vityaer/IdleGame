using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceObjectCost : MonoBehaviour{
    public Image image;
    public Text textAmount;
	public Resource currentRes;
	private Resource storeResource; 

	public void SetInfo(Resource res){
		this.currentRes = res;
		CheckResource();
		image.sprite = this.currentRes.sprite;
	}
	public bool CheckResource(){
		storeResource = PlayerScript.Instance.GetResource(currentRes.Name);
		bool flag     = PlayerScript.Instance.CheckResource(currentRes); 
		string result = flag ? "<color=black>" : "<color=red>";
		result = string.Concat(result, currentRes.ToString(), "</color>/", storeResource.ToString());
		textAmount.text = result;
		return flag;
	}
}
