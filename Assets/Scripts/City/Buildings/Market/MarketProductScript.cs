using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketProductScript : ProductScript{
	public  int countLeftProduct;
	public ButtonCostScript buttonCost;
	private MarketProduct marketProduct;
	public override void UpdateUI(Product product){
		this.marketProduct = product as MarketProduct;
		countLeftProduct   = marketProduct.countMaxProduct;
		buttonCost.UpdateCost(marketProduct.cost, Buy);
		UIItem.UpdateUI(this.marketProduct);
	}
    public void Buy(int count = 1){
		if(PlayerScript.Instance.CheckResource( marketProduct.cost )){
			PlayerScript.Instance.SubtractResource( marketProduct.cost );
			if(count > countLeftProduct) count = countLeftProduct;
			countLeftProduct -= count;
			GetProduct();
			if(countLeftProduct == 0){
				buttonCost.Disable();
			}
		}
	}
	public override void Clear(){
		countLeftProduct = 0;
		base.Clear();
	}
}

[System.Serializable]
public class MarketProduct : Product{
	public Resource cost;
	public  int countMaxProduct;
}
public enum CycleRecover{
	Day,
	Week,
	Never
}
