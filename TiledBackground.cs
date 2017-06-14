using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour 
{

	public int texture_size = 32;
	public bool scale_horizontally = true;
	public bool scale_vertically = true;

	// Use this for initialization
	void Start ()
	{
		var new_width = scale_horizontally == false ? 1 : Mathf.Ceil (Screen.width / (texture_size * PixelPerfectCamera.scale));
		var new_height = scale_vertically == false ? 1 : Mathf.Ceil (Screen.height / (texture_size * PixelPerfectCamera.scale));

		transform.localScale = new Vector3 (new_width * texture_size, new_height * texture_size, 1);

		GetComponent<Renderer> ().material.mainTextureScale = new Vector3 (new_width, new_height, 1);
	}

}
