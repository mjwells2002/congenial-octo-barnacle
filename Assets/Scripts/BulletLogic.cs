using System.Linq;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public int totalBounces = 1;

	private ParticleSystem _particle;
	private int usedBounces;
	private Rigidbody2D rb;

	public bool isExplsive = false;
	public GameObject explosiveParticles;
	public GameObject explosiveParticlesTrail;
	public float explosionRaidus = 10f;
	public void DestroyAfterTrail()
	{
		if(_particle == null || rb == null)
			Start();
		if(isExplsive)
		{
			explosiveParticles.SetActive(true);
			Collider2D[] a = Physics2D.OverlapCircleAll(transform.position,explosionRaidus);
			a.ToList().ForEach(collider => {
				if(collider.CompareTag("BadGuy")){
					collider.gameObject.GetComponent<EnemyBraincell>().damage(transform);
				}
			});
		}
		_particle.Stop();
		rb.velocity = Vector2.zero;
		StartCoroutine(waitForTail());
	}

	IEnumerator waitForTail(){
		for (;;)
		{
			yield return new WaitForFixedUpdate();
			if (_particle.particleCount == 0)
				Destroy(gameObject);
		}
	}

	private void Start()
	{
		if(isExplsive)
			explosiveParticlesTrail.SetActive(isExplsive);
		if(explosiveParticlesTrail != null)
			totalBounces = StaticHelpers.maxReboundPlayer;
		
		rb = GetComponent<Rigidbody2D>();
		_particle = GetComponent<ParticleSystem>();
		usedBounces = totalBounces;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.transform.tag == "Wall")
		{
			
			if (usedBounces <= 0f)
				StartCoroutine(waitDestroy());
			else {
				if(other.gameObject.name.Contains("Wall"))
				{
					//verticle wall
					rb.velocity = new Vector2(rb.velocity.x*-1,rb.velocity.y);
					usedBounces--;
				} else {
					//horizontal wall
					rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*-1);
					usedBounces--;
				}
			}
		}
	}

	IEnumerator waitDestroy()
	{
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}
}

