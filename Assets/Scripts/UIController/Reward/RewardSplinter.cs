using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardSplinter : ICloneable{
	public int ID;
	private static SplintersList splintersList;
    private Splinter _splinter;
	public Splinter splinter{get{
			if(splintersList == null) splintersList = Resources.Load<SplintersList>("Items/ListSplinters"); 
			if((_splinter == null)  || (_splinter.ID == 0)) _splinter = splintersList?.GetItem(ID);
			if((_splinter == null)  || (_splinter.ID == 0)) {_splinter = splintersList?.GetItem(2101); Debug.Log("осколок с id = 0");}
			return _splinter;
		}}
	public TypeIssue typeIssue;
	public float posibility = 100f;
	public int min, max, count;
	private SplinterController splinterController = new SplinterController();
	public SplinterController GetReward(int count = 1){
		int amount = 0;
		switch(typeIssue){
			case TypeIssue.Necessarily:
				amount = this.count;
				break;
			case TypeIssue.Perhaps:
				for(int i=0; i < count; i++)
					if(Random.Range(0f, 100f) < posibility)
						amount++;
				break;
			case TypeIssue.Range:
				float rand = 0f;
				amount = 0;
				for(int i=0; i < count; i++){
					rand = Random.Range(0f, 100f);
					amount += (int) Mathf.Round(((max - min)*rand/100f + min)/100f);
				}
				break;
			default:
				amount = count;
				break;			
		}
		if((amount > 0) && (splinter != null)) splinterController = new SplinterController(splinter, amount);
		return splinterController;
	}
	public object Clone(){
        return new RewardSplinter  { 	ID            = this.ID,
        							 	_splinter     = this._splinter,
        							 	typeIssue     = this.typeIssue,
        							 	posibility    = this.posibility,
        							 	min  = this.min,
        							 	max  = this.max,
        							 	count  = this.count
        							};				
    }
}
