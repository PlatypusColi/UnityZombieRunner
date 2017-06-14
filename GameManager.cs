using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
	public GameObject player_prefab;
	public Text continue_text;
	public Text score_text;

	public float time_elapsed = 0f;
	public float best_time = 0f;
	public float blink_time = 0f;
	public bool blink;
	private bool game_started;
	private TimeManager time_manager;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;
	private bool beat_best_time;

	void Awake ()
	{
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		time_manager = GetComponent<TimeManager> ();
	}

	void Start () 
	{
		var floor_height = floor.transform.localScale.y;
		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixels_to_unit) / 2) + (floor_height / 2);
		floor.transform.position = pos;

		spawner.active = false;

		Time.timeScale = 0;
		continue_text.text = "PRESS ANY BUTTON TO START";

		best_time = PlayerPrefs.GetFloat ("BestTime");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (game_started != true && Time.timeScale == 0) 
		{
			if (Input.anyKeyDown) 
			{
				time_manager.ManipulateTime (1, 1f);
				ResetGame ();
			}
		}

		if (!game_started) {
			++blink_time;

			if (blink_time % 40 == 0)
				blink = !blink;
			continue_text.canvasRenderer.SetAlpha (blink ? 0 : 1);

			var text_color = beat_best_time ? "#FF0" : "#FFF";
				
			score_text.text = "TIME: " + FormatTime (time_elapsed) + "\n" + "<color=" + text_color + ">BEST: " + FormatTime (best_time) + "</color>";
		} 
		else
		{
			time_elapsed += Time.deltaTime;
			score_text.text = "TIME: " + FormatTime (time_elapsed);

		}
	}

	void OnPlayerKilled ()
	{
		spawner.active = false;
		var player_destroy_script = player.GetComponent<DestroyOffScreen> ();
		player_destroy_script.DestroyCallBack -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		time_manager.ManipulateTime (0, 5.5f);
		game_started = false;

		continue_text.text = "PRESS ANY BUTTON TO RESTART";

		if (time_elapsed > best_time) 
		{
			best_time = time_elapsed;
			PlayerPrefs.SetFloat ("BestTime", best_time);
			beat_best_time = true;
		}
	}

	void ResetGame ()
	{
		spawner.active = true;

		player = GameObjectUtil.Instantiate(player_prefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixels_to_unit) / 2 + 100, 0));

		var player_destroy_script = player.GetComponent<DestroyOffScreen> ();
		player_destroy_script.DestroyCallBack += OnPlayerKilled;

		game_started = true;
		continue_text.canvasRenderer.SetAlpha (0);
		time_elapsed = 0;
		beat_best_time = false;
	}

	string FormatTime (float value)
	{
		TimeSpan t = TimeSpan.FromSeconds (value);

		return (string.Format ("{0:D2}:{1:D2}", t.Minutes, t.Seconds));
	}
}
