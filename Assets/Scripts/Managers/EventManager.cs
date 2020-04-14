using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class EventManager : Singleton<EventManager>
{
    [Serializable]
    public class GameEvent : UnityEvent<object[]> { };

    private Dictionary<string, GameEvent> _eventDictionary = new Dictionary<string, GameEvent>();

    /// <summary>Добавляем "слушателя" для события</summary>
    /// <param name="eventName">Название события</param>
    /// <param name="listener">Метод-обработчик события</param>
    public static void Subscribe(string eventName, UnityAction<object[]> listener)
    {
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>Удаляем "слушателя" из списка</summary>
    /// <param name="eventName">Название события</param>
    /// <param name="listener">Метод-обработчик события</param>
    public static void Unsubscribe(string eventName, UnityAction<object[]> listener)
    {
        if (Instance == null)
            return;
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.RemoveListener(listener);
    }

    /// <summary>Отправить событие</summary>
    /// <param name="eventName">Название события</param>
    /// <param name="parameters">Параметр массив</param>
    public static void SendEvent(string eventName, params object[] parameters)
    {
        GameEvent thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(parameters);
        }
    }
}