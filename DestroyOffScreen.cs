using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour 
{

	public float offset = 16.0f;
	public delegate void OnDestroy ();
	public event OnDestroy DestroyCallBack;

	private bool offscreen;
	private float offscreen_x = 0f;
	private Rigidbody2D body2d;

	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start ()
	{
		offscreen_x = (Screen.width / PixelPerfectCamera.pixels_to_unit) / 2 + offset;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var pos_x = transform.position.x;
		var dir_x = body2d.velocity.x;

		if (Mathf.Abs (pos_x) > offscreen_x)
		{
			if (dir_x < 0 && pos_x < -offscreen_x)
				offscreen = true;
			else if (dir_x > 0 && pos_x > offscreen_x)
				offscreen = true;
		}
		else
			offscreen = false;
		if (offscreen)
		{
			OnOutOfBounds ();
		}

	}

	public void OnOutOfBounds ()
	{
		offscreen = false;
		GameObjectUtil.Destroy (gameObject);

		if (DestroyCallBack != null) 
		{
			DestroyCallBack ();
		}
	}
}
