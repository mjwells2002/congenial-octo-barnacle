using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OnScreenTimer : MonoBehaviour
{
    public TextMeshProUGUI timer;

	
	private int timeSeconds = 150;
	private void Start()
	{
		StartCoroutine(doTime());
	}

	private void FixedUpdate()
	{
		
		string time = $"{((int)timeSeconds/60)}:{((timeSeconds%60).ToString("D2"))}";
		timer.text = time;
		if(timeSeconds<=0){
			SceneManager.LoadScene("Timer");
		}
	}
	IEnumerator doTime(){
		for(;;){
			yield return new WaitForSecondsRealtime(1f);
			if(Time.timeScale != 0f)
				timeSeconds--;
		}
	}
}
