using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardResource : ICloneable{
	[SerializeField]private Resource res;
	public Resource GetRes{get => res;}
	[SerializeField]private TypeIssue typeIssue;
	[SerializeField]private float posibility = 100f;
	[SerializeField]private float min, max, count;
	private Resource resource = null;
	public Resource GetReward(int count = 1){
		resource = new Resource(res.Name, 0, 0);
		switch(typeIssue){
			case TypeIssue.Necessarily:
				resource = res * count;
				break;
			case TypeIssue.Perhaps:
				for(int i=0; i < count; i++)
					if(Random.Range(0f, 100f) < posibility)
						resource.AddResource(res);
				break;
			case TypeIssue.Range:
				float rand = 0f, current = 0f;
				for(int i=0; i < count; i++){
					rand = Random.Range(0f, 100f);
					current = ((max - min)*rand/100f + min)/100f;
					resource.AddResource(res * current);
				}
				break;
			default:
				resource = res * count;
				break;			
		}
		return resource;
	}
	public object Clone(){
        return new RewardResource  {
        							 	res     = this.res,
        							 	typeIssue     = this.typeIssue,
        							 	posibility    = this.posibility,
        							 	min  = this.min,
        							 	max  = this.max
        							};				
    }
}
