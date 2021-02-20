using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebGLWarning : MonoBehaviour
{
	public GameObject[] webGLWarnings;

    private void Start()
	{
		#if !UNITY_EDITOR && UNITY_WEBGL
			webGLWarnings.ToList().ForEach(x => x.SetActive(true));
		#else
			confirm();
		#endif
	}

	public void confirm()
	{
		SceneManager.LoadScene(1);
	}
}
