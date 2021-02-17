using System.Security.Cryptography;
using Microsoft.CSharp.RuntimeBinder;
using UnityEngine;
using System;
using System.Collections;

public static class StaticHelpers
{
	public static int maxReboundPlayer = 1;
	public static int eOnScreen = 0;
	public static Color lightColor = Color.black;
	public static float difficulty = 1f;
	static System.Random _R = new System.Random ();
	public static void cloneRigidbody(Rigidbody2D a,Rigidbody2D rb){
		a.velocity = rb.velocity;
		a.angularVelocity = rb.angularVelocity;
		a.angularDrag = rb.angularDrag;
		a.drag = rb.drag;
		a.gravityScale = rb.gravityScale;
		a.inertia = rb.inertia;
		a.mass = rb.mass;
		a.centerOfMass = rb.centerOfMass;
		a.freezeRotation = rb.freezeRotation;
		a.hideFlags = rb.hideFlags;
		a.interpolation = rb.interpolation;
		a.collisionDetectionMode = rb.collisionDetectionMode;
		a.simulated = rb.simulated;
	}

	public static T RandomEnumValue<T> ()
	{
		var v = Enum.GetValues (typeof (T));
		return (T) v.GetValue (_R.Next(v.Length));
	}

	
	
}