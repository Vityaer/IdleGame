using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class HeroStatusScript : MonoBehaviour{
	public SliderScript sliderHP;
	public SliderScript sliderStamina;
	private Vector2 delta = new Vector2(0, 30), posUI;
	private HeroControllerScript heroController;
	// private GameObject panelStatus;
	void Awake(){
		Vector2 TopLeft = GameObject.Find("CornerTopLeft").GetComponent<Transform>().position;
		Vector2 BottomRight = GameObject.Find("CornerBottomRight").GetComponent<Transform>().position;
		Vector2 pos = GetComponent<Transform>().position;
		posUI = new Vector2();
		posUI.x = 720 * (pos.x/(BottomRight.x - TopLeft.x));
		posUI.y = 1280 * (pos.y/(TopLeft.y - BottomRight.y)) + delta.y; 
		posUI += delta;
		// panelStatus = Instantiate(Resources.Load<GameObject>("UI/HeroStatus"), GameObject.Find("FightUI/BoxSliderHeroes").transform);
		// panelStatus.GetComponent<RectTransform>().anchoredPosition = posUI;
		// sliderHP      = panelStatus.transform.Find("SliderHP").GetComponent<SliderScript>();
		// sliderStamina = panelStatus.transform.Find("SliderStamina").GetComponent<SliderScript>();
		heroController = GetComponent<HeroControllerScript>();
	}
	void Start(){
		FightControllerScript.Instance.RegisterOnEndRound(RoundFinish);
		gameObject.transform.Find("CanvasHeroesStatus").gameObject.SetActive(true);
	}
//Helth	
	private float currentHP;
	public void ChangeHP(float HP){
		if(HP < currentHP){ListFightTextsScript.Instance.ShowDamage(currentHP - HP, posUI);} else{ListFightTextsScript.Instance.ShowHeal(HP - currentHP, posUI);}
		currentHP = HP;
		sliderHP.ChangeValue(HP);
		if((HP / sliderHP.maxValue < 0.5f) &&(HP / sliderHP.maxValue > 0.3f)){
			heroController.OnHPLess50();
		}else if(HP / sliderHP.maxValue < 0.3f){
			heroController.OnHPLess30();
		}
	}
	public void SetMaxHealth(float maxHP){
		if(currentHP == 0) currentHP = maxHP;
		sliderHP.SetMaxValue(maxHP);
	}
	public void ChangeMaxHP(float amountChange){
		SetMaxHealth(sliderHP.maxValue + amountChange);
	}
	public void Death(){
		sliderHP.Death();
		sliderStamina.Death();
	}
//Stamina
	private float stamina = 25;
	public float Stamina{get => stamina;}
	public void ChangeStamina(float addStamina){
		stamina += addStamina;
		if(stamina > 100f) stamina = 100f;
		sliderStamina.ChangeValue(stamina);
	}
}
