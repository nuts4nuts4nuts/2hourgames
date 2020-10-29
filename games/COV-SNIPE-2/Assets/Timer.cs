using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private struct EventEntry
    {
        public float timeKey;
        public Action action;
    }

    // Currently unused
    private class EventEntryComparer : IComparer<EventEntry>
    {
        public int Compare(EventEntry x, EventEntry y)
        {
            if (x.timeKey > y.timeKey)
            {
                return 1;
            }

            if (x.timeKey < y.timeKey)
            {
                return -1;
            }

            return 0;
        }
    }

    List<EventEntry> events;

    public void Start()
    {
        events = new List<EventEntry>();
    }

    public void DoAfterDelay(float seconds, Action action)
    {
        var timeKey = Time.time + seconds;
        events.Add(new EventEntry
        {
            timeKey = timeKey,
            action = action
        });
    }

    // Update is called once per frame
    void Update()
    {
        var actionsToTrigger = events.Where(action => Time.time >= action.timeKey).ToList();
        foreach (var action in actionsToTrigger)
        {
            action.action.Invoke();
            events.Remove(action);
        }
    }
}
