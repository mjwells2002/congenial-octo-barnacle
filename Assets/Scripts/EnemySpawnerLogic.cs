using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerLogic : MonoBehaviour
{
	private int toSpawn;
	private int remianingSpawn;
	
	public GameObject Enemy;

	private void Start()
	{
		toSpawn = Random.Range(1,9);
		StartCoroutine(spawnAll());
	}

	IEnumerator spawnAll()
	{
		for (int i = 0; i<toSpawn; i++)
		{
			yield return new WaitForSeconds(Random.Range(0.1f,0.6f));
			Instantiate(Enemy,transform.position,Quaternion.identity);
		}
	}
}
