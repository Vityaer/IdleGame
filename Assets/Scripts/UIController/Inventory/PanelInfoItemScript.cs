using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelInfoItemScript : MonoBehaviour{
   	[Header("PanelInfo Item")]
	public GameObject panelInfoItem;
	public Image imageInfoItem;
	public Text textNameItem, textTypeItem, textBonus;
	private bool isOpenInfoPanel = false;
	private Item selectItem = null;
	public GameObject btnAction, btnOpenInventory, btnDrop, btnClose; 
	private Button componentButtonAction, componentButtonDrop;
	private Text textButtonAction, textButtonDrop;

	void Awake(){
		GetComponents();
	}
	private CellItemHeroScript cellItem; 
	//Panel InfoAboutItem  
	public void OpenInfoAboutItem(Item item, CellItemHeroScript cellItem, bool onHero = false){
		if(componentButtonAction == null) GetComponents();
		componentButtonAction.onClick.RemoveAllListeners();
		componentButtonDrop.onClick.RemoveAllListeners();
		componentButtonAction.onClick.AddListener( Close );
		componentButtonDrop.onClick.AddListener( Close );
		this.cellItem = cellItem;
		btnOpenInventory.SetActive(onHero);
		btnAction.SetActive( (cellItem == null) ? false : true );
		if(onHero == false){
			componentButtonAction.onClick.AddListener(InventoryControllerScript.Instance.SelectItem);
			componentButtonDrop.onClick.AddListener(InventoryControllerScript.Instance.DropItem);
			textButtonDrop.text = "Выбросить";
			textButtonAction.text = "Снарядить";
		}else{
			componentButtonDrop.onClick.AddListener( () => cellItem.SetItem(null) );
			componentButtonDrop.onClick.AddListener( () => TrainCampScript.Instance.TakeOff(item) );
			textButtonDrop.text = "Снять";
		}
		selectItem = item;
		imageInfoItem.sprite = item.Image;
		textNameItem.text = item.Name;
		textTypeItem.text = item.Type.ToString();
		textBonus.text    = item.GetTextBonuses(); 
		panelInfoItem.SetActive(true);
		btnClose.SetActive(true);
		isOpenInfoPanel = true;
	}
	public void OpenInfoAboutItem(Resource res){
		imageInfoItem.sprite = res.Image;
		textNameItem.text    = res.Name.ToString();
		panelInfoItem.SetActive(true);
	}
	public void OpenInfoAboutItem(SplinterController splinterController){
		if(componentButtonAction == null) GetComponents();
		componentButtonAction.onClick.RemoveAllListeners();
		componentButtonDrop.onClick.RemoveAllListeners();
		componentButtonDrop.onClick.AddListener( Close );
		
		imageInfoItem.sprite = splinterController.splinter.Image;
		textNameItem.text    = splinterController.splinter.Name;
		textTypeItem.text    = "";
		textBonus.text       = "";
		componentButtonAction.onClick.AddListener(() => splinterController.GetReward());
		componentButtonDrop.onClick.AddListener(() => InventoryControllerScript.Instance.DropSplinter(splinterController));
		textButtonDrop.text = "Выбросить";
		btnAction.SetActive(true);
		panelInfoItem.SetActive(true);
		btnClose.SetActive(true);
		isOpenInfoPanel = true;
	}
	void GetComponents(){
		componentButtonAction = btnAction.GetComponent<Button>(); 
		componentButtonDrop   = btnDrop.GetComponent<Button>();
		textButtonAction      = btnAction.transform.Find("Text").GetComponent<Text>();
		textButtonDrop        = btnDrop.transform.Find("Text").GetComponent<Text>();
	}
	public void OpenInventory(){
		InventoryControllerScript.Instance.Open(cellItem.typeCell, cellItem);
		Close();
	}
	public void Close(){
		panelInfoItem.SetActive(false);
		btnClose.SetActive(false);
	}
}
