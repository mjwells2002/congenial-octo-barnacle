using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum screenEdges
{
	Top,
	Bottom,
	Left,
	Right
}

public class MovingLightTrain : MonoBehaviour
{
	public static Vector2[] coords;
	private Camera localCamera;
	public float maxLights = 8;
	private void Start()
	{
		localCamera = Camera.main;
		coords = new Vector2[16];
		populateList();

		for (int i = 0; i<maxLights; i++)
		{
			GameObject g = new GameObject("MovingLight");
			g.transform.parent = transform;
			g.AddComponent<MovingLight>().lightIndex = i;

		}
		/* coords.ToList().ForEach(x => {
			GameObject g = new GameObject("LightTest");
			Light a = g.AddComponent<Light>();
			a.intensity = 250f;
			a.range = 20f;
			g.transform.position = new Vector3(x.x,x.y,-10f);
		});  */
	}

	private void populateList()
	{
		foreach (screenEdges screenEdge in Enum.GetValues(typeof(screenEdges)))
		{
			int oI;
			switch (screenEdge){
				case screenEdges.Top:
					oI = 0;
					for (int i = 0; i<5; i++){
						//0,1,2,3,4
						
						coords[i+oI] = localCamera.ViewportToWorldPoint(new Vector2(1-Mathf.Abs(i-4)/4f,1f));
					}
					break;
				case screenEdges.Right:
					oI = 5-1;
					for (int i = 0; i<5; i++){
						//9,10,11,12,13
						
						coords[i+oI] = localCamera.ViewportToWorldPoint(new Vector2(1f,Mathf.Abs(i-4)/4f));
					}
					break;
				case screenEdges.Bottom:
					oI = 9-1;
					for (int i = 0; i<4; i++){
						//5,6,7,8,9
						
						coords[i+oI] = localCamera.ViewportToWorldPoint(new Vector2(Mathf.Abs(i-4)/4f,0f));
					}
					break;
				
				case screenEdges.Left:
					oI = 14-2;
					for (int i = 0; i<4; i++){
						//14,15,16,17,19
						
						coords[i+oI] = localCamera.ViewportToWorldPoint(new Vector2(0f,1-Mathf.Abs(i-4)/4f));
					}
					break;
			}
		}
		

	}
}


