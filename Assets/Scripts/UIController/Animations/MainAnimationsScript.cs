using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainAnimationsScript : MonoBehaviour{
	public RectTransform rect;
	private Vector2 startSize = new Vector2(1f, 1f);
	void Start(){
		if(rect == null) rect = GetComponent<RectTransform>();
	}
	Tween sequenceScalePulse;
	public void ScalePulse(){
		sequenceScalePulse = DOTween.Sequence()
						.Append(rect.DOScale(startSize * 0.9f, 0.25f))
						.Append(rect.DOScale(startSize, 0.25f));
	}
}
