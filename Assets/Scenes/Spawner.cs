using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Lvl1Manager m_Lvl1Manager;
    [SerializeField]
	List<Waypoint> array_waypoints;
    [Serializable]
    public class kvEnemy
    {
        public string name;
        public GameObject gm;
    }

    public List<kvEnemy> ListEnemys = new List<kvEnemy>();
    public Dictionary<string, GameObject> enemies = new Dictionary<string, GameObject>();

    Coroutine runningCoroutine;
    public int EnemiesPerWeave { private get; set; } = 0;
    public Enemy EnemyType { private get; set; } = Enemy.NONE;

    //Unity no sabe poner en el inspector diccionarios entonces hago una lista y luego los meto jaja xd
    private void Awake()
    {
        foreach (var enemy in ListEnemys)
        {
            enemies[enemy.name] = enemy.gm;
        }
    }

    public void StartSpawning() {
		if (EnemiesPerWeave==0) {
            Debug.LogError("Faltan valores por introucir, no puede ser 0... No se iniciara el spawner.");
            return;
        }
		if (!enemies.ContainsKey(EnemyType.ToString())) {
			Debug.LogError("El nombre del enemigo que intentas invocar en el spawn no existe. No se iniciara el spawner.");
			return;
		}
		runningCoroutine = StartCoroutine(EnemicSpawner());
    }
	IEnumerator EnemicSpawner()
	{
		while(EnemiesPerWeave>0)
		{
			List<Waypoint> arr_way = new List<Waypoint>(array_waypoints);
			GameObject newEnemic = Instantiate(enemies[EnemyType.ToString()]);
			newEnemic.GetComponent<EnemicManager>().setWaypoints(arr_way);
            newEnemic.GetComponent<EnemicManager>().IDied += m_Lvl1Manager.EnemyDied;
            newEnemic.transform.position = this.transform.position;
			EnemiesPerWeave--;
            yield return new WaitForSeconds(1);
		}
        StopCoroutine(runningCoroutine);
    }
}
public enum Enemy {
	NONE, Bat, Slime
}
