using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardScript : MonoBehaviour{

	public InfoHero hero;

	[SerializeField] private Image imageUI;
	[SerializeField] private Text  levelUI;
	[SerializeField] private Image panelSelect;
	public RatingHeroScript ratingController;
	private ListCardOnWarTableScript listCardController;
	void Start(){
		UpdateUI();
	}

	public void UpdateUI(){
		if(hero != null){
			imageUI.sprite       = hero.generalInfo.Prefab?.GetComponent<SpriteRenderer>().sprite;
			levelUI.text         = hero.generalInfo.Level.ToString();
			ratingController.ShowRating(hero.generalInfo.ratingHero); 
		}else{
			listCardController?.RemoveCards(new List<CardScript>(){this});
		}
	}

	public bool selected = false;

	public void ClickOnCard(){
		if(selected == false){
			listCardController.SelectCard(this);
		}else{
			listCardController.UnSelectCard(this);
		}
	}
	public void Selected(){
		selected = true;
		panelSelect.enabled = true;
	}
	public void UnSelected(){
		selected = false;
		panelSelect.enabled = false;
	} 

//API
	public void ChangeInfo(InfoHero hero, ListCardOnWarTableScript listCardController){
		this.hero = hero;
		this.listCardController = listCardController;
		UpdateUI();
	}
	public void ChangeInfo(InfoHero hero){
		this.hero = hero;
		UpdateUI();
	}
	public void DestroyCard(){
		listCardController.RemoveCardFromList(this);
		Destroy(gameObject);
	}
}
