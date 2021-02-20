using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static PauseMenu instance;
	public GameObject pause;

	private void Start()
	{
		instance = this;
		pause.SetActive(false);
		DontDestroyOnLoad(gameObject);
	}

	public void ShowControls(){
		SceneManager.LoadScene("ShowControls",LoadSceneMode.Additive);
	}
	public void doPause(){
		Time.timeScale = 0f;
		pause.SetActive(true);
	}
	public void unpause(){
		Time.timeScale = 1f;
		pause.SetActive(false);
	}
	public void quitGame(){
		Application.Quit();
	}
}


