using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
[System.Serializable]
public class Reward{
    [SerializeField] private ListResource resources = new ListResource();
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private List<Splinter> splinters = new List<Splinter>();

	public int Count{
        get{
            if(generalList.Count == 0) PrepareRewardForShow();
            return generalList.Count;
        }
    }
    private List<BaseObject> generalList = new List<BaseObject>();
    public List<BaseObject> GetList{
        get{
            if(generalList.Count == 0) PrepareRewardForShow();
            return generalList;
        }
    }
	public Reward Clone(){
        ListResource listRes = (ListResource) resources.Clone();
        List<Item> cloneItems = new List<Item>();
        List<Splinter> cloneSplinters = new List<Splinter>();
        foreach(Item item in this.items){ cloneItems.Add((Item) item.Clone());}
        foreach(Splinter splinter in this.splinters){ cloneSplinters.Add((Splinter) splinter.Clone());}
        return new Reward(listRes, cloneItems, cloneSplinters);			
    }
    public Reward(ListResource resources, List<Item> items, List<Splinter> splinters){
        this.resources = resources;
        this.items = items;
        this.splinters = splinters;
    }
    public Reward(){}
    private void PrepareRewardForShow(){
        for(int i = 0; i < resources.List.Count; i++) generalList.Add(resources.List[i]);
        for(int i = 0; i < items.Count; i++) generalList.Add(items[i]);
        for(int i = 0; i < splinters.Count; i++) generalList.Add(splinters[i]);

    }
    public ListResource GetListResource{get => resources;}
	public List<Item> GetItems{get => items;}
	public List<Splinter> GetSplinters{get => splinters;}
}

