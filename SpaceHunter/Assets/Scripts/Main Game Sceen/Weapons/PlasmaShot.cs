﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShot : MonoBehaviour {

    public ListParticle LP;
    public MeshRenderer MR;
    public PlasmaShotEffect PSE;

    public float timer = 0.0f;
    public float timeToLive = 3.0f;

    public void OnEnable()
    {
        timer = 0.0f;

        LP.listBusyObjects.Add(gameObject);
        LP.listFreeObjects.RemoveAt(0);
        MR.enabled = true;
    }

    public void SetCoords(Vector3 position, Quaternion rotation)
    {
        PSE.SetPosition(position, rotation);
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
        LP.listFreeObjects.Add(gameObject);
        LP.listBusyObjects.RemoveAt(0);
        gameObject.SetActive(false);
    }
}