using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationControllerScript : MonoBehaviour{
	public List<Location> locations = new List<Location>();
	private Location curLocation;
	public void OpenLocation(TypeLocation typeLocation){
		foreach(Location location in locations){
			if(location.type == typeLocation){
				curLocation = location;
				BackGroundControllerScript.Instance.OpenBackground(curLocation.backgroundForFight);
				break;
			}
		}
	}
	// public void CloseLocation(){
	// 	curLocation.backgroundForFight.SetActive(false);
	// }
	public Sprite GetBackgroundForMission(TypeLocation typeLocation){
		Sprite result = null;
		foreach(Location location in locations){
			if(location.type == typeLocation){
				result = location.backgroundForMission;
				break;
			}
		}
		return result;
	}
}

public enum TypeLocation{
	Forest,
	NightForest,
	Desert
}
[System.Serializable]
public class Location{
	public TypeLocation type;
	public GameObject backgroundForFight;
	public Sprite backgroundForMission;
}