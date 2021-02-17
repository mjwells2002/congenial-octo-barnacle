using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeZero : MonoBehaviour
{
	private void Awake()
	{
		transform.position = Vector3.zero;
	}
}
