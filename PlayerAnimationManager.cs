using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
	private Animator animator;
	private InputState input_state;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		input_state = GetComponent<InputState> ();
	}
	// Update is called once per frame
	void Update ()
	{
		var running = true;

		if (input_state.abs_vel_x > 0 && input_state.abs_vel_y < input_state.standing_threshold)
			running = false;

		animator.SetBool ("Running", running);
	}
}
