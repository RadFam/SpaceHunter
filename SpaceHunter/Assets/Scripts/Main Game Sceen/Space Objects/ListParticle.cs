using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListParticle : MonoBehaviour 
{
    [Header("Plasma")]
    public List<GameObject> listFreeObjects_weak = new List<GameObject>();
    public List<GameObject> listBusyObjects_weak = new List<GameObject>();
    public List<GameObject> listFreeObjects_average = new List<GameObject>();
    public List<GameObject> listBusyObjects_average = new List<GameObject>();
    public List<GameObject> listFreeObjects_strong = new List<GameObject>();
    public List<GameObject> listBusyObjects_strong = new List<GameObject>();

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
            //CPCS.UpdatePlasma(listFreeObjects.Count, listFreeObjects.Count + listBusyObjects.Count);

            //CPCS.UpdatePlasma(listFreeObjects_weak.Count, listFreeObjects_weak.Count + listBusyObjects_weak.Count);
            //CPCS.UpdatePlasma(listFreeObjects_average.Count, listFreeObjects_average.Count + listBusyObjects_average.Count);
            //CPCS.UpdatePlasma(listFreeObjects_strong.Count, listFreeObjects_strong.Count + listBusyObjects_strong.Count);
        }
    }
}
