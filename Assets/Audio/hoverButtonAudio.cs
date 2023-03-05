using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class hoverButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hover;
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource.PlayClipAtPoint(hover, this.transform.position, 100);
        print("hoverSound");
    }
}
