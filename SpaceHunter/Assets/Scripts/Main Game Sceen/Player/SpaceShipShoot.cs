﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShoot : MonoBehaviour {

    public GameObject leftGun;
    public GameObject rightGun;
    public ListParticle allPlasmaShots;

    public bool isDead = false;

    private float timer;
    private float deltaTime = 0.1f;
    // Use this for initialization
    void Start()
    {
        allPlasmaShots.lpUC = true;
        timer = deltaTime;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {

            if (Input.GetKey(KeyCode.Space)) // условие для стрельбы
            {
                if (timer >= deltaTime)
                {
                    timer = 0.0f;

                    if (allPlasmaShots.listFreeObjects_weak.Count >= 2) // ЧТобы сразу два снаряда выпустить
                    {
                        PlasmaShot leftPlasma = allPlasmaShots.listFreeObjects_weak[0].GetComponent<PlasmaShot>();
                        PlasmaShot rightPlasma = allPlasmaShots.listFreeObjects_weak[1].GetComponent<PlasmaShot>();
                        leftPlasma.gameObject.SetActive(true);
                        leftPlasma.SetCoords(leftGun.transform.position, leftGun.transform.rotation);
                        allPlasmaShots.UpdateCanvas();
                        rightPlasma.gameObject.SetActive(true);
                        rightPlasma.SetCoords(rightGun.transform.position, rightGun.transform.rotation);
                        allPlasmaShots.UpdateCanvas();
                    }
                }
            }

            timer += Time.deltaTime;
        }
    }
}
