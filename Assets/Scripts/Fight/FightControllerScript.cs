using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
using UnityEngine.UI;
public partial class FightControllerScript : MonoBehaviour{
	public Canvas canvasFightUI;
	[Header("Place heros")]
	public List<PlaceHero> leftTeamPos  = new List<PlaceHero>();
	public List<PlaceHero> rightTeamPos = new List<PlaceHero>();

	[Header("Teams")]
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
	}
    public void CreateTeams(List<WarriorPlaceScript> leftWarriorPlace, List<WarriorPlaceScript> rightWarriorPlace){
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
		textTimer.text = "";
 		isFightFinish = false;
		StartFight();

    }
    private void CreateTeam(List<PlaceHero> teamPos, List<WarriorPlaceScript> team, Side side){
    	HeroControllerScript heroScript;
    	GameObject hero;
    	for(int i=0; i < team.Count; i++){
    			hero = null;
    			heroScript = null;
    			if(side == Side.Left){
    				if(team[i].card != null){
			    		hero = Instantiate(team[i].hero.generalInfo.Prefab, teamPos[i].tr.position, Quaternion.identity);
    				}
    			}else{
    				if(team[i].hero != null)
		    			hero = Instantiate(team[i].hero.generalInfo.Prefab, teamPos[i].tr.position, Quaternion.identity);
    			}
    			if(hero != null){
	    			heroScript = hero.GetComponent<HeroControllerScript>(); 
	    			List<Warrior> workTeam = (side == Side.Left) ? leftTeam : rightTeam;
	    			workTeam.Add(new Warrior(heroScript, teamPos[i]));
	    			heroScript.SetHero(team[i].hero, teamPos[i]);
	    			listInitiative.Add(heroScript);
    			}

    	}
    }
//Fight loop    	
 	public void StartFight(){
 		NextHero();
 	} 
 	private bool isFightFinish = false;
 	private int currentHero = -1;
 	private  int round = 1;
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
 		Debug.Log("next round");
 		currentHero = 0;
		round++;
		PlayDelegateEndRound();
		if(round == MaxCountRound){
			Win(Side.Right);
		}
 	}
//Victory
 	void Win(Side side){
 		isFightFinish = true;
 		if(side == Side.Left){
 			MessageControllerScript.Instance.AddMessage("Ты выиграл!");
 		}else{
 			MessageControllerScript.Instance.AddMessage("Ты проиграл!");
 			
 		}
    	StartCoroutine(FinishFightCountdown(side));

 	}
 	Mission mission;
 	IEnumerator FinishFightCountdown(Side side){
 		Debug.Log("конец боя");
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
 	void CheckSaveResult(){
 		if(mission.saveResult){
	 		Debug.Log("change hp");
	 		int id, currentHP;
	 		for(int j = 0; j < mission.listEnemy.Count; j++){
	 			Debug.Log(mission.listEnemy[j].enemyPrefab.name);
	 			id = int.Parse(mission.listEnemy[j].enemyPrefab.name);
	 			currentHP = 0;
	 			for(int i=0; i < rightTeam.Count; i++){
	 				if(rightTeam[i].heroController != null){
	 					Debug.Log(rightTeam[i].heroController.hero.generalInfo.idHero.ToString() + " and " + id.ToString());
	 					if(rightTeam[i].heroController.hero.generalInfo.idHero == id){
	 						currentHP = rightTeam[i].heroController.hero.characts.HP;
	 						break;
	 					}	
	 				}
	 			}
 				mission.listEnemy[j].CurrentHP = currentHP; 
	 		}
 		}
 	}
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

//Listeners
	public delegate void Del();
	private Del delsOnEndRound;
	public void RegisterOnEndRound(Del d){ delsOnEndRound += d;}	 	
	public void UnRegisterOnEndRound(Del d){ delsOnEndRound -= d; }
	private void PlayDelegateEndRound(){ if(delsOnEndRound != null) delsOnEndRound(); }
}
