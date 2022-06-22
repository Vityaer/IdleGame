using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementMenuScript : Building{
	[SerializeField] private Transform  taskboard;
	[SerializeField] private GameObject prefabRequirement;
	[SerializeField] private ListRequirement patternRequirements; 

	
	bool isCreate = false;
	protected override void OnStart(){
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
