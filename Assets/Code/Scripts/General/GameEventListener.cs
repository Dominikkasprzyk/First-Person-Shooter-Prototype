using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// The CustomGameEvent class allows for passing data through the events.
/// </summary>
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

/// <summary>
/// The GameEventListener class can be attached to the GameObejcts to listen
/// for specific events and respond with CustomGameEvent. 
/// </summary>
public class GameEventListener : MonoBehaviour
{
    /// <summary>
    /// GameEvent to listen for.
    /// </summary>
    public GameEvent gameEvent;
    /// <summary>
    /// Response to the GameEvent.
    /// </summary>
    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    /// <summary>
    /// Invokes the response when the event is detected.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="data">Data passed.</param>
    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
