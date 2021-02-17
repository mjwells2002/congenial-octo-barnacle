using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBounceBorderToScreen : MonoBehaviour
{
    private Vector3 t;
	private Vector3 b;
	private Vector3 l;
	private Vector3 r;
	private Vector2 scale;
	private Camera localCamera;

	public Vector2 offset = new Vector2(0.5f,0.5f);
	public Transform bottomObject;
	public Transform topObject;
	public Transform leftObject;
	public Transform rightObject;

    void Start()
    {
		localCamera = Camera.main;
        
		FixedUpdate();
    }

	private void FixedUpdate()
	{
        t = localCamera.ViewportToWorldPoint(new Vector3(0.5f,1));
		b = localCamera.ViewportToWorldPoint(new Vector3(0.5f,0));
		l = localCamera.ViewportToWorldPoint(new Vector3(0,0.5f));
		r = localCamera.ViewportToWorldPoint(new Vector3(1,0.5f));
		t.y += offset.y;
		b.y -= offset.y;
		l.x -= offset.x;
		r.x += offset.x;
		t.z = -1f;
		b.z = -1f;
		l.z = -1f;
		r.z = -1f;
		topObject.transform.position = t;
		bottomObject.transform.position = b;
		leftObject.transform.position = l;
		rightObject.transform.position = r;
		scale = localCamera.ViewportToWorldPoint(new Vector3(1f,1f))*2f;
		topObject.transform.localScale = new Vector3(scale.x,1f);
		bottomObject.transform.localScale = new Vector3(scale.x,1f);
		leftObject.transform.localScale = new Vector3(1f,scale.y);
		rightObject.transform.localScale = new Vector3(1f,scale.y);
	}
}
