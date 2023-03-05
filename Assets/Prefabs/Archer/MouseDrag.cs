using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public Action Clicked;
    public Action Dropped;
    public Action DroppedL;

    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject m_DmgArea;
    public Grid grid;

    Floor m_Floor;
    Floor m_NewFloor;
    /*private void OnMouseDrag()
    {
        Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (vector2);
    }
    */

    private void OnMouseDrag()
    {

        /*
        Vector3Int posicio = grid.LocalToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        transform.localPosition = grid.GetCellCenterLocal(posicio);
        */

        Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.localPosition = pos;

    }

    public bool CanIPlaceArcher()
    {
        if (m_NewFloor == null) return false;
        return !m_NewFloor.IsFilled();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            if (!collision.GetComponent<Floor>().IsFilled())
            {
                m_NewFloor = collision.GetComponent<Floor>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            m_NewFloor = null;
        }
    }

    protected void OnMouseUp()
    {
        Dropped?.Invoke();

        if (CanIPlaceArcher())
        {
            m_Floor?.Empty();
            m_Floor = m_NewFloor;
            m_NewFloor = null;
            this.transform.position = new Vector3(m_Floor.transform.position.x, m_Floor.transform.position.y + 0.4f, 0);
            m_Floor.Fill();
            animator.Play("Afk");
            m_DmgArea.SetActive(true);
        }
        else
        {
            if (m_Floor != null)
                this.transform.position = new Vector3(m_Floor.transform.position.x, m_Floor.transform.position.y + 0.4f, 0);
        }
        DroppedL?.Invoke();
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke();
	    m_DmgArea.SetActive(false);
    }
}
