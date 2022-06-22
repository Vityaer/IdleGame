using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeighbourCell{
	public NeighbourDirection direction;
	private HexagonCellScript cell;

	public  HexagonCellScript Cell{get => cell;}
	public bool achievableMove{get => cell.achievableMove;}

	public void CheckMove(int step, bool playerCanController){ cell.CheckMove(step, playerCanController); }
	public NeighbourCell(HexagonCellScript mainCell, HexagonCellScript neighbourCell){
		cell = neighbourCell;
		float deltaX = neighbourCell.Position.x - mainCell.Position.x;
		float deltaY = neighbourCell.Position.y - mainCell.Position.y;
		if(deltaX > 0){
			if(deltaY > 0.1f){
				direction = NeighbourDirection.UpRight; 
			}else if(deltaY < -0.1f){
				direction = NeighbourDirection.BottomRight; 
			}else{
				direction = NeighbourDirection.Right; 
			}
		}else{
			if(deltaY > 0.1f){
				direction = NeighbourDirection.UpLeft; 
			}else if(deltaY < -0.1f){
				direction = NeighbourDirection.BottomLeft; 
			}else{
				direction = NeighbourDirection.Left; 
			}
		}
	}
}
public enum NeighbourDirection{
	UpLeft = 0,
	UpRight = 1,
	Left = 2,
	Right = 3,
	BottomLeft = 4,
	BottomRight = 5
}