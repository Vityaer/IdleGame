using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplinterController : ItemController, VisualAPI, ICloneable{

	private Splinter _splinter;
	public Splinter splinter{get => _splinter;}
	public void ClickOnItem(){
		InventoryControllerScript.Instance.OpenInfoItem(this);
	}
	public SplinterController(Splinter splinter, int amount) : base(splinter, amount){
		this._splinter = splinter;
		this.amount = amount;
	}
	public SplinterController() : base(){
		_splinter = null;
	}
	public void SetUI(ThingUIScript UI){
		this.UI = UI;
		UpdateUI();
	}
	public void UpdateUI(){
		UI?.UpdateUI(this);
	}
	public void ClearUI(){
		this.UI = null;
	}
	public VisualAPI GetVisual(){
		return (this as VisualAPI);
	}
	public void GetReward(){
		amount -= splinter.GetReward(Amount);
		if(Amount == 0){
			InventoryControllerScript.Instance.DropSplinter(this);
		}
	}
	public object Clone(){
        return new SplinterController  { 	_splinter  = this._splinter,
        							 	    amount        = this.amount
        							};				
    }
}
