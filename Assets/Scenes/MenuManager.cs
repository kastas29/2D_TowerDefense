using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_Menu;
    Animator m_AnimatorMenu;
    [SerializeField]
    GameObject m_Map;
    Animator m_AnimatorMap;
    [SerializeField]
    GameObject m_Clouds;
    Animator m_AnimatorClouds;

    [SerializeField]
    Camera m_MainCamera;
    MenuCameraControl m_MenuCameraControl;

    [SerializeField]
    GameObject m_OptionsOnMap;

    [SerializeField]
    List<Button> m_ButtonsOnMenu;

    [SerializeField]
    List<Button> m_ButtonsOnMap;

    private void Start()
    {
        m_AnimatorMenu = m_Menu.GetComponent<Animator>();
        m_AnimatorMap = m_Map.GetComponent<Animator>();
        m_AnimatorClouds = m_Clouds.GetComponent<Animator>();
        m_MenuCameraControl = m_MainCamera.GetComponent<MenuCameraControl>();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowOptions(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void StartGameScene(Scene scene)
    {

        SceneManager.SetActiveScene(scene);
    }

    public void setResolutionFullscreen()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void setResolutionFullscreenWindowed()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    public void setResolution720()
    {
        Screen.SetResolution(1280, 720, false);
    }
    public void setResolution480()
    {
        Screen.SetResolution(640, 480, false);
    }

    public void curtainOff() { 
        
    }

    public void OffMenu()
    {
        ButtonsOnMenuDisable();
        OnMap();
        StartCoroutine("DoOffMenu");
    }

    public void OnMap()
    {
        m_OptionsOnMap.SetActive(true);
        m_Map.SetActive(true);
        ButtonsOnMapEnabled();
        m_AnimatorMap.Play("FadeIn");
    }

    private IEnumerator DoOffMenu()
    {
        m_AnimatorMenu.Play("FadeOut");
        m_AnimatorClouds.Play("FadeOut");
        InvokeRepeating("ZoomIn", 0, 0.008f);
        yield return new WaitForSeconds(m_AnimatorMenu.GetCurrentAnimatorStateInfo(0).length);
        m_Menu.SetActive(false);
    }

    private void ZoomOut()
    {
        if (m_MainCamera.orthographicSize < 9)
        {
            m_MainCamera.orthographicSize += 0.05f;
            m_MenuCameraControl.updateSizeCamera();
            m_MenuCameraControl.checkIfCameraIsOut();
            return;
        }
        CancelInvoke("ZoomOut");
    }

    private void ZoomIn()
    {
        if (m_MainCamera.orthographicSize > 7)
        {
            m_MainCamera.orthographicSize -= 0.05f;
            m_MenuCameraControl.updateSizeCamera();
            m_MenuCameraControl.checkIfCameraIsOut();
            return;
        }
        CancelInvoke("ZoomIn");
    }

    public void OnMenu()
    {
        m_OptionsOnMap.SetActive(false);
        m_Menu.SetActive(true);
        ButtonsOnMenuEnableds();
        m_AnimatorMenu.Play("FadeIn");
    }

    public void OffMap()
    {
        ButtonsOnMapDisable();
        OnMenu();
        StartCoroutine("DoOffMap");
    }

    private IEnumerator DoOffMap()
    {
        m_AnimatorMap.Play("FadeOut");
        m_AnimatorClouds.Play("FadeIn");
        InvokeRepeating("ZoomOut", 0, 0.008f);
        yield return new WaitForSeconds(m_AnimatorMap.GetCurrentAnimatorStateInfo(0).length);
        m_Map.SetActive(false);
    }

    private void ButtonsOnMenuDisable()
    {
        m_ButtonsOnMenu.ForEach(x => x.enabled = false);
    }
    private void ButtonsOnMenuEnableds()
    {
        m_ButtonsOnMenu.ForEach(x => x.enabled = true);
    }
    private void ButtonsOnMapDisable()
    {
        m_ButtonsOnMap.ForEach(x => x.enabled = false);
    }
    private void ButtonsOnMapEnabled()
    {
        m_ButtonsOnMap.ForEach(x => x.enabled = true);
    }
}
