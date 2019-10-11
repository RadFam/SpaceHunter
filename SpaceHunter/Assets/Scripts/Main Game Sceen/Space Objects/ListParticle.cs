using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListParticle : MonoBehaviour 
{
    [Header("Plasma")]
    public List<GameObject> listFreeObjects = new List<GameObject>();
    public List<GameObject> listBusyObjects = new List<GameObject>();

    [Header("Explosion")]
    public List<GameObject> listFreeExplodes = new List<GameObject>();
    public List<GameObject> listBusyExplodes = new List<GameObject>();

    public ControlPanelCanvasScript CPCS;
    private bool canUpdateCanvas = false;

    void Start()
    { 
        CPCS = FindObjectOfType<ControlPanelCanvasScript>();
    }

    public bool lpUC
    {
        set {canUpdateCanvas = value;}
        get {return canUpdateCanvas;}
    }

    public void UpdateCanvas()
    {
        if (canUpdateCanvas)
        {
            CPCS.UpdatePlasma(listFreeObjects.Count, listFreeObjects.Count + listBusyObjects.Count);
        }
    }
}
