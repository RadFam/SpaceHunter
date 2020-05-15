using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameLog : MonoBehaviour
{
    private List<string> Eventlog = new List<string>();
    private string guiText = "";
    private int maxLines = 20;

    void OnGUI()
    {
        GUI.Label(new Rect(10, Screen.height - (Screen.height / 1.8f), Screen.width / 4, Screen.height / 2), guiText, GUI.skin.textArea);
    }

    public void AddEvent(string eventString)
    {
        //Debug.Log("Event comes " + eventString);
        Eventlog.Add(eventString);

        if (Eventlog.Count >= maxLines)
            Eventlog.RemoveAt(0);

        guiText = "";

        foreach (string logEvent in Eventlog)
        {
            guiText += logEvent;
            guiText += "\n";
        }
    }
}
