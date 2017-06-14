using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour
{
	public bool action_button;
	public float abs_vel_x = 0f;
	public float abs_vel_y = 0f;
	public bool standing;
	public float standing_threshold = 1;

	private Rigidbody2D body2d;

	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		action_button = Input.anyKeyDown;
	}

	void FixedUpdate ()
	{
		abs_vel_x = System.Math.Abs (body2d.velocity.x);
		abs_vel_y = System.Math.Abs (body2d.velocity.y);

		standing = abs_vel_y <= standing_threshold;
	}
}
