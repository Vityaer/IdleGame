using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiteDependFromCount{
	[SerializeField] private Sprite sprite;
	public Sprite GetSprite{get => sprite;}
	[SerializeField] private int requireCount;
	public int RequireCount{get => requireCount;}

}
[System.Serializable]
public class ListSpriteDependFromCount{
    [SerializeField] private List<SpiteDependFromCount> listSpriteGoldHeap = new List<SpiteDependFromCount>();
    public Sprite GetSprite(int tact){
    	Sprite result = null;
    	int maxCount = -1; 
    	for(int i = 0; i < listSpriteGoldHeap.Count; i++){
    		if((tact >= listSpriteGoldHeap[i].RequireCount) && (maxCount < listSpriteGoldHeap[i].RequireCount)){
    			result = listSpriteGoldHeap[i].GetSprite;
    			maxCount = listSpriteGoldHeap[i].RequireCount;
    		}
    	}
    	return result;
    }
}