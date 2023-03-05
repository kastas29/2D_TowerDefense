using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_FoodText;
    [SerializeField]
    TextMeshProUGUI m_MaterialsText;
    private void OnEnable()
    {
        EconomyManager.EM.FoodChanged += updateFood;
        EconomyManager.EM.MaterialsChanged += updateMaterials;
    }

    private void OnDisable()
    {
        EconomyManager.EM.FoodChanged -= updateFood;
        EconomyManager.EM.MaterialsChanged -= updateMaterials;
    }

    private void updateFood(int num) {
        m_FoodText.text = num.ToString();
    }

    private void updateMaterials(int num) {
        m_MaterialsText.text = num.ToString();
    }
}
