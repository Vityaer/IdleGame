using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "PatternTasks", menuName = "Custom ScriptableObject/PatternTasks", order = 55)]
public class PatternTask : ScriptableObject{
	public List<Task> tasks = new List<Task>();

	public Task GetSimpleTask(){
		List<Task> workTasks = tasks.FindAll(x => (x.rating <= 4));
		return (Task) (workTasks[Random.Range(0, workTasks.Count)]).Clone();
	}  
	public Task GetSpecialTask(){
		List<Task> workTasks = tasks;
		return (Task) (workTasks[Random.Range(0, workTasks.Count)]).Clone();
	} 
}
