using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerScript : MonoBehaviour{
	[SerializeField] private List<InfoHero> listHeroes = new List<InfoHero>();
	public Player player;
	private static PlayerScript instance;
	public  static PlayerScript Instance{get => instance;}
	void Awake(){
		instance = this;
	}
	void Start(){
		SaveLoadControllerScript.LoadGame(player.PlayerGame);
		SaveLoadControllerScript.LoadListHero(listHeroes);
		if(listHeroes.Count == 0) {
			PlayerScript.Instance.AddResource(new Resource(TypeResource.SimpleHireCard, 50, 0));
			PlayerScript.Instance.AddResource(new Resource(TypeResource.SimpleTask, 50, 0));
			PlayerScript.Instance.AddResource(new Resource(TypeResource.SpecialTask, 50, 0));
			PlayerScript.Instance.AddResource(new Resource(TypeResource.SpecialHireCard, 50, 0));
		}else{
			OnChangeListHeroes(null);
		}
		CampaignScript.Instance.OpenMissions(player.PlayerGame.CampaignMissionNumComplete);
		UpdateAllResource();
		flagLoadedGame = true;
	}
	void OnDestroy(){
		SaveGame();
	}
	public bool flagLoadedGame = false;
	public void SaveGame(){
		if(flagLoadedGame){
			player.PlayerGame.PrepareForSave();
			SaveLoadControllerScript.SaveGame(player.PlayerGame);
			SaveLoadControllerScript.SaveListHero(listHeroes);
		}
	}
	void OnApplicationPause(bool pauseStatus){
		#if UNITY_ANDROID && !UNITY_EDITOR
		SaveGame();
		#endif
	}
//API List Heroes
	public Action<InfoHero> observerChangeListHeroes;
	public void GetListHeroesWithObserver(ref List<InfoHero> listHeroes, Action<InfoHero> d){
		observerChangeListHeroes += d;
		listHeroes = this.listHeroes;
	}
	public List<InfoHero> GetListHeroes{get => listHeroes;}
	public void AddHero(InfoHero newHero){
		listHeroes.Add(newHero);
		OnChangeListHeroes(newHero);
	}
	public void RemoveHero(InfoHero removeHero){
		bool flag = listHeroes.Remove(removeHero);
		if(flag) Debug.Log("герой успешно удалён!");
		OnChangeListHeroes(removeHero);
	}	
	private void OnChangeListHeroes(InfoHero hero){
		if(observerChangeListHeroes != null)
			observerChangeListHeroes(hero);
	}
//API mine
	public Mine LoadMine(int ID){
		return player.PlayerGame.LoadMine(ID);
	}
	public DateTime GetPreviousDateTimeForMine(int ID){
		return FunctionHelp.StringToDateTime(player.PlayerGame.GetPreviousDateTimeForMine(ID));
	}
	public void SaveMine(Mine mine){
		player.PlayerGame.SaveMine(mine);
		SaveGame();
	}
//API resources	
	public void AddReward(CalculatedReward reward){
		Debug.Log("add reward");
		PlayerScript.Instance.AddResource(reward.GetListResource);
		InventoryControllerScript.Instance.AddItems(reward.GetItems);
		InventoryControllerScript.Instance.AddSplinters(reward.GetSplinters);
	}
	public bool CheckResource(Resource res){
		return CheckResource(new ListResource(res));
	}
	
	public bool CheckResource(ListResource listResource){
		return player.PlayerGame.StoreResources.CheckResource(listResource);
	}
	public void AddResource(params Resource[] resources){
		for(int i = 0; i < resources.Length; i++)
			AddResource(new ListResource(resources[i]));
	}
	public void AddResource(ListResource listResource){
		player.PlayerGame.StoreResources.AddResource(listResource);
		UpdateAllResource();
	}

	public void SubtractResource(params Resource[] resources){
		for(int i = 0; i < resources.Length; i++)
			SubtractResource(new ListResource(resources[i]));
	}
	public void SubtractResource(ListResource listResource){
		player.PlayerGame.StoreResources.SubtractResource(listResource);
		UpdateAllResource();
	}
	public Resource GetResource(TypeResource name){
		return player.PlayerGame.StoreResources.GetResource(name);
	}

	public void ClearAllResource(){
		player.PlayerGame.StoreResources.Clear();
		UpdateAllResource();
	}

	List<ObserverResource> observersResource = new List<ObserverResource>();
	public void UpdateAllResource(){
		UpdateResource(player.PlayerGame.StoreResources);
	}
	public void UpdateResource(ListResource listResources){
		foreach(Resource res in listResources.List){
			foreach(ObserverResource obs in observersResource){
				if(res.Name == obs.typeResource){
					obs.ChangeResource(res);
					break;					
				}
			}
		}
	}

	public void RegisterOnChangeResource(Action<Resource> d, TypeResource type){
		bool findObserver = false;
		ObserverResource observer = new ObserverResource(TypeResource.Gold);
		foreach(ObserverResource obs in observersResource){
			if(obs.typeResource == type){
				findObserver = true;
				observer = obs;
				break;
			}
		}
		if(findObserver == false) {
			observer = new ObserverResource(type);
			observersResource.Add(observer);
		}	
		observer.RegisterOnChangeResource(d);
	}
	public void UnRegisterOnChangeResource(Action<Resource> d, TypeResource type){
		foreach(ObserverResource obs in observersResource){
			if(obs.typeResource == type){
				obs.UnRegisterOnChangeResource(d);
				break;
			}
		}
	}
	void OnApplicationFocus(bool hasFocus){
		#if UNITY_ANDROID && !UNITY_EDITOR
        SaveGame();
        #endif
    }
}

