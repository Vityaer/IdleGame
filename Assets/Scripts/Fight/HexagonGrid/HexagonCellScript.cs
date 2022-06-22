using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR_WIN
using UnityEditor;
#endif


public class HexagonCellScript : MonoBehaviour{

	private Transform tr;
	public Vector3 Position{get => transform.position;} 
	[SerializeField] private List<NeighbourCell> neighbours = new List<NeighbourCell>();
	public SpriteRenderer spriteCell, spriteAvailable;
	private GameObject subject;
	HeroControllerScript heroScript;
	public HeroControllerScript Hero{get => heroScript;}
	public bool available = true;
	public bool availableMove = true;

	void Awake(){
		tr = base.transform;
	}
	public bool achievableMove = false; 
	public int step = 0;
	private bool showAchievable = false;

	public bool playerCanController = false;	
	public void StartCheckMove(int step, bool playerCanController){
		this.playerCanController = playerCanController;
		this.step = step;
		achievableMove = true;
		HeroControllerScript.RegisterOnEndAction(ClearCanMove);
		for(int i = 0; i < neighbours.Count; i++) neighbours[i].CheckMove(step - 1, playerCanController);
	}

	public void CheckMove(int step, bool playerCanController){
		if(available && availableMove){
			if((achievableMove == false) || (this.step < step)){
				this.playerCanController = playerCanController;
				this.step = step;
				OnAchivableMove();
				achievableMove = true;
				if((showAchievable == false) && playerCanController){
					showAchievable = true;
					spriteAvailable.enabled = true;
					HeroControllerScript.RegisterOnEndAction(ClearCanMove);
				}
				if(step > 0) for(int i = 0; i < neighbours.Count; i++) neighbours[i].CheckMove(step - 1, playerCanController);
			}
		}
	}
	public HexagonCellScript GetAchivableNeighbourCell(){
		return neighbours.Find(x => x.achievableMove).Cell;
	}
	public bool GetCanAttackCell{ get => (neighbours.Find(x => (x.achievableMove == true)) != null); }
		// get{
		// 	Debug.Log("current cell: "  + gameObject.name);
		// 	for(int i= 0; i < neighbours.Count  ;i++){
		// 		if(neighbours[i].achievableMove == true){
		// 			Debug.Log(neighbours[i].Cell.gameObject.name);
		// 		}
		// 	}
		// 	return (neighbours.Find(x => (x.achievableMove == true)) != null);
		// }
	// }
	public void RegisterOnSelectDirection(Action<HexagonCellScript> selectDirectionForHero, bool showUIDirection = true){
		observerSelectDirection = selectDirectionForHero;
		if(showUIDirection)
			ShowDirectionsAttack();
	}
	private void ShowDirectionsAttack(){
		FightUI.Instance.SelectDirection.RegisterOnSelectDirection(SelectDirection, this, neighbours);
	}

	private void SelectDirection(NeighbourDirection direction){
		if(observerSelectDirection != null){
			observerSelectDirection(GetNeighbourCellOnDirection(direction));
			observerSelectDirection = null;
		}
	}
	private HexagonCellScript GetNeighbourCellOnDirection(NeighbourDirection direction){
		return neighbours.Find(x => x.direction == direction).Cell;
	}


	public void ClearCanMove(){
		showAchievable = false;
		this.step = 0;
		achievableMove = false;
		spriteAvailable.enabled = false;
		HeroControllerScript.UnregisterOnEndAction(ClearCanMove);
	}
	public void SetSubject(GameObject newSubject){
		this.subject = newSubject;
	}
	public void SetHero(HeroControllerScript hero){
		heroScript = hero;
		subject = hero.gameObject;
		availableMove = false;
	}
	public void ClearSublject(){
		this.subject = null;
		heroScript = null;
		availableMove = true;
	}

	public void ClickOnMe(){
		if(playerCanController){
			if(achievableMove || (heroScript != null)){
				OnClickCell();
			}
		}
	}
	public void AITurn(){ OnClickCell(); }
	private static Action<HexagonCellScript> observerClick, observerSelectDirection, observerAchivableMove;
	public static void RegisterOnClick(Action<HexagonCellScript> d){ observerClick += d;}
	public static void UnregisterOnClick(Action<HexagonCellScript> d){ observerClick -= d;}
	private void OnClickCell(){if((observerClick != null) && (available == true)) observerClick(this);}
	
	private void OnSelectDirection(){if((observerClick != null) && (available == true)) observerClick(this);}

	public static void RegisterOnAchivableMove(Action<HexagonCellScript> d){observerAchivableMove += d; }
	public static void UnregisterOnAchivableMove(Action<HexagonCellScript> d){observerAchivableMove += d; }
	private void OnAchivableMove(){if(observerAchivableMove != null) observerAchivableMove(this);}

	public void CheckOnNeighbour(HexagonCellScript otherCell){
		if(Vector3.Distance(this.Position, otherCell.Position) < (1.1f * transform.localScale.x))
			neighbours.Add(new NeighbourCell(this, otherCell));
	}
	public void ClearNeighbours(){ neighbours.Clear(); }
	
#if UNITY_EDITOR_WIN
//Potision controller	
	private static Vector2 deltaSize = new Vector2(0.826f, 0.715f);
	[ContextMenu("UpLeft")]
	private void MoveUpLeft(){ Move( new Vector3(-deltaSize.x * transform.localScale.x/2, deltaSize.y * transform.localScale.y,0) ); }
	
	[ContextMenu("UpRight")]
	private void MoveUpRight(){ Move( new Vector3(deltaSize.x * transform.localScale.x/2, deltaSize.y * transform.localScale.y,0) ); }

	[ContextMenu("Left")]
	private void MoveLeft(){ Move( new Vector3(- deltaSize.x * transform.localScale.x, 0,0) ); }
	
	[ContextMenu("Right")]
	private void MoveRight(){ Move( new Vector3(deltaSize.x * transform.localScale.x, 0,0) ); }
	
	[ContextMenu("DownLeft")]
	private void MoveDownLeft(){ Move( new Vector3(-deltaSize.x * transform.localScale.x/2, -deltaSize.y * transform.localScale.y,0) ); }

	[ContextMenu("DownRight")]
	private void MoveDownRight(){ Move( new Vector3(deltaSize.x * transform.localScale.x/2, -deltaSize.y * transform.localScale.y,0) ); }


	private void Move(Vector3 dir){
		Undo.RecordObject(transform, "hexagon position");
		tr.position = tr.position + dir;
	}
#endif
}
