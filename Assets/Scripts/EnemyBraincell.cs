using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBraincell : MonoBehaviour
{
	private Rigidbody2D rb;
	private countdownTimer timer;
	private countdownTimer timer2;
	private Vector3 offset;
	private float Distance;

	public GameObject eExplosion;
	public float velocity = 250f;

	public GameObject eBullet;
	public float Health = 100f;

	private void Start()
	{
		StaticHelpers.eOnScreen++;
		rb = GetComponent<Rigidbody2D>();
		timer = new countdownTimer(Random.Range(0.2f,1f));
		timer2 = new countdownTimer(ZeroGMovement.EReloadspeed);
		float angle = Random.Range(0f,360f);
		transform.localRotation = Quaternion.Euler(0f,0f,angle);
		timer.fixedDelta = true;
		timer2.fixedDelta = true;
		Distance = 0f;
	}
	public void damage(Transform damagerTransform)
	{
		Vector2 direction = (transform.position-damagerTransform.position);
		direction.Normalize();
		rb.AddForce(direction*5f,ForceMode2D.Impulse);
		Health-=10;
	}

	private void FixedUpdate()
	{
		Distance = Mathf.Clamp(Vector2.Distance(transform.position,ZeroGMovement.playerInstance.position)/10f,0.5f,10f);
		timer2.maxTime = ZeroGMovement.EReloadspeed;
		if(timer.isTriggered)
		{
			rb.AddForce(randomDirection()*velocity,ForceMode2D.Impulse);
			offset = (Vector3)randomDirection()* (ZeroGMovement.instance.isInMotion ? Distance : Distance*0.7f);
		}
		if(timer2.isTriggered)
		{
			if(randomChance(ZeroGMovement.EFirespeed)){
				doFire();
			}
		}
		transform.up = ZeroGMovement.playerInstance.position - transform.position + offset;
		if(Health<=0f)
			Die();
	}

	private void Die()
	{
		Rigidbody2D a = Instantiate(eExplosion,transform.position,transform.rotation).GetComponent<Rigidbody2D>();
		StaticHelpers.cloneRigidbody(a,rb);
		StaticHelpers.eOnScreen--;	
		Destroy(gameObject);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBullet"))
		{
			other.gameObject.GetComponent<BulletLogic>().DestroyAfterTrail();
			//rb.AddForce(-transform.up*2f,ForceMode2D.Impulse);
			Vector2 direction = (transform.position-other.transform.position);
			direction.Normalize();
			rb.AddForce(direction*3f,ForceMode2D.Impulse);
			Health-=5;
		}
	}

	private Vector2 randomDirection()
	{
		Vector2 v = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
		v.Normalize();
		return v;
	}

	private void doFire()
	{
		GameObject b = Instantiate(eBullet,transform.position,transform.rotation);
		Rigidbody2D bR = b.GetComponent<Rigidbody2D>();
		bR.AddForce(b.transform.up*40f,ForceMode2D.Impulse);
		rb.AddForce(-transform.up*3f*(Distance*0.1f),ForceMode2D.Impulse);
	}

	private bool randomChance(int percentageChance)
	{
		int a = (int)Random.Range(0f,100f);
		return a<=percentageChance;
	}
}
