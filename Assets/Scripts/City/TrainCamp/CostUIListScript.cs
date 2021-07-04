using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostUIListScript : MonoBehaviour{
    public List<GameObject> costObject = new List<GameObject>();

    public void ShowCosts(ListResource resourcesCost){
    	ClearPanelCosts();
    	for(int i=0; i< resourcesCost.List.Count; i++){
    		costObject[i].GetComponent<ResourceObjectCost>().SetInfo(resourcesCost.List[i]);
    		costObject[i].SetActive(true);
    	}
    }
    private void ClearPanelCosts(){
    	foreach (GameObject obj in costObject) {
    		obj.SetActive(false);
    	}
    }
}
