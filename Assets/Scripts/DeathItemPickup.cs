using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathItemPickup : MonoBehaviour
{
    private Rigidbody2D rb;
	private Vector2 direction = Vector2.zero;
	private Transform ZeroGT;
	private countdownTimer targetTimer;
	private countdownTimer initalRBShutoffTimer;
	private float distance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		ZeroGT = ZeroGMovement.instance.transform;
		targetTimer = new countdownTimer(0.1f);
		targetTimer.fixedDelta = true;
		initalRBShutoffTimer = new countdownTimer(10f);
		initalRBShutoffTimer.autoReset = false;
    }
	private void Update()
	{
		if(initalRBShutoffTimer.isTriggered)
		{
			if(targetTimer.isTriggered)
			{
				distance = Vector2.Distance(transform.position,ZeroGMovement.instance.transform.position);
				if (distance >= 40f)
					rb.Sleep();
			}
			
		}
	}
	private void FixedUpdate()
	{
	
		distance = Vector2.Distance(transform.position,ZeroGMovement.instance.transform.position);
		if (distance<=40f)
		{
			//transform.position - ZeroGMovement.instance.transform.position
			direction.x =  transform.position.x - ZeroGT.position.x;
			direction.y =  transform.position.y - ZeroGT.position.y;				
			direction.Normalize();
			rb.velocity = -direction*50f;
		}
		
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.transform.tag == "Wall")
		{

			if(other.gameObject.name.Contains("Wall"))
			{
				//verticle wall
				rb.velocity = new Vector2(rb.velocity.x*-1,rb.velocity.y);
			} else {
				//horizontal wall
				rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*-1);
			}
			
		}
		if (other.CompareTag("Player"))
		{
			
			ZeroGMovement.instance.eat();
			Destroy(gameObject);
		}
	}
}
