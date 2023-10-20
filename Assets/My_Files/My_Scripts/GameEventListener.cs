using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

[System.Serializable]
public class EventResponse
{
	public GameEvent Event;
	public UnityEvent Response;
}

public class GameEventListener : MonoBehaviour
{
[Tooltip("Events and their associated responses")]
	public List<EventResponse> EventResponses;

	private void OnEnable()
	{
		foreach(var eventResponse in EventResponses)
		{
			eventResponse.Event.RegisterListener(this);
		}
	}

	private void OnDisable()
	{
		foreach(var eventResponse in EventResponses)
		{
			eventResponse.Event.UnregisterListener(this);
		}
	}

	public void OnEventRaised(GameEvent raisedEvent)
	{
		foreach(var eventResponse in EventResponses)
		{
			if(eventResponse.Event == raisedEvent)
			{
				eventResponse.Response.Invoke();
			}
		}
	}
}