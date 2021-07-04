using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectSave{
	[System.Serializable]
	public class HeroSave{
		public int ID;
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
			this.Amount = splinterController.Amount;
		}
	}
	[System.Serializable]
	public class MineSave{
		public int ID;
		public int Level;
		public string previousDateTime;
		public MineSave(Mine mine){
			this.ID = mine.ID;
			this.Level = mine.level; 
			this.previousDateTime = mine.previousDateTime.ToString();
		}
		public void ChangeInfo(Mine mine){
			this.ID = mine.ID;
			this.Level = mine.level; 
			this.previousDateTime = mine.previousDateTime.ToString();
		}
	}
}
