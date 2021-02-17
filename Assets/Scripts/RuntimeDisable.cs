using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDisable : MonoBehaviour
{
    private void Awake()
	{
		gameObject.SetActive(false);
	}
}
