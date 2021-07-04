using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHero : MonoBehaviour{
	private Transform _tr;
	public Transform tr {get{if(_tr == null) _tr = GetComponent<Transform>(); return _tr;}}
	public Side side;
	public int ID;
	public TypeLine typeLine;
	public int layer;

}
public enum Side{
	Left,
	Right
}
public enum TypeLine{
	First,
	Second
}

