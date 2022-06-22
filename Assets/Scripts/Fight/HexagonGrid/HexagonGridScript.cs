using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGridScript : MonoBehaviour{

	[SerializeField] private List<HexagonCellScript> cells = new List<HexagonCellScript>(); 
	private void FindAllCell(){
		cells.Clear();
		foreach(Transform child in base.transform){
			if(child.GetComponent<HexagonCellScript>() != null){
				cells.Add(child.GetComponent<HexagonCellScript>());
			}
		}
	}
	[ContextMenu("FindAllCell")]
	public void FindNeighbours(){
		if(cells.Count == 0) FindAllCell();
		for(int i = 0; i < cells.Count; i++) cells[i].ClearNeighbours();
		for(int i = 0; i < cells.Count - 1; i++){
			for(int j = i + 1; j < cells.Count; j++){
				cells[i].CheckOnNeighbour(cells[j]);
				cells[j].CheckOnNeighbour(cells[i]);
			}
		}
	}
	private static HexagonGridScript instance;
	public static HexagonGridScript Instance {get => instance;}
	void Awake(){
		instance = this;
	}
	void Start(){
		FindNeighbours();
		FightControllerScript.Instance.RegisterOnStartFight(StartFight);
		FightControllerScript.Instance.RegisterOnFinishFight(FinishFight);
	}
	bool fighting = false;
	Coroutine coroutineCheckClick = null;
	void StartFight(){
		fighting = true;
	}
    
    RaycastHit2D hit;
 	void Update(){
		if(fighting){
			if (Input.GetMouseButtonDown(0)){
		        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.down);
		        if(hit != null){
		        	if(hit.transform != null){
		                if (hit.transform.CompareTag("HexagonCell")){
		                    HexagonCellScript HexagonCell = hit.collider.transform.GetComponent<HexagonCellScript>();
		                    HexagonCell.ClickOnMe();
		                }
		                if(hit.transform.CompareTag("Hero")){
		                	 hit.collider.transform.GetComponent<HeroControllerScript>().ClickOnMe();
			        	}
	                }
	            }
	        }
		}
 	}
	void FinishFight(){
		fighting = false;
	}
}