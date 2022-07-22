using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SliderTimeScript : MonoBehaviour{
    private Slider slider;
	private Image fillImage;
	private DateTime requireTime, startTime;
	public Text textTime;
	void Awake(){
		fillImage = transform.Find("Fill Area/Fill").GetComponent<Image>();
		slider = GetComponent<Slider>();
		slider.maxValue = 1f;
	}
	public Color lowValue, fillValue;
	int waitSeconds = 0;
	public void ChangeValue(){
		DateTime deltaTime = requireTime - (DateTime.Now - startTime);
		TimeSpan interval  = new TimeSpan(deltaTime.Hour, deltaTime.Minute, deltaTime.Second);
		TimeSpan generalInterval = new TimeSpan(requireTime.Hour, requireTime.Minute, requireTime.Second);  
		waitSeconds = (int) interval.TotalSeconds;
		if(waitSeconds == 0) StopTimer();
		float t =  1f - (float) (waitSeconds / generalInterval.TotalSeconds);
		fillImage.color = Color.Lerp(lowValue, fillValue, t);
		slider.value = t;
		textTime.text = String.Concat(interval.Hours.ToString(), "h ", interval.Minutes.ToString(), "m"); 
	}
	public void SetMaxValue(DateTime requireTime){
		this.requireTime = requireTime;
		textTime.text = String.Concat(requireTime.Hour.ToString(), "h ", requireTime.Minute.ToString(), "m"); 
	}
	Coroutine coroutineTimer;
	public void SetData(DateTime startTime, DateTime requireTime){
		this.startTime = startTime;
		this.requireTime = requireTime;
		ChangeValue();
		if(coroutineTimer == null)
			coroutineTimer = StartCoroutine(StartTimer());
	}
	public void StopTimer(){
		if(coroutineTimer != null){
			StopCoroutine(coroutineTimer);
			coroutineTimer = null;
		}
	} 
	public void SetInfo(string str){
		textTime.text = str;
	}
	public void SetFinish(){
		StopTimer();
		textTime.text = "Готово!";
	}
	 IEnumerator StartTimer(){
	 	while(true){
	 		ChangeValue();
			yield return new WaitForSeconds((waitSeconds <= 60) ? 1f : waitSeconds % 60);
	 	}
    }
}
