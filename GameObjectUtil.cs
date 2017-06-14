using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtil
{
	private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool> ();

	public static GameObject Instantiate(GameObject prefab, Vector3 pos)
	{
		GameObject instance = null;
		var recycled_script = prefab.GetComponent<RecycleGameObject> ();

		if (recycled_script != null) 
		{
			var pool = GetObjectPool (recycled_script);
			instance = pool.NextObject (pos).gameObject;
		} 
		else 
		{
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}

		return (instance);
	}

	public static void Destroy (GameObject game_object)
	{
		var recycle_game_object = game_object.GetComponent<RecycleGameObject> ();
		if (recycle_game_object != null)
			recycle_game_object.Shutdown ();
		else
			GameObject.Destroy (game_object);
	}

	private static ObjectPool GetObjectPool (RecycleGameObject reference)
	{
		ObjectPool pool = null;

		if (pools.ContainsKey (reference))
			pool = pools [reference];
		else 
		{
			var pool_container = new GameObject (reference.gameObject.name + "ObjectPool");
			pool = pool_container.AddComponent<ObjectPool> ();
			pool.prefab = reference;
			pools.Add (reference, pool);
		}

		return (pool);
	}
}
