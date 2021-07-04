using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGiverScript : Building{

	public static List<Task> tasks = new List<Task>();
	public Transform taskboard;
	private List<TaskControllerScript> taskControllers = new List<TaskControllerScript>(); 
	public GameObject prefabTask;
	public PatternTask patternTasks; 
	protected override void OpenPage(){
		LoadTasks();
		if((taskControllers.Count == 0) && (tasks.Count > 0)) FirstCreateTasks();
	}

	protected override void ClosePage(){
		foreach(TaskControllerScript task in taskControllers){
			task.StopTimer();
		}
	}
	private void FirstCreateTasks(){
		for(int i=0; i < tasks.Count; i++){
			taskControllers.Add(Instantiate(prefabTask, taskboard).gameObject.GetComponent<TaskControllerScript>());
			taskControllers[i].UpdateUI(tasks[i]);
		}
	}
	public void CreateSimpleTask(){
		CreateTask(patternTasks.GetSimpleTask());
	}
	public void CreateSprecialTask(){
		CreateTask(patternTasks.GetSpecialTask());
	}
	private void CreateTask(Task newTask){
		tasks.Add(newTask);
		TaskControllerScript newTaskController = Instantiate(prefabTask, taskboard).gameObject.GetComponent<TaskControllerScript>();
		taskControllers.Add(newTaskController);
		newTaskController.UpdateUI(newTask);

	}
	private static TaskGiverScript instance;
	public static TaskGiverScript Istance{get => instance;}	
	bool isLoadedTask = false;
	void Start(){
		if(instance == null){
			instance = this;
			tasks = PlayerScript.Instance.player.PlayerGame.listTasks;
		}else{
			Debug.Log("double task giver");
		}
	}
	void LoadTasks(){
		if(isLoadedTask == false){
			if(PlayerScript.Instance.flagLoadedGame == true){
				tasks = PlayerScript.Instance.player.PlayerGame.listTasks;
				isLoadedTask = true;
			}
		}

	}	
}
