using System;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
	public int lightIndex;
	private Light _l;

	private Vector3 nextLerp;
	private void Start()
	{
		lightIndex *= 2;
		transform.position = MovingLightTrain.coords[lightIndex];
		transform.position = new Vector3(transform.position.x,transform.position.y,-10f);
		_l = gameObject.AddComponent<Light>();
		_l.intensity = 250f*5f;
		_l.range = 20f*50f;
		nextLerp = MovingLightTrain.coords[lightIndex+1];
		nextLerp.z = -10f;
	}

	private void Update()
	{
		if(lightIndex>=16)
			lightIndex = 0;
		
		//transform.position = Vector3.Lerp(transform.position,nextLerp,0.2f*Time.deltaTime);
		Vector3 direction = nextLerp - transform.position;
		direction.Normalize();
		transform.position += direction*10f*Time.deltaTime;
		if (Vector3.Distance(transform.position,nextLerp)<=1f)
		{
			lightIndex++;
			if(lightIndex>=16)
				lightIndex = 0;
			nextLerp = MovingLightTrain.coords[lightIndex];
			nextLerp.z = -10f;
		}
		_l.color = StaticHelpers.lightColor;
	}


}
