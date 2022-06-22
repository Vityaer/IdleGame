using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour{
	public List<Warrior> leftTeam = new List<Warrior>();
	public List<Warrior> rightTeam = new List<Warrior>();
	public List<HexagonCellScript> achievableMoveCells = new List<HexagonCellScript>();
	public void StartAI(){
		FightControllerScript.Instance.RegisterOnStartFight(StartFight);
		FightControllerScript.Instance.RegisterOnFinishFight(FinishFight);
		HexagonCellScript.RegisterOnAchivableMove(AddAchivableMoveCell);
		HeroControllerScript.RegisterOnStartAction(OnHeroStartAction);
		HeroControllerScript.RegisterOnEndAction(ClearInfo);
		ClearInfo();
	}
	void StartFight(){
		rightTeam = FightControllerScript.Instance.rightTeam;
		leftTeam = FightControllerScript.Instance.leftTeam;
	}
	void ClearInfo(){
		Debug.Log("skynet: clear info");
		achievableMoveCells.Clear();
	}
	void FinishFight(){
		FightControllerScript.Instance.UnregisterOnStartFight(StartFight);
		FightControllerScript.Instance.UnregisterOnFinishFight(FinishFight);
		HeroControllerScript.UnregisterOnEndAction(ClearInfo);
		HexagonCellScript.UnregisterOnAchivableMove(AddAchivableMoveCell);
	}
	Side sideForAI = Side.Right;
	void OnHeroStartAction(HeroControllerScript heroConroller){
		if((heroConroller.side == sideForAI) ||(sideForAI == Side.All)){
			List<Warrior> workTeam = (heroConroller.side == Side.Right) ? leftTeam : rightTeam;
			Warrior enemy = workTeam.Find(x => x.Cell.GetCanAttackCell == true);
			if(heroConroller.Mellee == true){
				if(enemy != null){
					heroConroller.SelectDirectionAttack(enemy.Cell.GetAchivableNeighbourCell(), enemy.heroController);
				}else{
					achievableMoveCells[UnityEngine.Random.Range(0, achievableMoveCells.Count)].AITurn();				
				}
			}else{
				enemy = workTeam[UnityEngine.Random.Range(0, workTeam.Count)]; 
				heroConroller.StartDistanceAttackOtherHero(enemy.heroController);
			}
		}
	}
	public bool CheckMeOnSubmission(Side side){
		return ((side == sideForAI) || (sideForAI == Side.All));
	}
	void AddAchivableMoveCell(HexagonCellScript newCell){
		achievableMoveCells.Add(newCell);
	}

}