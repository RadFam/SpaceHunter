using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShoot : MonoBehaviour {

    public GameObject leftGun;
    public GameObject rightGun;
    public ListParticle allPlasmaShots;

    private float timer;
    private float deltaTime = 0.1f;
    // Use this for initialization
    void Start()
    {
        timer = deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // условие для стрельбы
        {
            if (timer >= deltaTime)
            {
                timer = 0.0f;

                if (allPlasmaShots.listFreeObjects.Count >= 2) // ЧТобы сразу два снаряда выпустить
                {
                    PlasmaShot leftPlasma = allPlasmaShots.listFreeObjects[0].GetComponent<PlasmaShot>();
                    PlasmaShot rightPlasma = allPlasmaShots.listFreeObjects[1].GetComponent<PlasmaShot>();
                    leftPlasma.gameObject.SetActive(true);
                    leftPlasma.SetCoords(leftGun.transform.position, leftGun.transform.rotation);
                    rightPlasma.gameObject.SetActive(true);
                    rightPlasma.SetCoords(rightGun.transform.position, rightGun.transform.rotation);
                }
            }
        }

        timer += Time.deltaTime;
    }
}
