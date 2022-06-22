using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class MarketScript : Building{

	public Transform showcase;
	[Header("Products")]
	[OdinSerialize] private List<MarketProduct> productsForSale = new List<MarketProduct>();
	[SerializeField] private List<MarketProductScript> productControllers = new List<MarketProductScript>(); 
	protected override void OnStart(){
		if(productControllers.Count == 0) GetCells();
	}
	protected override void OpenPage(){
		for(int i=0; i < productsForSale.Count; i++){
			switch(productsForSale[i]){
				case MarketProduct<Resource> product:
					productControllers[i].SetData(product);
					break;
				case MarketProduct<Item> product:
					productControllers[i].SetData(product);
					break;
				case MarketProduct<Splinter> product:
					productControllers[i].SetData(product);
					break;		

			}
		}
		for(int i = productsForSale.Count; i < productControllers.Count; i++){
			productControllers[i].Hide();
		}
	}
	private void GetCells(){
		foreach(Transform child in showcase)
			productControllers.Add(child.GetComponent<MarketProductScript>());
	}

	[Button]
	 public void AddResource(){ productsForSale.Add(new MarketProduct<Resource>()); }
	[Button]
	 public void AddSplinter(){ productsForSale.Add(new MarketProduct<Splinter>()); }
	[Button]
	public void AddItem(){ productsForSale.Add(new MarketProduct<Item>()); }
}

