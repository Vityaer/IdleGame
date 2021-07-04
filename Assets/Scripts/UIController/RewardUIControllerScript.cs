using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUIControllerScript : MonoBehaviour{

	public List<InventoryCellControllerScript> cells = new List<InventoryCellControllerScript>();
	public Transform panelRewards;
	public GameObject btnAllReward;
	private CalculatedReward reward;
	public void SetReward(CalculatedReward reward, bool lengthReward = false){
		Debug.Log("open");
		this.reward  = reward;
		if(cells.Count == 0) GetCells();
		if(btnAllReward != null) btnAllReward.SetActive((reward.AllCount > 4) && (lengthReward == false));
		SetResource(reward.GetListResource);		
		SetItems(reward.GetItems, reward.GetListResource.Count);		
		SetSplinters(reward.GetSplinters, reward.GetListResource.Count + reward.GetItems.Count);		
		for(int i = reward.AllCount; i < cells.Count; i++) cells[i].OffCell();
	}
	private void SetResource(ListResource listResource){
		for(int i = 0; i < listResource.Count; i++)
			if(i < cells.Count)
				cells[i].SetItem(listResource.List[i]);
	}
	private void SetItems(List<ItemController> items, int startPos){
		for(int i = startPos; i < startPos + items.Count; i++)
			if(i < cells.Count)
				cells[i].SetItem(items[i - startPos]);
	}
	private void SetSplinters(List<SplinterController> splinters, int startPos){
		for(int i = startPos; i < startPos + splinters.Count; i++)
			if(i < cells.Count)
				cells[i].SetItem(splinters[i  - startPos]);
	}
    void GetCells(){
    	foreach(Transform cell in panelRewards){
			cells.Add(cell.GetComponent<InventoryCellControllerScript>());
		}
    }
    public void OpenAllReward(){ BoxAllRewards.Instance.ShowAll(this.reward); }
    public void CloseReward(){ gameObject.SetActive(false);}
    public void OpenReward(){gameObject.SetActive(true);}
}
