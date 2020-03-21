﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPortalScript : MonoBehaviour
{
    public PlanetCountPortals myPlanetCtrl;
    public Collider myCheckCollider;
    public Texture changeTexture;
    public Texture changeTexture_init;
    protected Renderer myRenderer;
    protected int myNum;
    protected bool isPassed;

    public GameObject warp1;
    public GameObject warp2;
    public GameObject warp3;

    protected float speedRot1 = -15.0f;
    protected float speedRot2 = 12.0f;
    protected float speedRot3 = 25.0f;

    [SerializeField]
    protected float rechargeTime = 60.0f;
    protected float timer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void SetParams(int num, PlanetCountPortals pcp, Vector3 position, Vector3 localAngles)
    {
        myNum = num;
        myPlanetCtrl = pcp;
        isPassed = false;
        gameObject.transform.localPosition = position;
        gameObject.transform.localEulerAngles = localAngles;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPassed)
        {
            OnCharge();
        }
    }

    void Update()
    {
        warp1.transform.RotateAround(warp1.transform.position, warp1.transform.forward, speedRot1 * Time.deltaTime);
        warp2.transform.RotateAround(warp2.transform.position, warp2.transform.forward, speedRot2 * Time.deltaTime);
        warp3.transform.RotateAround(warp3.transform.position, warp3.transform.forward, speedRot3 * Time.deltaTime);

        if (isPassed)
        {
            timer += Time.deltaTime;

            if (timer >= rechargeTime)
            {
                timer = 0.0f;
                OffCharge();
            }
        }
    }

    public void OnCharge()
    {
        if (isPassed)
        {
            timer = 0.0f;
        }
        else
        {
            isPassed = true;
            myPlanetCtrl.SendSignal(myNum);
            myCheckCollider.enabled = false;
        }

        // Change texture
        myRenderer.material.SetTexture("_MainTex", changeTexture);
    }

    public void OffCharge()
    {
        isPassed = false;
        myCheckCollider.enabled = true;
        myPlanetCtrl.SendBackSignal(myNum);
        myRenderer.material.SetTexture("_MainTex", changeTexture_init);
    }
}
