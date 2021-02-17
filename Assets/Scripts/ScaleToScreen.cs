using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreen : MonoBehaviour
{
	private Vector3 localScale;
	private Camera localCamera;
    void Start()
    {
		localScale = new Vector3(1,1,1);
		localCamera = Camera.main;
    }

    private void FixedUpdate()
	{
		localScale.x = Mathf.Abs(localCamera.ViewportToWorldPoint(Vector3.zero).x)*2f;
		localScale.y = Mathf.Abs(localCamera.ViewportToWorldPoint(Vector3.one).y)*2f;
		transform.localScale = localScale;
	}
}
