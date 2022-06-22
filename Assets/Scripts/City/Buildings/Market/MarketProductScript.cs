using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class MarketProductScript : MonoBehaviour{
	public ButtonCostScript buttonCost;
	[OdinSerialize] private MarketProduct marketProduct;
	public SubjectCellControllerScript cellProduct;
	public void SetData(MarketProduct<Resource> product){
		marketProduct = product;
		buttonCost.UpdateCost(product.cost, Buy);
		cellProduct.SetItem(product.subject);
	}
	public void SetData(MarketProduct<Item> product){
		marketProduct = product;
		buttonCost.UpdateCost(product.cost, Buy);
		cellProduct.SetItem(product.subject);
	}
	public void SetData(MarketProduct<Splinter> product){
		marketProduct = product;
		buttonCost.UpdateCost(product.cost, Buy);
		cellProduct.SetItem(product.subject);
	}
	private void UpdateUI(){

	}
    public void Buy(int count = 1){
		if((count + marketProduct.CountLeftProduct) > marketProduct.CountMaxProduct) count = marketProduct.CountMaxProduct - marketProduct.CountLeftProduct;
		marketProduct.GetProduct(count);
		if(marketProduct.CountLeftProduct == marketProduct.CountMaxProduct){
			buttonCost.Disable();
		}
	}
	public void Hide(){
		gameObject.SetActive(false);
	}
}
