using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraEdge : MonoBehaviour
{
	private Camera localCamera;
	private Vector3 target;
	private Vector3 target1;
	private Vector3 target2;
	private Vector3 target3;
	private Vector3 target4;
	public float speed;


    void Start()
    {
		localCamera = Camera.main;
		target = localCamera.ViewportToWorldPoint(Vector3.zero);
		transform.position = target;
		target1 = localCamera.ViewportToWorldPoint(new Vector3(0,0));
		target2 = localCamera.ViewportToWorldPoint(new Vector3(0,1));
		target3 = localCamera.ViewportToWorldPoint(new Vector3(1,1));
		target4 = localCamera.ViewportToWorldPoint(new Vector3(1,0));
		target = target1;
		
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
		if (transform.position == target)
		{
			if (transform.position == target1)
				target = target2;
			else if (transform.position == target2)
				target = target3;
			else if (transform.position == target3)
				target = target4;
			else if (transform.position == target4)
				target = target1;
			else 
			{
				target = target2;
				transform.position = target1;
			}
				
		}
    }

	private void FixedUpdate()
	{
		target1 = localCamera.ViewportToWorldPoint(new Vector3(0,0));
		target2 = localCamera.ViewportToWorldPoint(new Vector3(0,1));
		target3 = localCamera.ViewportToWorldPoint(new Vector3(1,1));
		target4 = localCamera.ViewportToWorldPoint(new Vector3(1,0));
	}
}
