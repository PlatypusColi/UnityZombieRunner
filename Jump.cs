using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	public float jump_speed = 240f;
	public float forward_speed = 20f;

	private Rigidbody2D body2d;
	private InputState input_state;
	// Use this for initialization
	void Awake()
	{
		body2d = GetComponent<Rigidbody2D> ();
		input_state = GetComponent<InputState> ();
	}
	// Update is called once per frame
	void Update () 
	{
		if (input_state.standing) 
		{
			if (input_state.action_button)
				body2d.velocity = new Vector2 (transform.position.x < 0 ? forward_speed : 0, jump_speed);
		}
	}
}
