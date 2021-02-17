using System.Drawing;
using System.IO.Pipes;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightHueShift : MonoBehaviour
{
    public Color baseColor;
	public float fadeSpeed = 1f;
	
	public Color currentColor;
	private colourHSV hsv;
	private Light localLight;
	private void Start()
	{
		hsv = new colourHSV();
		localLight = GetComponent<Light>();
		Color.RGBToHSV(baseColor, out hsv.h, out hsv.s, out hsv.v);
	}

	private void Update()
	{
		hsv.h += Time.deltaTime*fadeSpeed;
		if (hsv.h >= 1f)
			hsv.h = 0f;
		currentColor = hsv.toColor();
		localLight.color = currentColor;
	}

}

public class colourHSV
{
	public float h;
	public float s;
	public float v;

	public Color toColor()
	{
		return Color.HSVToRGB(h,s,v);
	}

}