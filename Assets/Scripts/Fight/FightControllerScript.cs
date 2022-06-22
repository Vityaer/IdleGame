using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
using UnityEngine.UI;
using TMPro;
using System;
public partial class FightControllerScript : MonoBehaviour{
	public Canvas canvasFightUI;
	[Header("Place heros")]
	public List<HexagonCellScript> leftTeamPos  = new List<HexagonCellScript>();
	public List<HexagonCellScript> rightTeamPos = new List<HexagonCellScript>();

	public  List<Warrior> leftTeam  = new List<Warrior>(); 
	public  List<Warrior> rightTeam = new List<Warrior>(); 

	[Header("Initiative List")]	
	public List<HeroControllerScript> listInitiative  = new List<HeroControllerScript>();

	[Header("Location")]
	public LocationControllerScript locationController;

//Create Teams
	public void SetMission(Mission mission){
		this.mission = mission;
		locationController.OpenLocation( mission.location );
		Debug.Log("begin AI!!!!");
		AIController.Instance.StartAIFight();
	}
    public void CreateTeams(List<WarriorPlaceScript> leftWarriorPlace, List<WarriorPlaceScript> rightWarriorPlace){
    	Screen.orientation = ScreenOrientation.LandscapeRight;
    	canvasFightUI.enabled = true;
    	CreateTeam(leftTeamPos,  leftWarriorPlace,  Side.Left );
    	CreateTeam(rightTeamPos, rightWarriorPlace, Side.Right );
    	listInitiative.Sort(new HeroInitiativeComparer());
    	StartCoroutine(StartFightCountdown());
    }
	Text textTimer;
    IEnumerator StartFightCountdown(){
    	for (int i = 3; i>0; i--){
	    	textTimer.text = i.ToString();
			yield return new WaitForSeconds(0.75f);
		}
		textTimer.text = "Fight!";
		yield return new WaitForSeconds(0.5f);
		textNumRound.text = string.Concat("Round 1"); 
		textTimer.text = "";
 		isFightFinish = false;
 		PlayDelegateOnStartFight();
		StartFight();

    }
    private void CreateTeam(List<HexagonCellScript> teamPos, List<WarriorPlaceScript> team, Side side){
    	HeroControllerScript heroScript;
    	GameObject hero;
    	for(int i=0; i < team.Count; i++){
    			hero = null;
    			heroScript = null;
    			if((side == Side.Left && (team[i].card != null)) || (team[i].hero != null))
		    		hero = Instantiate(team[i].hero.generalInfo.Prefab, teamPos[i].Position, Quaternion.identity);
    			if(hero != null){
	    			heroScript = hero.GetComponent<HeroControllerScript>(); 
	    			List<Warrior> workTeam = (side == Side.Left) ? leftTeam : rightTeam;
	    			workTeam.Add(new Warrior(heroScript));
	    			heroScript.SetHero(team[i].hero, teamPos[i], side);
	    			listInitiative.Add(heroScript);
    			}

    	}
    }
//Fight loop    	
 	public void StartFight(){ NextHero(); } 
 	private bool isFightFinish = false;
 	private int currentHero = -1;
 	private  int round = 1;
 	public TextMeshProUGUI textNumRound;
 	public int MaxCountRound = 3;
 	public void NextHero(){
 		if((currentHero + 1) < listInitiative.Count){
 			currentHero++;
 		}else{
 			NewRound();
 		} 
 		if(isFightFinish == false){
			listInitiative[currentHero].DoAction();
 		}
 	}
 	private void NewRound(){
 		currentHero = 0;
		round++;
		textNumRound.text = string.Concat("Round ",round.ToString()); 
		PlayDelegateEndRound();
		if(round == MaxCountRound){
			Win(Side.Right);
		}
 	}
//Victory
 	private void CheckFinishFight(){
 		if((leftTeam.Count == 0) || (rightTeam.Count == 0)) Win(leftTeam.Count > 0 ? Side.Left : Side.Right);
 	}
 	void Win(Side side){
    	Screen.orientation = ScreenOrientation.Portrait;
 		isFightFinish = true;
 		PlayDelegateOnFinishFight();
 		if(side == Side.Left){
 			MessageControllerScript.Instance.AddMessage("Ты выиграл!");
 		}else{
 			MessageControllerScript.Instance.AddMessage("Ты проиграл!");
 			
 		}
    	StartCoroutine(FinishFightCountdown(side));

 	}
 	Mission mission;
 	IEnumerator FinishFightCountdown(Side side){
		textTimer.text = "Конец боя!";
		if(side != Side.Left) CheckSaveResult();
		yield return new WaitForSeconds(4f);
		if(side == Side.Left){
	 		CampaignScript.Instance.GetResult(win: true);
 		}else{
	 		CampaignScript.Instance.GetResult(win: false);
 		}
		textTimer.text = "";
 		ClearAll();
    }
    public void CloseFigthUI(){
 		WarTableControllerScript.Instance.FinishMission();
 		canvasFightUI.enabled = false;
    }
 	void ClearAll(){
 		for (int i = rightTeam.Count - 1; i >= 0;  i-- ){
 			rightTeam[i].heroController?.DeleteHero();
 			rightTeam[i].heroController = null;
		}
		for (int i = leftTeam.Count - 1; i >= 0;  i-- ){
 			leftTeam[i].heroController?.DeleteHero();
 			leftTeam[i].heroController = null;
		}
		listInitiative.Clear();
		currentHero = -1; 
		round = 1;
 	}
 	void CheckSaveResult(){ if((mission is BossMission)) (mission as BossMission).SaveResult(); }
 	private static FightControllerScript instance;
	public  static FightControllerScript Instance{get => instance;}
	void Awake(){
		instance = this;
		textTimer = GameObject.Find("TimerText").GetComponent<Text>();
	}

//API
 	public void MessageAboutDamageForAttacker(float damage){
 		listInitiative[currentHero].MessageDamageAfterStrike(damage);
 	}
 	public void DeleteHero(HeroControllerScript heroForDelete){
 		Warrior warrior = leftTeam.Find(x => x.heroController == heroForDelete);
 		if(warrior != null){
 			leftTeam.Remove(warrior);
 		}else{
	 		warrior = rightTeam.Find(x => x.heroController == heroForDelete);
 			rightTeam.Remove(warrior);
 		}
 		CheckFinishFight();
 	}
 	public HeroControllerScript GetCurrentHero(){ return listInitiative[currentHero]; }

//Listeners
	private Action delsOnEndRound, delsOnStartFight, delsOnFinishFight;
	public void RegisterOnStartFight(Action d){ delsOnStartFight += d;}	 	
	public void UnregisterOnStartFight(Action d){ delsOnStartFight -= d; }
	private void PlayDelegateOnStartFight(){ if(delsOnStartFight != null) delsOnStartFight(); }

	public void RegisterOnEndRound(Action d){ delsOnEndRound += d;}	 	
	public void UnregisterOnEndRound(Action d){ delsOnEndRound -= d; }
	private void PlayDelegateEndRound(){ if(delsOnEndRound != null) delsOnEndRound(); }

	public void RegisterOnFinishFight(Action d){ delsOnFinishFight += d;}	 	
	public void UnregisterOnFinishFight(Action d){ delsOnFinishFight -= d; }
	private void PlayDelegateOnFinishFight(){ if(delsOnFinishFight != null) delsOnFinishFight(); }

}
