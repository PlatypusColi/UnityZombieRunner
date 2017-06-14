using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecycle
{
	void Restart();
	void Shutdown();
}

public class RecycleGameObject : MonoBehaviour
{
	private List<IRecycle> recycle_components;

	void Awake()
	{
		var components = GetComponents<MonoBehaviour> ();
		recycle_components = new List<IRecycle> ();

		foreach (var component in components)
		{
			if (component is IRecycle)
				recycle_components.Add (component as IRecycle);
		}
	}

	public void Restart ()
	{
		gameObject.SetActive (true);
		foreach (var component in recycle_components) 
			component.Restart ();	
	}

	public void Shutdown ()
	{
		gameObject.SetActive (false);
		foreach (var component in recycle_components) 
			component.Restart ();	
	}
}
