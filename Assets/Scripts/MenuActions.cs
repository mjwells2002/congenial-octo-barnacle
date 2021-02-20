using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
	public Slider musicVolume;
	public Slider SFXVolume;
	private AudioSource _a;

	public void exitGame(){
		Application.Quit();
	}
	public void loadNext(){
		SceneManager.LoadScene("Game");
	}
	public void openLeaderboard(){
		openIt("http://example.com");
	}

	public void SFXVolumeChaneged(){
		AudioController.instance.SFXVolume = SFXVolume.value/5f;
		_a.clip = AudioController.instance.pickupSFX[0];
		_a.volume = AudioController.instance.SFXVolume;
		_a.Play();
		PlayerPrefs.SetFloat("SFXVolume",SFXVolume.value);
	}
	public void ShowControls(){
		SceneManager.LoadScene("ShowControls",LoadSceneMode.Additive);
	}

	public void MusicVolumeChanged(){
		AudioController.instance.MusicVolume = musicVolume.value/5f;
		PlayerPrefs.SetFloat("MusicVolume",musicVolume.value); 
	}
	private void Start()
	{
		_a = GetComponent<AudioSource>();
		SFXVolume.value = PlayerPrefs.GetFloat("SFXVolume",0.5f);
		AudioController.instance.SFXVolume = SFXVolume.value/5f;

		musicVolume.value = PlayerPrefs.GetFloat("MusicVolume",0.5f);
		AudioController.instance.MusicVolume = musicVolume.value/5f;
	}
	[DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
		#if !UNITY_EDITOR && UNITY_WEBGL
			OpenNewTab(url);
		#else
			Application.OpenURL(url);
		#endif
    }
}
