using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Responce : UnityEvent<string, int>
{
}

public class CollectEventListener : MonoBehaviour
{
    [SerializeField]
    private CollectEvent collectEvent;
    [SerializeField]
    private Responce response;

    private string prizeName;
    private int prizeCount;

    private void Start()
    {
        response = new Responce();
        response.AddListener(GameObject.FindObjectOfType<ControlPanelCanvasScript>().GetParameterUpdate);
    }

    private void OnEnable()
    {
        collectEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        collectEvent.UnregisterListener(this);
    }

    public void OnEventRaised(ConstGameCtrl.PlanetSurprize ps, int value)
    {
        prizeName = System.Enum.GetName(typeof(ConstGameCtrl.PlanetSurprize), (int)ps);
        prizeCount = value;
        response.Invoke(prizeName, prizeCount);
    }
}
