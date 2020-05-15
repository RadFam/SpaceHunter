using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collect Event", menuName = "ScriptableObjects/Collect Event", order = 5)]
public class CollectEvent : ScriptableObject
{
    private List<CollectEventListener> listeners = new List<CollectEventListener>();

    public void Raise(ConstGameCtrl.PlanetSurprize ps, int value)
    {
        //Debug.Log("Listeners count: " + listeners.Count.ToString());
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(ps, value);
        }
    }

    public void RegisterListener(CollectEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(CollectEventListener listener)
    {
        listeners.Remove(listener);
    }
}