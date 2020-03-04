using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceType
{ Weak, Average, Strong}

public class PlasmaShot : MonoBehaviour {

    public ListParticle LP;
    public MeshRenderer MR;
    public PlasmaShotEffect PSE;

    public float timer = 0.0f;
    public float timeToLive = 3.0f;
    public ForceType myType;

    public void OnEnable()
    {
        timer = 0.0f;
        if (myType == ForceType.Weak)
        {
            LP.listBusyObjects_weak.Add(gameObject);
            LP.listFreeObjects_weak.RemoveAt(0);
        }
        if (myType == ForceType.Average)
        {
            LP.listBusyObjects_average.Add(gameObject);
            LP.listFreeObjects_average.RemoveAt(0);
        }
        if (myType == ForceType.Strong)
        {
            LP.listBusyObjects_strong.Add(gameObject);
            LP.listFreeObjects_strong.RemoveAt(0);
        }

        MR.enabled = true;
    }

    public void SetCoords(Vector3 position, Quaternion rotation)
    {
        PSE.SetPosition(position, rotation);
    }

    public void PlayEffect(Vector3 coord)
    {
        PlasmaExplosionEffect pee = LP.listFreeExplodes[0].GetComponent<PlasmaExplosionEffect>();
        pee.gameObject.SetActive(true);
        pee.SetCoords(coord, -1 * gameObject.transform.forward);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (timer >= timeToLive)
        {
            EndAll();
        }

        timer += Time.deltaTime;
	}

    public void EndAll()
    {
        MR.enabled = false;
        if (myType == ForceType.Weak)
        {
            LP.listFreeObjects_weak.Add(gameObject);
            LP.listBusyObjects_weak.RemoveAt(0);
        }
        if (myType == ForceType.Average)
        {
            LP.listFreeObjects_average.Add(gameObject);
            LP.listBusyObjects_average.RemoveAt(0);
        }
        if (myType == ForceType.Strong)
        {
            LP.listFreeObjects_strong.Add(gameObject);
            LP.listBusyObjects_strong.RemoveAt(0);
        }
        
        LP.UpdateCanvas();
        gameObject.SetActive(false);
    }
}
