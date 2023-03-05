using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    float m_Mid_Height = 0;
    float m_Mid_Width = 0;

    [SerializeField]
    Transform topLimit;
    [SerializeField]
    Transform bottomLimit;
    [SerializeField]
    Transform rightLimit;
    [SerializeField]
    Transform leftLimit;
	public Camera m_Camera;
	
	[SerializeField]
	int ZoomSpeed;

    [SerializeField]
    private float cameraSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        updateSizeCamera();
    }

    public void updateSizeCamera()
    {
        m_Mid_Height = m_Camera.orthographicSize;
        m_Mid_Width = m_Mid_Height * m_Camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfZooming();
        checkIfMoving();
    }

    public virtual void checkIfZooming() {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
        {
            if (m_Camera.orthographicSize < 5.1 && wheel < 0)
            {
	            m_Camera.orthographicSize -= wheel * ZoomSpeed;
            }

            if (m_Camera.orthographicSize > 3.1 && wheel > 0)
            {
                m_Camera.orthographicSize -= wheel * ZoomSpeed;
            }
            updateSizeCamera();
            checkIfCameraIsOut();
        }
    }

    public virtual void checkIfMoving() {
        Vector3 mousePosition = Input.mousePosition;
        if (Input.GetKey(KeyCode.S) || mousePosition.y <= 0)
        {
            if (!bottomIsAtLimit()) this.transform.position -= new Vector3(0, cameraSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D) || mousePosition.x >= Screen.width)
        {
            if (!rightIsAtLimit()) this.transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W) || mousePosition.y >= Screen.height)
        {
            if (!topIsAtLimit()) this.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A) || mousePosition.x <= 0)
        {
            if (!leftIsAtLimit()) this.transform.position -= new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
        }
    }

    public bool bottomIsAtLimit()
    {
        if (bottomLimit.position.y < transform.position.y - m_Mid_Height)
        {
            return false;
        }
        return true;
    }

    public bool topIsAtLimit()
    {
        if (topLimit.position.y > transform.position.y + m_Mid_Height)
        {
            return false;
        }
        return true;
    }

    public bool leftIsAtLimit()
    {
        if (leftLimit.position.x < transform.position.x - m_Mid_Width)
        {
            return false;
        }
        return true;
    }
    public bool rightIsAtLimit()
    {
        if (rightLimit.position.x > transform.position.x + m_Mid_Width)
        {
            return false;
        }
        return true;
    }

    public void checkIfCameraIsOut()
    {
        if (leftIsAtLimit()) {
            this.transform.position = new Vector3(leftLimit.position.x + m_Mid_Width, transform.position.y, transform.position.z);
        }
        if (topIsAtLimit()) {
            this.transform.position = new Vector3(transform.position.x, topLimit.position.y - m_Mid_Height, transform.position.z);
        }
        if (rightIsAtLimit()) {
            this.transform.position = new Vector3(rightLimit.position.x - m_Mid_Width, transform.position.y, transform.position.z);
        }
        if (bottomIsAtLimit()) {
            this.transform.position = new Vector3(transform.position.x, bottomLimit.position.y + m_Mid_Height, transform.position.z);
        }
    }
}