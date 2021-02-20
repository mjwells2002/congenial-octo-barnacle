using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeStartDelay : MonoBehaviour
{
	public GameObject[] objs;
	public float time;
	private void Start()
	{
		StartCoroutine(waitDelay());
	}

	IEnumerator waitDelay()
	{
		yield return new WaitForSeconds(time);
		objs.ToList().ForEach(x => x.SetActive(true));
	}
}