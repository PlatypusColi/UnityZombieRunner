using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour, IRecycle
{
	public Sprite[] sprites;
	public Vector2 collider_offset = Vector2.zero;

	public void Restart ()
	{
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprites [Random.Range (0, sprites.Length)];

		var collider = GetComponent<BoxCollider2D> ();
		var size = renderer.bounds.size;
		size.y += collider_offset.y;

		collider.size = size;
		collider.offset = new Vector2 (-collider_offset.x, collider.size.y / 2 - collider_offset.y);
	}

	public void Shutdown ()
	{
	}
}
