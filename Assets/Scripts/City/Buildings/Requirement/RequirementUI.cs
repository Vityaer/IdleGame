using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUI : MonoBehaviour{
	[SerializeField] private Text description;
	public Requirement requirement;
	public void GetReward(){
		requirement.GetReward();
	}
	public void SetData(Requirement requirement){
		this.requirement = requirement;
		description.text = requirement.description;
	}
}