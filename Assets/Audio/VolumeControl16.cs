using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl16 : MonoBehaviour
{
	
	public Slider sliderControl;
	private float value;
	public GameObject GameManager;
	private float volume;

    // Update is called once per frame
	public void ControlVolume()
	{
		volume = GameManager.GetComponent<AudioSource>().volume;
		value = sliderControl.value;
		
		GameManager.GetComponent<AudioSource>().volume = (((float)1/16) * value);
    }
}
