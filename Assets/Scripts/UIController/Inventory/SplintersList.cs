using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSplinter", menuName = "Custom ScriptableObject/Splinter", order = 52)]

[System.Serializable]
public class SplintersList : ScriptableObject{
	[SerializeField]
	private List<Splinter> list = new List<Splinter>();
	public Splinter GetItem(int ID){
		if(ID > 1000){
			return new Splinter(ID);
		}else{
			return list.Find(x => (x.ID == ID));
		}
	}

}
