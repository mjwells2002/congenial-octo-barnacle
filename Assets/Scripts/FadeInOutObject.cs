using System.Security.AccessControl;
using System.IO.Pipes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FadeInOutObject : MonoBehaviour
{
	[Range(0f,1f)] public float progress = 0f;
	public float speed = 1f;

	private MeshRenderer meshRenderer;
	private Material original;
	private Material custom;
	private bool fadingIn = false;
	private bool fadingOut = false;
	private Color originalColor;
	private Color originalEmmisive;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
		original = meshRenderer.material;
		custom = new Material(meshRenderer.material);
		originalEmmisive = original.GetColor("_EmissionColor");
		originalColor = original.color;

		fadingIn = true;
		meshRenderer.material = custom;
		custom.color = Color.black;
		custom.SetColor("_EmissionColor",Color.black);
		StartCoroutine(fadeAfter(10f));
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingIn)
		{	
			progress+=Time.deltaTime*speed;
			custom.color *= progress;
			custom.SetColor("_EmissionColor",original.GetColor("_EmissionColor")*progress);
			if(progress>=1f){
				fadingIn = false;
			}
			
		}
		if(fadingOut)
		{
			progress-=Time.deltaTime*speed;
			custom.color *= progress;
			custom.SetColor("_EmissionColor",original.GetColor("_EmissionColor")*progress);
			if(progress<=0f){
				fadingOut = false;
				Destroy(gameObject);
			}
		}
    }

	IEnumerator fadeAfter(float time)
	{
		yield return new WaitForSeconds(time);
		fadingOut = true;
	}

}
