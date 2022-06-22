using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class FigthDirectionAttackUI : MonoBehaviour{
	public GameObject panelDirectionAttack;
	private Action<NeighbourDirection> actionOnSelectDirection;
	public Image directionUpLeft,directionUpRight,directionLeft,directionRight,directionBottomLeft,directionBottomRight;
	public void AttackDirectionSelect(int numDirection){
		if(actionOnSelectDirection != null)
			actionOnSelectDirection((NeighbourDirection) numDirection);
		Close();	
	}
	private void Open(HexagonCellScript cell, List<NeighbourCell> neighbours){
		directionUpLeft.enabled      = CheckNeighbour(neighbours, NeighbourDirection.UpLeft);
		directionUpRight.enabled     = CheckNeighbour(neighbours, NeighbourDirection.UpRight);
		directionLeft.enabled        = CheckNeighbour(neighbours, NeighbourDirection.Left);
		directionRight.enabled       = CheckNeighbour(neighbours, NeighbourDirection.Right);
		directionBottomLeft.enabled  = CheckNeighbour(neighbours, NeighbourDirection.BottomLeft);
		directionBottomRight.enabled = CheckNeighbour(neighbours, NeighbourDirection.BottomRight);
		panelDirectionAttack.transform.position = cell.Position; 
		panelDirectionAttack.SetActive(true);
	}
	private bool CheckNeighbour(List<NeighbourCell> neighbours, NeighbourDirection direction){
		NeighbourCell cell = neighbours.Find(x => x.direction == direction);
		return (cell == null) ? false : cell.achievableMove;
	}
	private void Close(){
		panelDirectionAttack.SetActive(false);
	}
	public void RegisterOnSelectDirection(Action<NeighbourDirection> d,HexagonCellScript cell, List<NeighbourCell> neighbours){ actionOnSelectDirection += d; Open(cell, neighbours); }
	public void UnregisterOnSelectDirection(Action<NeighbourDirection> d){ actionOnSelectDirection -= d; Close(); }
}

