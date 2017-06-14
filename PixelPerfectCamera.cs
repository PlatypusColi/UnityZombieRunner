using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour 
{

	public static float pixels_to_unit = 1f;
	public static float scale = 1f;

	public Vector2 native_resolution = new Vector2 (240, 160);

	void Awake () 
	{
		var camera = GetComponent<Camera> ();

		if (camera.orthographic)
		{
			scale = Screen.height / native_resolution.y;
			pixels_to_unit += scale;
			camera.orthographicSize	= (Screen.height / 2.0f) / pixels_to_unit;
		}
	}
}
