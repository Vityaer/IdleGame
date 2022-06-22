using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSliderControllerScript : MonoBehaviour{

	public Slider slider;
	public Text textSlider;

    void Awake(){
    	if(slider == null){
	        slider = GetComponent<Slider>();
	        textSlider = transform.Find("TextSlider").GetComponent<Text>();
    	}
    }

    public void SetAmount(int currentAmount, int maxAmount){
        slider.maxValue = maxAmount; 
        slider.value = currentAmount;
		textSlider.text = string.Concat(currentAmount.ToString(), " / ", maxAmount.ToString());
    	Show();
    }
    public void SetAmount(BigDigit currentAmount, BigDigit maxAmount){
        slider.maxValue = 1f;
        slider.value = (currentAmount/maxAmount).ToFloat();
        textSlider.text = string.Concat(currentAmount.ToString(), " / ", maxAmount.ToString());
        Show();
    }
    public void Hide(){
    	gameObject.SetActive(false);
    }
    void Show(){
    	gameObject.SetActive(true);
    } 
}
