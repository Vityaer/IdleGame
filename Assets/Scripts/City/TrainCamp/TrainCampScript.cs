using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainCampScript : MonoBehaviour{

	private Canvas trainCanvas;

	public List<InfoHero> listHeroes = new List<InfoHero>();
	public int numSelectHero = 0;
	private InfoHero hero;
	[Header("Controller")]
	public GameObject btnToLeftList;
	public GameObject btnToRightList;
	public Button btnLevelUP;
	public Text textLevelUp;
	public ListCardOnWarTableScript listCardPanel;
	public ListMyHeroesControllerScript ListCard;
	public CostLevelUp costLevelObject;

	[Header("Information")]
	public Image imageHero;
	public Text textLevel;
	public Text textNameHero;
	public Text textHP;
	public Text textAttack;
	public Text textArmor;
	public Text textInitiative;
	public Text textStrengthHero;

	[Header("Items")]
	public List<CellItemHeroScript> CellsForItem = new List<CellItemHeroScript>(); 
	private void SelectHero(int num){
		if(num < 0) num = 0;
		if(num >= listHeroes.Count) num = listHeroes.Count - 1;
		numSelectHero = num;
		hero = listHeroes[numSelectHero];
		UpdateInfoAbountHero();
	}
	[Header("Skills")]
	public SkillUIControllerScript skillController;
	[Header("Costs")]
	public CostUIListScript costController;

	public void UpdateInfoAbountHero(){
		imageHero.sprite    = hero.generalInfo.ImageHero;
		textNameHero.text   = hero.generalInfo.Name;
		UpdateTextAboutHero();
		foreach(CellItemHeroScript cell in CellsForItem){
			cell.Clear();
			cell.SetItem(hero.CostumeHero.GetItem(cell.typeCell));
		}
		CheckResourceForLevelUP();
	} 
	public void UpdateTextAboutHero(){
		textLevel.text      =  hero.generalInfo.Level.ToString();
		textHP.text         = (hero.GetCharacteristic("HP")        ).ToString();
		textAttack.text     = (hero.GetCharacteristic("Attack")    ).ToString();
		textArmor.text      = (hero.GetCharacteristic("Armor")     ).ToString();
		textInitiative.text = (hero.GetCharacteristic("Initiative")).ToString();
		textStrengthHero.text  = hero.GetStrength.ToString(); 
		hero.PrepareSkillLocalization();
		skillController.ShowSkills(hero.skills);
		costController.ShowCosts( costLevelObject.GetCostForLevelUp(hero.generalInfo.Level) );
	}
	private void CheckResourceForLevelUP(){
		btnLevelUP.interactable = PlayerScript.Instance.CheckResource( costLevelObject.GetCostForLevelUp(hero.generalInfo.Level) );
	}

	
//API
	public void TakeOff(Item item){
		hero.CostumeHero.TakeOff(item);
		UpdateTextAboutHero();
	}
	public void SelectHero(CardScript card){
		numSelectHero = listHeroes.FindIndex(x => x == card.hero);
		OpenTrainCamp();
	}
	public InfoHero ReturnSelectHero(){
		return listHeroes[numSelectHero];
	}
	public void LevelUpHero(){
		PlayerScript.Instance.SubtractResource( costLevelObject.GetCostForLevelUp(hero.generalInfo.Level) );
		hero.LevelUP();
		UpdateInfoAbountHero();
	}
	public void NextHero(){
		SelectHero(numSelectHero + 1);
	}
	public void PreviousHero(){
		SelectHero(numSelectHero - 1);
	}	
	public void OpenTrainCamp(){
		isOpen = true;
		if(listHeroes.Count == 0) PlayerScript.Instance.GetListHeroesWithObserver(ref listHeroes, OnChangeListHeroes);
		if(listHeroes.Count > 0){
			SelectHero(numSelectHero);
		}
		ListCard.Close();
		MenuControllerScript.Instance.CloseMainPage();
		trainCanvas.enabled = true;
	}
	public void CloseTrainCamp(){
		isOpen = false;
		trainCanvas.enabled = false;
		MenuControllerScript.Instance.OpenMainPage();
		PlayerScript.Instance.SaveGame();
	} 

	private Vector3 startPosition;
	private Vector3 endPosition;
	private float dragDistance;
	private bool isOpen = false;
	void Update(){
		if(isOpen == true){
			if (Input.GetMouseButtonDown(0)){
				startPosition  = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(0)){
				endPosition  = Input.mousePosition;
				if(Mathf.Abs(endPosition.y - startPosition.y) < dragDistance){
					if(endPosition.x - startPosition.x > dragDistance){
						PreviousHero();
					}else if(startPosition.x - endPosition.x > dragDistance){
						NextHero();
					}
				}
			}
		}
	}

	private static TrainCampScript instance;
	public static TrainCampScript Instance{get => instance;}
	void Awake(){
		instance = this;
		trainCanvas = GetComponent<Canvas>();
	} 
	void Start(){
		listCardPanel.RegisterOnSelect(SelectHero);
		dragDistance = Screen.width*30/100;
	}
	void OnChangeListHeroes(InfoHero hero){
		listCardPanel.ChangeList(hero);
	}
}
