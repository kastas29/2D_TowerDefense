using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prices", menuName = "ScriptableObjects/Prices", order = 1)]
public class Prices : ScriptableObject
{
    [SerializeField]
    private int m_PriceArchers;
    [SerializeField]
    private int m_PriceWarriors;
    [SerializeField]
    private int m_PriceBuildingT1;
    [SerializeField]
    private int m_PriceBuildingT2;
    [SerializeField]
    private int m_PriceBuildingT3;
    public int PriceArchers { get => m_PriceArchers; private set => m_PriceArchers = value; }
    public int PriceWarriors { get => m_PriceWarriors; private set => m_PriceWarriors = value; }
    public int PriceBuildingT1 { get => m_PriceBuildingT1; private set => m_PriceBuildingT1 = value; }
    public int PriceBuildingT2 { get => m_PriceBuildingT2; private set => m_PriceBuildingT2 = value; }
    public int PriceBuildingT3 { get => m_PriceBuildingT3; private set => m_PriceBuildingT3 = value; }
}
