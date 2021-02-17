using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleController : MonoBehaviour
{
    private ParticleSystem _particle;

	public GameObject pickupObject;
	private void Start()
	{
		_particle = GetComponent<ParticleSystem>();
		for(int i = 0; i<=Random.Range(3,11); i++)
		//for(int i = 0; i<=100; i++)
		{
			Rigidbody2D a = Instantiate(pickupObject,transform.position,Quaternion.identity).GetComponent<Rigidbody2D>();
			a.AddForce(randomDirection()*5f,ForceMode2D.Impulse);
		}


		StartCoroutine(waitForLastParticle());

	}

	private Vector2 randomDirection()
	{
		Vector2 v = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
		v.Normalize();
		return v;
	}
	IEnumerator waitForLastParticle()
	{
		for (;;){
			yield return new WaitForFixedUpdate();
			if(_particle.particleCount == 0f)
				Destroy(gameObject);
		}
	}
}
