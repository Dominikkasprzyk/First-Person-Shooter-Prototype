using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GameEvent class represents generic scriptable object game event without parameters. 
/// </summary>
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// List of listeners subscribing to this event.
    /// </summary>
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    /// <summary>
    /// Notify listeners that the event happened. 
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="data">Data passed with the event.</param>
    public void Raise(Component sender, object data)
    {
        for (int listenerIndex = _listeners.Count - 1; listenerIndex >= 0; listenerIndex--)
        {
            _listeners[listenerIndex].OnEventRaised(sender, data);
        }
    }

    /// <summary>
    /// Add listener to the list of subscribers.
    /// </summary>
    /// <param name="_newListener">Listener to add.</param>
    public void AddListener(GameEventListener _newListener)
    {
        _listeners.Add(_newListener);
    }

    /// <summary>
    /// Removes listener from the list of subsribers.
    /// </summary>
    /// <param name="_removedListener">Listener to remove.</param>
    public void RemoveListener(GameEventListener _removedListener)
    {
        _listeners.Remove(_removedListener);
    }
}
