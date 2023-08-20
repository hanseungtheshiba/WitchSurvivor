using System;
using System.Collections.Generic;

public class EventManager
{
    public enum EventName
    {
        PLAYER_DAMAGED = 0,
        PLAYER_DEAD = 1,

        /// <summary>
        /// 군번줄 습득
        /// </summary>
        GET_DOGTAG = 10,

        /// <summary>
        /// UI 패널 보이기
        /// </summary>
        PANEL_SHOW = 100,
        /// <summary>
        /// UI 패널 숨기기
        /// </summary>
        PANEL_HIDE = 101
    }

    private static Dictionary<EventName, Action> eventDictionary = new Dictionary<EventName, Action>();

    public static void StartListening(EventName eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }       
    }

    public static void StopListening(EventName eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static bool IsListening(EventName eventName) 
    {
        if (eventDictionary == null) return false;
        return eventDictionary.ContainsKey(eventName);
    }

    public static void TriggerEvent(EventName eventName)
    {
        Action thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
