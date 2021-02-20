using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceEnemySpawnerController : MonoBehaviour
{
    private Camera localCamera;
	private countdownTimer timer;
	public GameObject spawner;
	private bool isSpawned;
    void Start()
    {
        localCamera = Camera.main;
		timer = new countdownTimer(10f);
		spawnWave();
    }

    void Update()
    {
		if(Pterodyactl.eOnScreenPterodyactl == 0)
		{
			if(!isSpawned)
				spawnWave();
			isSpawned = true;
		} else
			isSpawned = false;
			
			
        if(timer.isTriggered)
		{
			if(randomChance((int)(100*Pterodyactl.difficultyPterodyactl)))
			{
				spawnWave();
			}
		}
    }

	private Vector2 randomCoord()
	{
		Vector2 a = new Vector2();
		a.x = Random.Range(localCamera.ViewportToWorldPoint(Vector2.zero).x,localCamera.ViewportToWorldPoint(Vector2.one).x);
		a.y = Random.Range(localCamera.ViewportToWorldPoint(Vector2.zero).y,localCamera.ViewportToWorldPoint(Vector2.one).y);
		return a;
	}

	private Vector2[] randomCoords(int count)
	{
		Vector2[] a = new Vector2[count];
		for (int i = 0; i<count; i++)
		{
			a[i] = randomCoord();
		}
		return a;
	}

	private bool randomChance(int percentageChance)
	{
		if (percentageChance >= 100)
			return true;
		int a = (int)Random.Range(0f,100f);
		return a<=percentageChance;
	}

	private void spawnWave()
	{
		Vector2[] waveCoords = randomCoords((int)Random.Range(3,(int)100*(Pterodyactl.difficultyPterodyactl/10f)));
		
		waveCoords.ToList().ForEach(x => {
			
			//Debug.Log(x);
			if (Pterodyactl.eOnScreenPterodyactl <= Pterodyactl.eMaxPterodyactl)
				Instantiate(spawner,new Vector3(x.x,x.y,-1f),Quaternion.identity);
		});
		
	}
}
