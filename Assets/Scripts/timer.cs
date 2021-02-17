using System;
using UnityEngine;
public class countdownTimer
{
	private float remainingTime;
	public float maxTime;

	public bool autoReset = true;
	public bool autoCount = true;
	public bool fixedDelta = false;

	public countdownTimer(float time)
	{
		remainingTime = time;
		maxTime = time;

	}

	public bool isTriggered
	{
		get 
		{ 
			if (autoCount == true)
					remainingTime -= fixedDelta ? Time.fixedDeltaTime : Time.deltaTime;
			if (remainingTime <= 0)
			{
				if (autoReset == true)
					remainingTime = maxTime;
				return true;
			} else 
			{
				return false;
			}
		}
	}

	public void resetTimer()
	{
		remainingTime = maxTime;
	}
}

