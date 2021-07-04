using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : Building{

	public Transform showcase;
	[Header("Products")]
	public List<MarketProduct> productsForSale = new List<MarketProduct>();
	private List<ProductScript> productControllers = new List<ProductScript>(); 
	protected override void OpenPage(){
		if(productControllers.Count == 0) GetCells();
		for(int i=0; i < productsForSale.Count; i++){
			productControllers[i].UpdateUI(productsForSale[i] as MarketProduct);
		}
		for(int i = productsForSale.Count; i < productControllers.Count; i++){
			productControllers[i].Clear();
		}
	}
	private void GetCells(){
		foreach(Transform child in showcase)
			productControllers.Add(child.GetComponent<ProductScript>());
	}
}

