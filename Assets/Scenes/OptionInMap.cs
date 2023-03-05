using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionInMap : MonoBehaviour
{
    [SerializeField]
    GameObject m_PosArrow;
    [SerializeField]
    GameObject m_Poster;
    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        m_PosArrow.SetActive(false);
        m_Poster.SetActive(true);
    }

    private void OnMouseExit()
    {
        m_PosArrow.SetActive(true);
        m_Poster.SetActive(false);
    }
}
