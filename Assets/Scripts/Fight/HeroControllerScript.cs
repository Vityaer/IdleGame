using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HeroControllerScript : MonoBehaviour{
	public HeroStatusScript statusState;
	private Transform tr;
	private HexagonCellScript myPlace;
	public List<HeroControllerScript> listTarget = new List<HeroControllerScript>();
	private Animator anim;
	public float speedMove = 2f;
	public float speedAnimation = 1f;
	private Rigidbody2D rb;
	public Hero hero;
	public Side side = Side.Left;
	private Vector3 delta = new Vector2(-0.6f, 0f);
	public int maxCountCounterAttack = 1;
	private int currentCountCounterAttack = 1;
	public enum StageAction{
		MoveToTarget,
		Attack,
		MoveHome,
		Spell
	}
	public StageAction action;

	public HexagonCellScript Cell{get => myPlace;}
	public bool Mellee{get => hero.characts.baseCharacteristic.Mellee;}
	public TypeStrike typeStrike{get => hero.characts.baseCharacteristic.typeStrike;}
	void Awake(){
		statusState = GetComponent<HeroStatusScript>();
		tr          = GetComponent<Transform>();
		rb          = GetComponent<Rigidbody2D>();
        anim        = GetComponent<Animator>();
	}
	void Start(){
		hero.PrepareSkills(this);
		OnStartFight();
		FightControllerScript.Instance.RegisterOnEndRound(RefreshOnEndRound);
	}
	public void DoAction(){
		if((isDeath == false) && statusState.GetCanAction()){
			listTarget.Clear();
			myPlace.StartCheckMove(hero.characts.baseCharacteristic.Speed, playerCanController: !AIController.Instance.CheckMeOnSubmission(side));
			HexagonCellScript.RegisterOnClick(SelectHexagonCell);

			OnStartAction();
			// if(statusState.Stamina < 100){
			// 	ChooseEnemies(listTarget, hero.characts.CountTargetForSimpleAttack);
			// 	action      = ChooseAction();
			// }else{
			// 	GetListForSpell(listTarget); 
			// 	action = StageAction.Spell;
			// }
			// if(listTarget.Count > 0){
			// 	DoStage();
			// }else{
			// 	EndAction();
			// }

		}else{
			EndAction();
		}
	}
	HexagonCellScript cellTarget;
	HeroControllerScript selectHero;
	public void SelectHexagonCell(HexagonCellScript cell){
		if(cell.CanStand){
			StartMelleeAttackOtherHero(cell, null);
		}else{
			if(cell.Hero != null){
				if(CanAttackHero(cell.Hero) ){
					selectHero = cell.Hero;
					HexagonCellScript.UnregisterOnClick(SelectHexagonCell);
					if((hero.characts.baseCharacteristic.Mellee == true)){
						if(cell.GetCanAttackCell){
							cell.RegisterOnSelectDirection(SelectDirectionAttack);
						}else{
							Debug.Log("i can't attak him");
						}
					}else{
						Debug.Log("i can shoot in him");
						StartDistanceAttackOtherHero(selectHero);
					}
				}	
			}
		}
	}
	public void SelectDirectionAttack(HexagonCellScript targetCell){
		StartMelleeAttackOtherHero(targetCell, selectHero);
	}
	public void SelectDirectionAttack(HexagonCellScript targetCell, HeroControllerScript otherHero){
		StartMelleeAttackOtherHero(targetCell, otherHero);
	}
//Attack
	Coroutine coroutineAttackEnemy;
 	private void StartMelleeAttackOtherHero(HexagonCellScript targetCell, HeroControllerScript enemy){
        coroutineAttackEnemy = null;
		coroutineAttackEnemy = StartCoroutine(IMelleeAttackOtherHero(targetCell, enemy));
 	}
 	public void StartDistanceAttackOtherHero(HeroControllerScript enemy){
		HexagonCellScript.UnregisterOnClick(SelectHexagonCell);
		OnEndSelectCell();
 		CreateArrow(new List<HeroControllerScript>(){enemy});
 	}
 	private bool onGround = true;
 	Stack<HexagonCellScript> way = new Stack<HexagonCellScript>();
	IEnumerator IMelleeAttackOtherHero(HexagonCellScript targetCell, HeroControllerScript enemy){
		HexagonCellScript.UnregisterOnClick(SelectHexagonCell);
		OnEndSelectCell();
		myPlace.ClearSublject();
		way = HexagonGridScript.Instance.FindWay(myPlace, targetCell, onGround: onGround);
		Vector3 target;
		Vector2 dir;
		HexagonCellScript currentCell;
		while(way.Count > 0){
			currentCell = way.Pop();
			Debug.Log(currentCell.gameObject.name);
			target = currentCell.Position;
			dir = target - tr.position; 
	        dir.Normalize();
	        rb.velocity = dir * speedMove;
	        float dist = Vector2.Distance(tr.position, target); 
	        while(dist > 0.05f){
	    		dist = Vector2.Distance(tr.position, target);
				yield return null;
	        }
	        rb.velocity = Vector2.zero;
		}
        myPlace = targetCell;
        myPlace.SetHero(this);
        rb.velocity = Vector2.zero;
		yield return new WaitForSeconds(0.5f);
        if(enemy != null){
			statusState.ChangeStamina(30f);
			anim.Play("Attack");
			enemy.GetDamage(new Strike(hero.characts.Damage, hero.characts.GeneralAttack, typeStrike: typeStrike));
        }
        EndAction();
 	} 

	private void Attack(){
		if(Mellee){
			OnStrike();
			// enemy.GetDamage(new Strike(hero.characts.Damage, hero.characts.GeneralAttack, typeStrike: hero.generalInfo.typeStrike));
		}else{
			CreateArrow(listTarget);		
		}
	}
	private void CouterAttack(){
		if(currentCountCounterAttack > 0){
			HeroControllerScript otherHero = FightControllerScript.Instance.GetCurrentHero();
			if(otherHero.PermissionCoutnerAttack()){
				anim.Play("Attack");
				otherHero.GetDamage(new Strike(hero.characts.Damage, hero.characts.GeneralAttack, typeStrike: typeStrike));
			}			
		}
	}
	private bool permissionCoutnerAttack = true;
	public bool PermissionCoutnerAttack(){ return permissionCoutnerAttack; }

	void CreateArrow(List<HeroControllerScript> listTarget){
		GameObject arrow;
		hitCount = 0;
		this.listTarget = listTarget;
		foreach (HeroControllerScript target in listTarget) {
			arrow = Instantiate(Resources.Load<GameObject>("CreateObjects/Bullet"), tr.position, Quaternion.identity);
			arrow.GetComponent<ArrowScript>().SetTarget(target, new Strike(hero.characts.Damage, hero.characts.GeneralAttack, typeStrike: typeStrike, isMellee: false));
			arrow.GetComponent<ArrowScript>().RegisterOnCollision(HitCount);
		}
	}
	public int hitCount = 0;
	public void HitCount(){
		Debug.Log("distance hit");
		OnStrike();
		hitCount += 1;
		if(hitCount == listTarget.Count) { 
			EndAction();
		}
	}
	protected virtual void DoSpell(){
		anim.Play("SpecialAttack");
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
 		if(Mellee){
	 		return StageAction.MoveToTarget;
 		}else{
	 		return StageAction.Attack;
 		}
 	}
//End action 	
	public void EndAction(){
		OnEndAction();
		anim.Play("Idle");
		FightControllerScript.Instance.NextHero();
	}
	
//API
	public void IsSide(Side side){
		this.side = side;
		delta = (side == Side.Left) ? new Vector2(-0.6f, 0f) : new Vector2(0.6f, 0f);
		if(side == Side.Right) FlipX();
	}
	public void SetHero(InfoHero infoHero, HexagonCellScript place, Side side){
		myPlace = place;
		place.SetHero(this);
		IsSide(side);
		this.hero.SetHero(infoHero);
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
			if(hero.characts.HP == 0){
				Death();
			}else{
				if(strike.isMellee && (this != FightControllerScript.Instance.GetCurrentHero())){
					CouterAttack();
				}
			}	
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
		Debug.Log("delete hero");
		if(coroutineAttackEnemy != null) StopCoroutine(coroutineAttackEnemy);
		coroutineAttackEnemy = null;
		FightControllerScript.Instance.UnregisterOnEndRound(RefreshOnEndRound);
		DeleteAllDelegate();
		Destroy(gameObject);
	}	
	public void ClickOnMe(){
		myPlace?.ClickOnMe();
	}
	public bool isDeath = false;
	public bool IsDeath{get => isDeath;}
	private void Death(){
		statusState.Death();
		anim.Play("Death");
		FightControllerScript.Instance.DeleteHero(this);
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

		private static Action<HeroControllerScript> observerStartAction;
		public static void RegisterOnStartAction(Action<HeroControllerScript> d){ observerStartAction += d;}
		public static void UnregisterOnStartAction(Action<HeroControllerScript> d){ observerStartAction -= d;}
		private void OnStartAction(){if(observerStartAction != null) observerStartAction(this);}

		private static Action observerEndAction;
		public static void RegisterOnEndAction(Action d){ observerEndAction += d;}
		public static void UnregisterOnEndAction(Action d){ observerEndAction -= d;}
		private void OnEndAction(){if(observerEndAction != null) observerEndAction();}

		private static Action observerEndSelectCell;
		public static void RegisterOnEndSelectCell(Action d){ observerEndSelectCell += d;}
		public static void UnregisterOnEndSelectCell(Action d){ observerEndSelectCell -= d;}
		private void OnEndSelectCell(){if(observerEndSelectCell != null) observerEndSelectCell();}

	private void FlipX(){
		Vector3 locScale = transform.localScale;
		locScale.x *= -1;
		transform.localScale = locScale; 
	} 
//Fight helps
	private bool CanAttackHero(HeroControllerScript otherHero){
		if(this.side != otherHero.side){
			return true;
		}else{
			return false;
		}
	}
	private void RefreshOnEndRound(){
		currentCountCounterAttack = maxCountCounterAttack;
	}
}





public enum Side{
	Left,
	Right,
	All
}