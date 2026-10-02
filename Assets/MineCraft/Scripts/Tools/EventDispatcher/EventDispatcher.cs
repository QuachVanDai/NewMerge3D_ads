using UnityEngine;
using System.Collections;
using System;

public class EventDispatcher : PriorityEventDispatcher<EventName, object, EventTypeComparer> 
{
	static EventDispatcher instance = new EventDispatcher();

	public static EventDispatcher Instance {
		get { return instance; }
	}

    internal void AddListener(object eventn)
    {
        throw new NotImplementedException();
    }
}