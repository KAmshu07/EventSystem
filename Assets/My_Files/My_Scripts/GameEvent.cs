using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	///<summary>
	///The list of listeners that this event will notify if it is raised
	///</summary>

	private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();


	public void Raise()
	{
		// Creating a copy of the list in order to prevent modification while iterating 
		List<GameEventListener> listenersCopy = new List<GameEventListener>(eventListeners);

		// Iterating over the copied list and raising the events
		foreach (var listener in listenersCopy)
		{
			listener.OnEventRaised(this);
		}
	}

	public void RegisterListener(GameEventListener listener)
	{
		if (!eventListeners.Contains(listener))
		{
			eventListeners.Add(listener);
		}
	}

	public void UnregisterListener(GameEventListener listener)
	{
		if (eventListeners.Contains(listener))
		{
			eventListeners.Remove(listener);
		}
	}
}