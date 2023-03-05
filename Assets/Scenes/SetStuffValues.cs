using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetStuffValues : MonoBehaviour
{
	public TextMeshProUGUI InGameFood;
	public TextMeshProUGUI InGameMaterial;
	public TextMeshProUGUI SliderFood;
	public TextMeshProUGUI SliderMaterial;
	public Slider slider;

	[SerializeField]
	int values;
	private void Start()
	{
		ChangeSliderValue(values);
    }

	public void ChangeSliderValue(int value) {
        SliderFood.text = (value / 2).ToString();
        SliderMaterial.text = (value / 2).ToString();
        slider.maxValue = value;
        slider.value = value / 2;
    }

	public void ChangeValues()
	{
		SliderFood.text = slider.value.ToString();
        SliderMaterial.text = (values - slider.value).ToString();
    }

	public void SetInitialValues()
	{
        EconomyManager.EM.setFood(int.Parse(SliderFood.text));
        EconomyManager.EM.setMaterial(int.Parse(SliderMaterial.text));
	}

}
