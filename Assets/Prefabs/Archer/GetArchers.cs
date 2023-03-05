using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArchers : MonoBehaviour
{
    public static event Func<bool> BuyArcher;
    public static event Func<bool> PlaceArcher;

    [SerializeField]
    GameObject m_Icon;

    [SerializeField]
    SpawnArcher m_SpawnArcher;

    GameObject m_Archer;

    bool dragging = false;
    public void onEnter() {
        if (!dragging) {
            m_Icon.SetActive(false);
            m_Archer = m_SpawnArcher.SpawnArcherOnPosition();
            PlaceArcher += m_Archer.GetComponent<MouseDrag>().CanIPlaceArcher;
            m_Archer.GetComponent<MouseDrag>().Clicked += ArcherIsClicked;
            m_Archer.GetComponent<MouseDrag>().Dropped += ArcherIsDropped;
        }
    }

    public void ArcherIsClicked() {
        dragging = true;
        m_Archer.transform.SetParent(null);
    }

    
    //Si se suelta el arquero, damos por hecho de que lo ha comprado...
    public void ArcherIsDropped() {

        if (PlaceArcher?.Invoke() == false || BuyArcher?.Invoke() == false)
        {
            m_SpawnArcher.DeleteLastArcher();
        }

        dragging = false;
        m_Icon.SetActive(true);
        m_Archer.GetComponent<MouseDrag>().Clicked -= ArcherIsClicked;
        m_Archer.GetComponent<MouseDrag>().Dropped -= ArcherIsDropped;
        PlaceArcher -= m_Archer.GetComponent<MouseDrag>().CanIPlaceArcher;
        m_Archer = null;

    }

    public void onExit()
    {
        if (!dragging && m_Archer!=null) {
            m_SpawnArcher.DeleteLastArcher();
            m_Icon.SetActive(true);
        }
    }

    public void Bought() {
        
    }
}
