using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControllerScript : MonoBehaviour{
	public HeroStatusScript statusState;
	private Transform tr;
	private PlaceHero homePlace;
	public List<HeroControllerScript> listTarget = new List<HeroControllerScript>();
	private Animator anim;
	public float speedMove = 2f;
	public float speedAnimation = 1f;
	private Rigidbody2D rb;
	public Hero hero;
	public Side side = Side.Left;
	private Vector3 delta = new Vector2(-0.6f, 0f);
	public enum StageAction{
		MoveToTarget,
		Attack,
		MoveHome,
		Spell
	}
	public StageAction action; 
	void Awake(){
		statusState = GetComponent<HeroStatusScript>();
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	void Start(){
		hero.PrepareSkills(this);
		OnStartFight();
	}
	public void DoAction(){
		if((isDeath == false) && statusState.GetCanAction()){
			listTarget.Clear();
			if(statusState.Stamina < 100){
				ChooseEnemies(listTarget, hero.characts.CountTargetForSimpleAttack);
				action      = ChooseAction();
			}else{
				GetListForSpell(listTarget); 
				action = StageAction.Spell;
			}
			if(listTarget.Count > 0){
				DoStage();
			}else{
				EndAction();
			}

		}else{
			EndAction();
		}
	}
	private void DoStage(){
		switch (action){
			case StageAction.MoveToTarget:
					MoveToPoint(listTarget[0].GetPosition() + delta);
				break;
			case StageAction.Attack:
					Attack();
				break;
			case StageAction.Spell:
					DoSpell();
				break;	
		}
	}
	private void NextStage(){
		switch (action){
			case StageAction.MoveToTarget:
				action = StageAction.Attack;
				Attack();
				break;
			case StageAction.Attack:
				action = StageAction.MoveHome;
				MoveToPoint(homePlace.tr.position);
				break;	
			case StageAction.MoveHome:
				EndAction();
				break;	
		}
	}
//Move to point	
	Coroutine coroutineMove;
	private void MoveToPoint(Vector3 target){
        coroutineMove = null;
		coroutineMove = StartCoroutine(IMove(target));
	}
	IEnumerator IMove(Vector3 target){
		Vector2 dir = target - tr.position; 
        dir.Normalize();
        rb.velocity = dir * speedMove;
        float dist = Vector2.Distance(tr.position, target); 
        while(dist > 0.25f){
    		dist = Vector2.Distance(tr.position, target);
			yield return null;
        }
        rb.velocity = new Vector2();
        NextStage();
 	} 	
//Attack
	private void Attack(){
		statusState.ChangeStamina(30f);
		if(hero.generalInfo.Mellee){
			anim.Play("Attack");
			OnStrike();
			listTarget[0].GetDamage(new Strike(hero.characts.Attack, typeStrike: hero.generalInfo.typeStrike));
			NextStage();
		}else{
			anim.Play("Attack");
			CreateArrow(listTarget);		
		}
	}

	void CreateArrow(List<HeroControllerScript> listTarget){
		GameObject arrow;
		hitCount = 0;
		foreach (HeroControllerScript target in listTarget) {
			arrow = Instantiate(Resources.Load<GameObject>("CreateObjects/Bullet"), tr.position, Quaternion.identity);
			arrow.GetComponent<ArrowScript>().SetTarget(target, new Strike(hero.characts.Attack, typeStrike: hero.generalInfo.typeStrike));
			arrow.GetComponent<ArrowScript>().RegisterOnCollision(HitCount);
		}
	}
	public int hitCount = 0;
	public void HitCount(){
		OnStrike();
		hitCount += 1;
		if(hitCount == listTarget.Count) { NextStage(); }
	}
	protected virtual void DoSpell(){
		anim.Play("Attack");
		OnSpell();
		Debug.Log("do spell");
		EndAction();
	} 	
//Brain hero 	
 	protected virtual void ChooseEnemies(List<HeroControllerScript> listTarget, int countTarget){
 		listTarget.Clear();
 		if(countTarget == 0){
 			countTarget = 1;
 			hero.characts.CountTargetForSimpleAttack = 1;
 		}
 		FightControllerScript.Instance.ChooseEnemies(side, countTarget, listTarget);

 	}
 	private StageAction ChooseAction(){
 		if(hero.generalInfo.Mellee){
	 		return StageAction.MoveToTarget;
 		}else{
	 		return StageAction.Attack;
 		}
 	}
//End action 	
	public void EndAction(){
		anim.Play("Idle");
		FightControllerScript.Instance.NextHero();
	}
	
//API
	public void IsSide(Side side){
		this.side = side;
		delta = (side == Side.Left) ? new Vector2(-0.6f, 0f) : new Vector2(0.6f, 0f);
		if(side == Side.Right) FlipX();
	}
	public void SetHero(InfoHero hero, PlaceHero place){
		homePlace = place;
		IsSide(place.side);
		this.hero.SetHero(hero);
		statusState.SetMaxHealth(this.hero.characts.HP);
	}
	public Vector3 GetPosition(){
		return tr.position;
	}
	public void GetDamage(Strike strike){
		if(statusState.PermissionGetStrike(strike)){
			OnTakingDamage();
			hero.GetDamage(strike);
			statusState.ChangeHP(hero.characts.HP);
			statusState.ChangeStamina(10f);
			if(hero.characts.HP == 0) Death();
		}
	}
	public void GetHeal(float heal, TypeNumber typeNumber = TypeNumber.Num){
		hero.GetHeal(heal, typeNumber);
		statusState.ChangeHP(hero.characts.HP);
		FightEffectControllerScript.Instance.CreateHealObject(tr);
		OnHeal();
	}
	
	public void ChangeMaxHP(int amountChange, TypeNumber typeNumber = TypeNumber.Num){
		hero.ChangeMaxHP(amountChange, typeNumber);
		statusState.ChangeMaxHP(amountChange);
	}
	public void DestroyHero(){
		Debug.Log("destroy hero");
		statusState.ChangeHP(hero.characts.HP);
		if(hero.characts.HP > 0f) Death();
		OnDeathHero();
		DeleteHero();
	}
	public void DeleteHero(){
		statusState.Delete();
		if(coroutineMove != null) StopCoroutine(coroutineMove);
		coroutineMove = null;
		DeleteAllDelegate();
		Destroy(gameObject);
	}	

	public bool isDeath = false;
	public bool IsDeath{get => isDeath;}
	private void Death(){
		statusState.Death();
		GetComponent<SpriteRenderer>().enabled = false;
		isDeath = true;
	}
	private float damageFromStrike;
	public void MessageDamageAfterStrike(float damage){
		damageFromStrike += damage;
	}
//Event
	//Register
		public delegate void Del();
		public delegate void DelFloat(float damage);
		public delegate void DelListTarget(List<HeroControllerScript> listTarget);
		private Del delsOnStartFight;
		private DelFloat delsOnStrikeFinish;
		private Del delsOnTakingDamage;
		private Del delsOnDeathHero;
		private Del delsOnHPLess50;
		private Del delsOnHPLess30;
		private Del delsOnHeal;
		private DelListTarget delsOnStrike;
		private DelListTarget delsOnSpell;
		private DelListTarget delsOnListSpell;
		public void RegisterOnStartFight(Del d){delsOnStartFight += d;}
		public void RegisterOnStrike(DelListTarget d){delsOnStrike += d;}
		public void RegisterOnTakingDamage(Del d){delsOnTakingDamage += d;}
		public void RegisterOnDeathHero(Del d){delsOnDeathHero += d;}
		public void RegisterOnHeal(Del d){delsOnHeal += d;}
		public void RegisterOnStrikeFinish(DelFloat d){delsOnStrikeFinish += d;}
		public void RegisterOnHPLess50(Del d){delsOnHPLess50 += d;}
		public void RegisterOnHPLess30(Del d){delsOnHPLess30 += d;}
		public void RegisterOnSpell(DelListTarget d){delsOnSpell += d; }
		public void RegisterOnGetListForSpell(DelListTarget d){delsOnListSpell += d;}
	//Action event	
		private void OnStartFight(){ if(delsOnStartFight != null) delsOnStartFight();}
		private void OnStrike(){if(delsOnStrike != null) delsOnStrike(listTarget);}
		private void OnTakingDamage(){if(delsOnTakingDamage != null) delsOnTakingDamage();}
		private void OnDeathHero(){if(delsOnDeathHero != null) delsOnDeathHero();}
		private void OnHeal(){if(delsOnHeal != null) delsOnHeal();}
		public void OnHPLess50(){if(delsOnHPLess50 != null) delsOnHPLess50();}
		public void OnHPLess30(){if(delsOnHPLess30 != null) delsOnHPLess30();}
		public void OnFinishStrike(){if(delsOnStrikeFinish != null) delsOnStrikeFinish(damageFromStrike);}
		public void OnSpell(){if(delsOnSpell != null) delsOnSpell(listTarget);}

		public void GetListForSpell(List<HeroControllerScript> listTarget){
			statusState.ChangeStamina(-100);
			if(delsOnListSpell != null)
				delsOnListSpell(listTarget);
		}
		private void DeleteAllDelegate(){
			Del delsOnStartFight = null;
			Del delsOnStrike = null;
			Del delsOnTakingDamage = null;
			Del delsOnDeathHero = null;
			Del delsOnHPLess50 = null;
			Del delsOnHPLess30 = null;
			Del delsOnHeal = null;
			DelListTarget delsOnSpell = null;
			DelListTarget delsOnListSpell = null;
		}



	private void FlipX(){
		Vector3 locScale = transform.localScale;
		locScale.x *= -1;
		transform.localScale = locScale; 
	} 

}