using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Floor;

public class Floor : MonoBehaviour
{

    public delegate bool TryUpgradeTower(string tier);
    public TryUpgradeTower tryUpgradeTower;

    [SerializeField]
    Prices m_Price;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject WhiteSquare;
    [SerializeField]
    Image Target;
    [SerializeField]
    GameObject Target2;
    [SerializeField]
    TextMeshProUGUI m_MaterialsPrice;
    [SerializeField]
    GameObject m_Canvas;
    [SerializeField]
    GameObject m_Tower;
    [SerializeField]
    List<Sprite> m_SpritesTowers = new List<Sprite>();

    bool m_ImATower = false;
    int m_ActualGap = 0;
    int m_MaxGap = 1;
    TierTower m_nextTier = TierTower.T1;

    private void OnEnable()
    {
        m_MaterialsPrice.text = m_Price.PriceBuildingT1.ToString();
        tryUpgradeTower += EconomyManager.EM.TryToBuild;
    }

    private void OnDisable()
    {
        tryUpgradeTower -= EconomyManager.EM.TryToBuild;
    }

    public void UpgradeTower()
    {
        if (tryUpgradeTower?.Invoke(m_nextTier.ToString()) == true)
        {
            switch (m_nextTier) {
                case TierTower.T1:
                    m_ImATower = true;
                    m_nextTier = TierTower.T2;
                    m_Tower.SetActive(true);
                    m_MaterialsPrice.text = m_Price.PriceBuildingT2.ToString();
                    break;
                case TierTower.T2:
                    m_MaxGap = 2;
                    m_nextTier = TierTower.T3;
                    m_Tower.GetComponent<SpriteRenderer>().sprite = m_SpritesTowers[0];
                    m_MaterialsPrice.text = m_Price.PriceBuildingT3.ToString();
                    break;
                case TierTower.T3:
                    m_MaxGap = 4;
                    m_Tower.GetComponent<SpriteRenderer>().sprite = m_SpritesTowers[1];
                    m_Canvas.SetActive(false);
                    m_Canvas = null;
                    break;
            }
        }
    }

    public void ShowOptions()
    {
        Target2.SetActive(true);
    }

    public void HideOptions()
    {
        Target2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Archer")
        {
            anim?.Play("GrowWhenHover");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Archer")
        {
            anim?.Play("DecreasWhenHover");
        }
    }

    public void AppearAnimation()
    {
        anim.Play("Appear");
    }

    public void DisappearAnimation()
    {
        anim.Play("Disappear");
    }

    public void ResetAnimation()
    {
        anim.Play("Idle");
    }

    public void Fill()
    {
        m_ActualGap++;
        WhiteSquare.SetActive(false);
        m_Canvas?.SetActive(true);
        if (!m_ImATower)
            this.transform.parent = GameObject.Find("Towers").transform;
    }

    public void Empty()
    {
        m_ActualGap--;
        m_Canvas?.SetActive(false);
        if (!m_ImATower) {
            WhiteSquare.SetActive(true);
            this.transform.parent = GameObject.Find("PlacesForArchers").transform;
        }
    }

    public bool IsFilled()
    {
        if (m_ActualGap >= m_MaxGap) {
            return true;
        }
        return false;
    }

}

public enum TierTower
{
    T1, T2, T3
}
