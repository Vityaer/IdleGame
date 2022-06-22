using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketResourceScript : MonoBehaviour{
	[Header("Resource")]
	public List<MarketProduct<Resource>> resources = new List<MarketProduct<Resource>>();
	public MarketProduct<Resource> GetProductFromTypeResource(TypeResource name){
		return resources.Find(x => (x.subject as Resource).Name == name);
	}
	private static MarketResourceScript instance; 
	public static MarketResourceScript Instance{get => instance;} 
	void Awake(){ instance = this; }
}
