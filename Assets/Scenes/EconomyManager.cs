using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager EM;

    public event Action<int> FoodChanged;
    public event Action<int> MaterialsChanged;
    [SerializeField]
    Prices m_Prices;

	public GameObject foodAmount;
	public GameObject materialAmount;

    //Just a singleton
    void Awake()
    {
        if (EM == null)
            EM = this;
        else
            GameObject.Destroy(EM);
    }

    public int m_Food {private get; set;}
	public int m_Materials { private get; set; }

    private void OnEnable()
    {
        GetArchers.BuyArcher += TryToBuyArcher;
    }

    private void OnDisable()
    {
        GetArchers.BuyArcher -= TryToBuyArcher;
    }

    public void setFood(int num) {
	    m_Food += num;
        FoodChanged?.Invoke(m_Food);
    }
    public void setMaterial(int num)
    {
        m_Materials += num;
        MaterialsChanged?.Invoke(m_Materials);
    }

    public bool TryToBuild(string tier)
    {
        if (m_Prices.PriceBuildingT1 > m_Materials)
        {
            return false;
        }
        m_Materials -= m_Prices.PriceBuildingT1;
        MaterialsChanged?.Invoke(m_Materials);
        return true;
    }

    private bool TryToBuyArcher()
    {
        if (m_Prices.PriceArchers > m_Food)
        {
            return false;
        }
        m_Food -= m_Prices.PriceArchers;
        FoodChanged?.Invoke(m_Food);
        return true;
    }
}
