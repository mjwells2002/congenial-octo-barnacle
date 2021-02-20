using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackUI : MonoBehaviour
{
	public void doDisable()
	{
		SceneManager.UnloadSceneAsync("ShowControls");
	}
}
