using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthAndProgressionController : MonoBehaviour
{
    public Slider healthBar;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI burstShot;
	public TextMeshProUGUI explosiveShot;
	public TextMeshProUGUI healthBoost;
	public TextMeshProUGUI reboundLevel;


	public float difficulty = 0.5f;
	public float lerpSpeed = 1f;
	//private int score;

	private float score;
	private float maxHealth;
	private float curHealth;

	public int BurstshotLevel = 1;
	public int ExplosiveLevel = 1;
	public int HealthboostLevel = 1;
	public int ReboundLevel = 1;

	private bool hasBoosted = false;
	public int lastMultiple = 0;

	private bool isFading = false;
	private enum perks
	{
		Burstshot,
		ExplosiveBullets,
		Healthboost,
		Rebound
	};

    void Update()
    {
		score = Mathf.Lerp(score,ZeroGMovement.instance.score,lerpSpeed*Time.deltaTime);
		if (Mathf.Abs(ZeroGMovement.instance.score-score) <= 2f)
			score = ZeroGMovement.instance.score;
		scoreText.text = ((int)score).ToString();

		maxHealth = Mathf.Lerp(maxHealth,ZeroGMovement.instance.maxHealth,lerpSpeed*Time.deltaTime);
		if (Mathf.Abs(ZeroGMovement.instance.maxHealth-maxHealth) <= 2f)
			maxHealth = ZeroGMovement.instance.maxHealth;
		healthBar.maxValue = (int)maxHealth;

		curHealth = Mathf.Lerp(curHealth,ZeroGMovement.instance.currentHealth,lerpSpeed*Time.deltaTime);
		if (Mathf.Abs(ZeroGMovement.instance.currentHealth-curHealth) <= 2f)
			curHealth = ZeroGMovement.instance.currentHealth;
		healthBar.value = (int)curHealth;


		int multiple = Mathf.FloorToInt(ZeroGMovement.instance.score/1000f);
		if (multiple != lastMultiple)
		{
			int diff = multiple-lastMultiple;
			for (int i = 0; i<diff;i++)
			{
				newPerk();
			}
			lastMultiple = multiple;
		}
		
		if (ZeroGMovement.instance.currentHealth <= ZeroGMovement.instance.maxHealth*0.25)
		{
			if (!isFading)
				StartCoroutine(fadeToColor(Color.red));
			isFading = true;
		} else {
			isFading = false;
		}

		/* if (ZeroGMovement.instance.score % 1000 == 0 && ZeroGMovement.instance.score != 0 )
		{
			Debug.Log("a");
			if(!hasBoosted)
			{
				Debug.Log("Multiple");
				newPerk();
				hasBoosted = true;
			}
			
		} else {
			hasBoosted = false;
		} */

		burstShot.text = $"Burstshot {BurstshotLevel}";
		explosiveShot.text = $"Explosive {ExplosiveLevel}";
		healthBoost.text = $"Healthboost {HealthboostLevel}";
		reboundLevel.text = $"Rebound {ReboundLevel}";
		
    }

	private void newPerk()
	{
		perks perk = StaticHelpers.RandomEnumValue<perks>();
		StaticHelpers.difficulty += 0.01f;
		switch(perk){
			case perks.Burstshot:
				BurstshotLevel +=1;
				//BurstshotLevel = Mathf.Clamp(BurstshotLevel,1,15);
				ZeroGMovement.instance.maxShot = BurstshotLevel;
				break;
			case perks.ExplosiveBullets:
				ExplosiveLevel +=1;
				ExplosiveLevel = Mathf.Clamp(ExplosiveLevel,1,100);
				ZeroGMovement.instance.explosiveShotChance = ExplosiveLevel;
				break;
			case perks.Healthboost:
				HealthboostLevel +=1;
				HealthboostLevel = Mathf.Clamp(HealthboostLevel,1,30);
				ZeroGMovement.instance.currentHealth += (ZeroGMovement.instance.defaultMaxHealth * HealthboostLevel - ZeroGMovement.instance.currentHealth);
				ZeroGMovement.instance.maxHealth = ZeroGMovement.instance.defaultMaxHealth * HealthboostLevel;
				break;
			case perks.Rebound:
				ReboundLevel +=1;
				ReboundLevel = Mathf.Clamp(ReboundLevel,1,3);
				StaticHelpers.maxReboundPlayer = ReboundLevel;
				break;
		}

	}

	IEnumerator fadeToColor(Color c)
	{
		StaticHelpers.lightColor = Color.black;
		for (float i = 0f; i<=1f; i+=0.01f)
		{
			StaticHelpers.lightColor = c*i;
			yield return new WaitForFixedUpdate();
		}
		while (isFading){yield return new WaitForFixedUpdate();}
		Color t = Color.black;
		for (float i = 0f; i<=1f; i+=0.01f)
		{
			StaticHelpers.lightColor = Color.Lerp(StaticHelpers.lightColor,t,0.1f);
			yield return new WaitForFixedUpdate();
		}
	}
}
