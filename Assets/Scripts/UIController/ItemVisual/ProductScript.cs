using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProductScript : MonoBehaviour{
    [Header("Product")]
	protected Product product;
	[Header("UI")]
	public ThingUIScript UIItem;

	public virtual void UpdateUI(Product product){
		Debug.Log("product script");
		this.product       = product;
		UIItem.UpdateUI(this.product);
	}
	public virtual void Clear(){
		product = null;
		gameObject.SetActive(false);
	}
	
	public void GetProduct(){
		switch(product.typeProduct){
			case TypeProduct.Resource:
				PlayerScript.Instance.AddResource( product.rewardResource );
				break;
			case TypeProduct.Item:	
				break;
			case TypeProduct.Splinter:
				break;
		}
	}
}

