using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelFortuneScript : Building{

	[Header("Images reward")]
	public List<Image> images = new List<Image>();
	public List<Text> texts = new List<Text>();

	[Header("List reward")]
	public List<FortuneReward> rewards = new List<FortuneReward>();

	[Header("Buttons")]
	public Button btnSimpleOneCoin;

	[Header("Arrow")]
	public RectTransform arrowRect;
	public float arrowSpeed;
	private float t;
	private float previousTilt = 0f;

	private float generalProbability = 0f;

	protected override void OpenPage(){
		FillWheelFortune();
		CalculateProbability();
		CheckResourceForButton(TypeResource.CoinFortune, 1, btnSimpleOneCoin);

	}
	public void PlaySimpleRoulette(int coin = 1){
		StartCoroutine(IRotateArrow(GetRandom()));
		PlayerScript.Instance.SubtractResource(new Resource(TypeResource.CoinFortune, 1));
		CheckResourceForButton(TypeResource.CoinFortune, 1, btnSimpleOneCoin);
	}
	int numReward = 0;
	private float GetRandom(){
		float result = 0f;
		int k = 0;
		float rand = Random.Range(0f, generalProbability);
		for(int i = 0; i < rewards.Count; i++){
			result += rewards[i].probability;
			if(result < rand){ k++; } else { break; } 
		}
		numReward = k;
		return (k * 36f + 360f);
	}
	private void CalculateProbability(){
		generalProbability = 0f;
		foreach(FortuneReward reward in rewards)
			generalProbability += reward.probability;
	}
	private void FillWheelFortune(){
		for(int i = 0; i < rewards.Count; i++){
			images[i].sprite = rewards[i].GetImage();
			texts[i].text = rewards[i].GetCount();
		}
	}
	private void CheckResourceForButton(TypeResource typeResource, int targetCount, Button button){
		button.interactable = PlayerScript.Instance.CheckResource( new Resource(typeResource, targetCount) );
	}

    IEnumerator IRotateArrow(float targetTilt){
    	Debug.Log(previousTilt.ToString() + " to " + targetTilt.ToString());
    	targetTilt += previousTilt;
	    float rand = Random.Range(-4f, 4f);
	    t = 0;
		while(t <= 1){
		    arrowRect.rotation = Quaternion.Euler(0, 0, - Mathf.Lerp(previousTilt, targetTilt, t) );
		    t += Time.deltaTime * arrowSpeed;
	    	yield return null; 
		}

    	previousTilt = targetTilt;
    	GetReward(); 
	}
	private void GetReward(){
		switch(rewards[numReward].typeProduct){
			case TypeProduct.Resource:
				PlayerScript.Instance.AddResource( rewards[numReward].rewardResource );
				MessageControllerScript.Instance.AddMessage("Поздравляем! Награда - " + rewards[numReward].rewardResource.ToString() +" " + rewards[numReward].rewardResource.Name.ToString() );
				break;
			case TypeProduct.Item:
				MessageControllerScript.Instance.AddMessage("Поздравляем! Награда - предмет" );
				InventoryControllerScript.Instance.AddItem(rewards[numReward].GetItem);
				break;
			case TypeProduct.Splinter:
			break;
		}
	}	
}

[System.Serializable]
public class FortuneReward : Product{
	public float probability;
}