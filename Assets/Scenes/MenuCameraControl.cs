using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class MenuCameraControl : CameraControl
{
    bool right = false;
    bool m_InMenu = true;
    [SerializeField]
    float m_MenuCameraSpeed;
    float m_SpeedToLvl1;

    private void Awake()
    {
        m_SpeedToLvl1 = m_MenuCameraSpeed/4;
    }
    public override void checkIfZooming()
    {
        //In Menu game i dont zoom.
    }

    public override void checkIfMoving()
    {
        if (!m_InMenu)
        {
            Vector3 mousePosition = Input.mousePosition;
            if (Input.GetKey(KeyCode.D) || mousePosition.x >= Screen.width)
            {
                if (!base.rightIsAtLimit()) this.transform.position += new Vector3(m_MenuCameraSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.A) || mousePosition.x <= 0)
            {
                if (!base.leftIsAtLimit()) this.transform.position -= new Vector3(m_MenuCameraSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.S) || mousePosition.y <= 0)
            {
                if (!bottomIsAtLimit()) this.transform.position -= new Vector3(0, m_MenuCameraSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.W) || mousePosition.y >= Screen.height)
            {
                if (!topIsAtLimit()) this.transform.position += new Vector3(0, m_MenuCameraSpeed * Time.deltaTime, 0);
            }
            return;
        }

        if (right)
        {
            if (!base.rightIsAtLimit()) this.transform.position += new Vector3(m_MenuCameraSpeed / 4 * Time.deltaTime, 0, 0); else right = false;
        }
        else
        {
            if (!base.leftIsAtLimit()) this.transform.position -= new Vector3(m_MenuCameraSpeed / 4 * Time.deltaTime, 0, 0); else right = true;
        }
    }

    public void moveToLevel1() {
        if (base.m_Camera.transform.position.x > 8.44) {
            if (base.m_Camera.transform.position.x - m_SpeedToLvl1 < 8.44) {
                m_SpeedToLvl1 = m_SpeedToLvl1 / 2;
            }
            base.m_Camera.transform.position -= new Vector3(m_SpeedToLvl1, 0,0);
            return;
        }
        m_SpeedToLvl1 = m_MenuCameraSpeed / 4;
        CancelInvoke("moveToLevel1");
    }

    public void onMap() {
        m_InMenu = false;
        InvokeRepeating("moveToLevel1", 0, 0.01f);
    }

    public void onMenu()
    {
        m_InMenu = true;
    }
}