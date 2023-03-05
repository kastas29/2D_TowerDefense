using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWarrior : MonoBehaviour
{
	public GameObject spawnerLocation;
	public GameObject WarriorGameObject;
	
	public void SpawnWarriorOnMouse()
	{
		GameObject Warrior = Instantiate(WarriorGameObject, spawnerLocation.transform.position, Quaternion.identity);
	}
}
