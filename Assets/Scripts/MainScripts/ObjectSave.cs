using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ObjectSave{
	[System.Serializable]
	public class HeroSave{
		public int ID;
		public int IDFromServer = 0; 
		public string name;
		public int level;
		public CostumeSave costume = new CostumeSave();
		public void NewData(InfoHero hero){
			name = hero.generalInfo.Name; 
			ID = hero.generalInfo.idHero;
			level = hero.generalInfo.Level;
			costume.NewData(hero.CostumeHero);
		}
	}
	[System.Serializable]
	public class CostumeSave{
		public List<int> listID = new List<int>();
		public void NewData(CostumeHeroControllerScript costume){
			listID.Clear();
			foreach(Item item in costume.items){
				listID.Add(item.ID);
			}
		}
	}
	[System.Serializable]
	public class InventorySave{
		public List<ItemSave> listItem = new List<ItemSave>();
		public List<SplinterSave> listSplinter = new List<SplinterSave>();
		public InventorySave(Inventory inventory){
			foreach(ItemController item in inventory.items)
				listItem.Add(new ItemSave(item));
			foreach(SplinterController splinter in inventory.splinters)
				listSplinter.Add(new SplinterSave(splinter));	
		} 
	}
	[System.Serializable]
	public class ItemSave{
		public int ID;
		public int Amount;
		public ItemSave(ItemController itemController){
			this.ID = itemController.item.ID;
			this.Amount = itemController.Amount;
		}
	}
	[System.Serializable]
	public class SplinterSave{
		public int ID;
		public int Amount;
		public SplinterSave(SplinterController splinterController){
			this.ID = splinterController.splinter.ID;
			this.Amount = splinterController.splinter.Amount;
		}
	}

//Main
	[System.Serializable]
	public class PlayerSaveObject{
		public ListResource storeResources = new ListResource();
		public List<Task> listTasks = new List<Task>(); 
		public AllRequirement allRequirement = new AllRequirement();
	}
	[System.Serializable]
	public class CitySaveObject{
		public TimeManagement timeManagement = new TimeManagement();
		public IndustrySave industry = new IndustrySave();
		public MallSave mall = new MallSave();
		public TaskGiverBuilding taskGiverBuilding = new TaskGiverBuilding();
		public BuildingWithFightTeams challengeTowerBuilding = new BuildingWithFightTeams();
		public BuildingWithFightTeams mainCampaignBuilding = new BuildingWithFightTeams();
	}	
//Mines
	[System.Serializable]
	public class IndustrySave{
		public List<MineBuildSave> listAdminMine = new List<MineBuildSave>();
		public List<MineSave> listMine = new List<MineSave>();
		public void SaveMine(MineControllerScript mineController){
			MineSave mineSave = listMine.Find(x => x.ID == mineController.ID);
			if(mineSave != null) {
				mineSave.ChangeInfo(mineController);
			}else{
				listMine.Add(new MineSave(mineController));
			}
		}
	}
	[System.Serializable]
	public class MineBuildSave{
		public int ID;
		public TypeMine typeMine;
		public int level;
		public MineBuildSave(MineControllerScript mineController){
			this.ID = mineController.ID;
			this.typeMine = mineController.GetMine.type;
			this.level = mineController.GetMine.level;
		}
		public MineBuildSave(int ID, TypeMine typeMine){
			this.ID = ID;
			this.typeMine = typeMine;
			this.level = 1;
		}
	}
	[System.Serializable]
	public class MineSave : MineBuildSave{
		public ResourceSave store;
		[SerializeField] private string previousDateTime;
		public DateTime PreviousDateTime{get => FunctionHelp.StringToDateTime(previousDateTime); set => previousDateTime = value.ToString();}
		public MineSave(MineControllerScript mineController) : base(mineController){
			ChangeInfo(mineController);
		}
		public void ChangeInfo(MineControllerScript mineController){
			this.ID = mineController.ID;
			this.level = mineController.GetMine.level; 
			this.typeMine = mineController.GetMine.type;
			this.store = new ResourceSave(mineController.GetMine.GetStore);
			this.previousDateTime = mineController.GetMine.previousDateTime.ToString();
		}
		public MineSave(int ID, TypeMine typeMine) : base(ID, typeMine){
			this.ID = ID;
			this.level = 1;
			this.store = new ResourceSave(MinesScript.GetTypeResourceFromTypeMine(typeMine));
			this.typeMine = typeMine;
			this.previousDateTime = DateTime.Now.ToString();
		}
	}

//Tasks
	[System.Serializable]
	public class TaskGiverBuilding{
		public List<Task> tasks = new List<Task>();
	}
	[System.Serializable]
	public class AllRequirement{
		public List<RequirementSave> saveMainRequirements = new List<RequirementSave>();
		public List<RequirementSave> saveEveryTimeTasks = new List<RequirementSave>();
		public SimpleBuildingSave eventAgentProgress;
	}	
	[System.Serializable]
	public class RequirementSave{
		public int ID;
		public int currentStage;
		public BigDigit progress;
		public RequirementSave(Requirement requirement){
			ChangeData(requirement);
		}
		public void ChangeData(Requirement requirement){
			this.ID = requirement.ID;
			this.currentStage = requirement.CurrentStage;
			this.progress = requirement.Progress;
		}
	}
//Markets
	[System.Serializable]
	public class MallSave{
		public List<MarketSave> markets = new List<MarketSave>();
	}
	[System.Serializable]
	public class MarketSave{
		public TypeMarket typeMarket;
		public List<MarketProductSave> products = new List<MarketProductSave>();
	}
	[System.Serializable]
	public class MarketProductSave{
		public int ID;
		public int countSell;
		public MarketProductSave(int ID, int countSell){
			this.ID = ID;
			this.countSell = countSell;
		}
		public void UpdateData(int newCountSell){
			this.countSell = newCountSell;
		}
	}
//Dates
	[System.Serializable]
	public class TimeManagement{
		public List<DateRecord> records = new List<DateRecord>(); 
	}
	[System.Serializable]
	public class DateRecord{
		public TypeDateRecord type;
		[SerializeField] private string date;
		public DateTime Date{get => FunctionHelp.StringToDateTime(date); set => date = value.ToString(); }
		public DateRecord(TypeDateRecord type, DateTime date){
			this.type = type;
			this.date = date.ToString();
		}
	}
//Resource
	[System.Serializable]
	public class ResourceSave{
		public TypeResource type;
		public BigDigit amount;
		public ResourceSave(Resource res){
			this.type = res.Name;
			this.amount = res.Amount;
		}
		public ResourceSave(TypeResource typeResource){
			this.type = typeResource;
			this.amount = new BigDigit();
		}
	}
//Simple building save
	[System.Serializable]
	public class SimpleBuildingSave{
		public Dictionary<string, int> intRecords     = new Dictionary<string, int>();
		public Dictionary<string, string> dateRecords = new Dictionary<string, string>();
		public void SetRecordInt(string name, int num){
			AddRecordInDictionary<int>(intRecords, name, num);
		}
		public void SetRecordDate(string name, DateTime date){
			AddRecordInDictionary<string>(dateRecords, name, date.ToString());
		}
		private void AddRecordInDictionary<T>(Dictionary<string, T> dictionary, string name, T newValue){
			if(dictionary.ContainsKey(name)){
				dictionary[name] = newValue;
			}else{
				dictionary.Add(name, newValue);
			}
		}
		public int GetRecordInt(string name){
			int result = 0; 
			if(intRecords.ContainsKey(name)) result = intRecords[name];
			return result;
		}
		public DateTime GetRecordDate(string name){
			string strDate = string.Empty;
			if(dateRecords.ContainsKey(name)) strDate = dateRecords[name];
			return FunctionHelp.StringToDateTime(strDate);
		}
	}
	[System.Serializable]
	public class BuildingWithFightTeams : SimpleBuildingSave{
		public Dictionary<string, TeamFight> teamRecords = new Dictionary<string, TeamFight>();
		public void SetRecordTeam(string name, TeamFight newTeam){
			if(teamRecords.ContainsKey(name)){
				teamRecords[name] = newTeam.Clone();
			}else{
				teamRecords.Add(name, newTeam.Clone());
			}
		} 
	}
	[System.Serializable]
	public class TeamFight{
		public List<int> listID = new List<int>(){-1, -1, -1, -1, -1, -1};
		public int petID = -1;
		public TeamFight(List<int> newListID, int newPetId = -1){
			for(int i = 0; i < newListID.Count; i++)
				this.listID[i] = newListID[i];
			this.petID = newPetId;
		}
		public void ChangeIdHero(InfoHero oldHero, InfoHero newHero){
			int pos = listID.FindIndex(x => x == oldHero.generalInfo.IDCreate);
			if(pos >= 0) listID[pos] = newHero.generalInfo.IDCreate;
		}
		public void AddHero(int pos, InfoHero hero){ listID[pos] = hero.generalInfo.IDCreate; }
		public void RemoveHero(InfoHero hero){
			int pos = listID.FindIndex(x => x == hero.generalInfo.IDCreate);
			if(pos >= 0) listID[pos] = hero.generalInfo.IDCreate; 
		}
		public void SetNewPetId(int newId){ petID = newId; }
		public TeamFight Clone(){ return new TeamFight(listID, petID); }
	}
}
