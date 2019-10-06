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
}
