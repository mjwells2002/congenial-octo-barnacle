using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
	public static AudioController instance;
	public AudioClip[] backgroundMusic;
	public AudioClip[] laserSFX;
	public AudioClip[] explosionSFX;
	public AudioClip[] pickupSFX;
	
	[Range(0f,1f)] public float SFXVolume = 0.01f;
	[Range(0f,1f)] public float MusicVolume = 0.2f;

	private AudioSource _s;
	private int index = 0;
	void Start()
    {
		instance = this;
        _s = GetComponent<AudioSource>();
		_s.loop = false;
		_s.clip = backgroundMusic[Random.Range(0,backgroundMusic.Length)];
		_s.Play();
		DontDestroyOnLoad(this);
    }

	private void Update()
	{
		_s.volume = MusicVolume;
		if(!_s.isPlaying)
		{
			index++;
			if (index>=backgroundMusic.Length)
				index = 0;
			_s.clip = backgroundMusic[index];
			_s.Play();
		}
	}

    
}
