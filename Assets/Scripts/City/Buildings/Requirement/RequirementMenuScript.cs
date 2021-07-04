using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementMenuScript : Building{
	[SerializeField] private Transform  taskboard;
	[SerializeField] private GameObject prefabRequirement;
	[SerializeField] private ListRequirements patternRequirements; 
	bool isCreate = false;
	protected override void OpenPage(){
		if(isCreate == false){
			CreateRequrements();
		}
		
	}
	void CreateRequrements(){
		isCreate = true;
		foreach(Requirement requirement in patternRequirements.listRequirement){
			Instantiate(prefabRequirement, taskboard).GetComponent<RequirementUI>().SetData(requirement);
		}
	}
}
