using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnR2L : MonoBehaviour
{
	public GameObject[] clouds;
	private float SpawnCooldown = 1f;
	
	// Start is called before the first frame update
    void Start()
    {
	    StartCoroutine("SpawnClouds");
    }

	IEnumerator SpawnClouds()
	{
		while (true)
		{
			float randomSize, randomHeight, randomSpeed;
			GameObject randomCloud = clouds[(int)Random.Range(0,clouds.Length)];
			randomSize = Random.Range(0.7f,1.3f);
			randomHeight = Random.Range(-12,12);
			randomSpeed = Random.Range(0.8f,2f);
		
			GameObject newCloud = Instantiate(randomCloud,new Vector3(this.transform.position.x,this.transform.position.y + randomHeight,0),Quaternion.identity);
			float randomAlpha = Random.Range(0.1f,1.1f);
			
			newCloud.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, newCloud.GetComponent<SpriteRenderer>().color.a * randomAlpha);
			
			newCloud.transform.localScale = newCloud.transform.localScale * randomSize;
			newCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(randomSpeed,0);
		
			yield return new WaitForSeconds(SpawnCooldown);
		}
	}
}
