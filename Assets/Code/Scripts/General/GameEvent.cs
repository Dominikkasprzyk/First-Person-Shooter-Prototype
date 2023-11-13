using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for(int listenerIndex = listeners.Count - 1; listenerIndex >= 0; listenerIndex --)
        {
            listeners[listenerIndex].OnEventRaised();
        }
    }

    public void AddListener(GameEventListener _newListener)
    {
        listeners.Add(_newListener);
    }

    public void RemoveListener(GameEventListener _removedListener)
    {
        listeners.Remove(_removedListener);
    }
}
