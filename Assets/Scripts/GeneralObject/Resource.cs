using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TypeResource{
	Gold = 0,
	Diamond = 1,
	ContinuumStone = 2,
	SimpleHireCard = 3,
	SpecialHireCard = 4,
	ForceStone = 5,
	TicketOnArena = 6,
	GenePet = 7,
	DopePet = 8,
	CoinFortune = 9,
	Exp = 10,
	SimpleTask = 11,
	SpecialTask = 12,
	TicketGrievance = 13,
	SimpleStoneReplacement = 14,
	RaceHireCard = 15
}


[System.Serializable]
public class Resource : ICloneable, VisualAPI{
	public TypeResource Name;
	[SerializeField]
	protected float count = 0;
	public float Count{ get => count; set => count = value; }
	[SerializeField]
	protected int e10 = 0;
	public int E10{get => e10;  set => e10 = value;}
	public Resource(){
		Name  = TypeResource.Gold;
		count = 0;
		e10   = 0;
	}
	public Resource(TypeResource name, float count = 0f, int e10 = 0){
		this.Name  = name;
		this.count = count;
		this.e10   = e10;
		NormalizeResource();
	}
	
	private void NormalizeResource(){
		while(this.count > 1000){
			this.e10   += 3;  
			this.count *= 0.001f;
		}
		while ((this.count < 1) && (e10 > 0)){
			this.e10 -= 3;
			this.count *= 1000f;
		}
	}
//API
	public override string ToString(){
		return FunctionHelp.BigDigit(this.count, this.E10);
	}
	public bool CheckCount(int count, int e10){
		bool result = false;
		if(this.e10 != e10){
			if(this.e10 > e10){
				if(this.count * (float) Mathf.Pow(10, this.e10 - e10) >= count)
					result = true;
			}else{
				if(this.count >= count * (float) Mathf.Pow(10, e10 - this.e10))
					result = true;
			}
		}else{
			if(this.count >= count){
				result = true;
			}
		}
		return result;
	}
	public void AddResource(float count, float e10){
		if((count > 0) && (e10 >= 0)){
			this.count += count * (float) Mathf.Pow(10f, e10 - this.e10);
			NormalizeResource();
		}
	}
	public void SubtractResource(float count, float e10){
		if((count > 0) && (e10 >= 0)){
			this.count -= count * (float) Mathf.Pow(10f, e10 - this.e10); 
			NormalizeResource();
		}
	}
	public bool CheckCount(Resource res){
		bool result = false;
		if(this.e10 != res.E10){
			if(this.e10 > res.E10){
				if(this.count * (float) Mathf.Pow(10, this.e10 - res.E10) >= res.Count)
					result = true;
			}else{
				if(this.count >= res.Count * (float) Mathf.Pow(10, res.E10 - this.e10))
					result = true;
			}
		}else{
			if(this.count >= res.Count){
				result = true;
			}
		}
		return result;
	}
	public void AddResource(Resource res){
			this.count += res.Count * (float) Mathf.Pow(10f, res.E10 - this.E10);
			NormalizeResource();
	}
	public void SubtractResource(Resource res){
		this.count -= res.Count * (float) Mathf.Pow(10f, res.E10 - this.E10); 
		NormalizeResource();
	}
	public void Clear(){
		this.count = 0;
		this.e10   = 0;
	}
	public void ClearUI(){
		this.UI = null;
	}
	public VisualAPI GetVisual(){
		return (this as VisualAPI);
	}

//Operators
	public static Resource operator* (Resource res, float k){
		Resource result = new Resource(res.Name, Mathf.Ceil(res.Count * k), res.E10);
		return result;
	}	
//Image
	private static Sprite[] spriteAtlas;   
	private Sprite image;
	public Sprite sprite{ 
							get{ 
								if(image == null){
									if(spriteAtlas == null) spriteAtlas = Resources.LoadAll<Sprite>("UI/GameImageResource/Resources");
									for(int i=0; i < spriteAtlas.Length; i++){
										if((Name.ToString()).Equals(spriteAtlas[i].name)){
											image = spriteAtlas[i];
											break;
										}
									}
								}
								return image;
							}
						}
	public object Clone(){
        return new Resource  { 
        	Name = this.Name,
        	Count = this.count,
        	E10  = this.e10
        };
    }   
//UI
	public void ClickOnItem(){ InventoryControllerScript.Instance.OpenInfoItem(this); }
	protected ThingUIScript UI;
	public void SetUI(ThingUIScript UI){
		this.UI = UI;
		UpdateUI();
	}
	public void UpdateUI(){
		UI?.UpdateUI(sprite, Rare.C, ToString());
	}

}

public class ObserverResource{
	public TypeResource typeResource;
	public Action<Resource> delObserverResource;
	public ObserverResource(TypeResource type){
		typeResource = type;
	}

	public void RegisterOnChangeResource(Action<Resource> d){
		delObserverResource += d;
	}
	public void UnRegisterOnChangeResource(Action<Resource> d){
		delObserverResource -= d;
	}
	public void ChangeResource(Resource res){
		if(delObserverResource != null)
			delObserverResource(res);
	}
}
