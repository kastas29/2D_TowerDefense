using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HoverOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject InfoPanel;
	
	public void OnPointerExit(PointerEventData eventData)
	{
        InfoPanel.SetActive(false);
		print("exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
        InfoPanel.SetActive(true);
		print("enter");
    }
}
