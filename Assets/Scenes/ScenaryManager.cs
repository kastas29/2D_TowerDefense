using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaryManager : MonoBehaviour
{
    [SerializeField]
	Animator m_Curtain;
    
	private string ChangeScene;
	public void level1() {
		ChangeScene = "Level 1";
        StartCoroutine("AnimationTo");
    }
	public void level2() {
		ChangeScene = "Level 2";
		StartCoroutine("AnimationTo");
	}
	public void menu() {
		ChangeScene = "MainMenu";
		StartCoroutine("AnimationTo");
	}


    private IEnumerator AnimationTo() {
        m_Curtain.Play("FadeIn");
        yield return new WaitForSeconds(m_Curtain.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(ChangeScene);
        StopAllCoroutines();
    }
    
    
}
