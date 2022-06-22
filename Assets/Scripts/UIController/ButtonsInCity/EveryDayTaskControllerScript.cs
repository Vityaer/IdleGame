using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryDayTaskControllerScript : Building{
    [SerializeField] private Transform  taskboard;
	[SerializeField] private GameObject prefabEveryDayTask;
	[SerializeField] private List<EveryDayTask> generalEveryDayTask = new List<EveryDayTask>(); 

	
	protected override void OnStart(){
		CreateTasks();
	}
	void CreateTasks(){
		foreach(EveryDayTask task in generalEveryDayTask){
			Instantiate(prefabEveryDayTask, taskboard).GetComponent<RequirementUI>().SetData(task as Requirement);
		}
	}
}
