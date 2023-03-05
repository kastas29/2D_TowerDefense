using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutTitleScreen : MonoBehaviour
{
	// Start is called before the first frame update
    void Start()
	{
		StartCoroutine(FadeOut(this.GetComponent<Image>()));
    }
    
	private IEnumerator FadeOut(Image blackintro)
	{
		while (blackintro.color.a > 0)
		{
			Color temp = blackintro.color;
			temp.a = 0.0125f;
			blackintro.color -= temp;
			
			yield return new WaitForSeconds(0.005f);
		}
		Destroy(this.gameObject);
	}
}
