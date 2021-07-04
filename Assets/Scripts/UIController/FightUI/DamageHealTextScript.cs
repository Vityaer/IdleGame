using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageHealTextScript : MonoBehaviour{
	private bool inWork = false;
	public bool InWork{get => inWork;}
	private Text textComponent;
	public Color colorDamage, colorHeal;
	public Vector2 delta = new Vector2(0, 100f);
	public float speed = 1f;
	private RectTransform rectTransform;
	void Awake(){
		textComponent = GetComponent<Text>();
		rectTransform = GetComponent<RectTransform>();
	}
	public void PlayDamage(float damage, Vector2 pos){
		PlayInfo(damage, pos, colorDamage);
	}
	public void PlayHeal(float heal, Vector2 pos){
		PlayInfo(heal, pos, colorHeal);
	}
	private void PlayInfo(float amount, Vector2 pos, Color color){
		if(inWork == false){
			inWork = true;
			textComponent.color = colorDamage;  
			textComponent.DOFade(1f, 0.05f);
			textComponent.text  = amount.ToString();
			rectTransform.anchoredPosition = pos;
			rectTransform.DOAnchorPos(new Vector2(pos.x + delta.x, pos.y + delta.y), speed).OnComplete(Disable);
		}
	}
	public void Disable(){
		textComponent.DOFade(0f, 0.25f).OnComplete(ClearText);
		inWork = false;
	} 
	public void ClearText(){
		textComponent.text = "";
	}
}
