using System.Security.Cryptography;
using Microsoft.CSharp.RuntimeBinder;
using UnityEngine;
using System;
using System.Collections;

public static class Pterodyactl
{
	public static int maxReboundPlayerPterodyactl = 1;
	public static int eOnScreenPterodyactl = 0;
	public static int eMaxPterodyactl = 10;
	public static Color lightColorPterodyactl = Color.black;
	public static float difficultyPterodyactl = 0.5f;
	public static int finalScorePterodyactl;
	static System.Random _RPterodyactl = new System.Random ();
	public static void cloneRigidbodyPterodyactl(Rigidbody2D a,Rigidbody2D rb){
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

	public static T RandomEnumValuePterodyactl<T> ()
	{
		var v = Enum.GetValues (typeof (T));
		return (T) v.GetValue (_RPterodyactl.Next(v.Length));
	}

	
	
}