using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Цель этого скрипта - в случае стрельбы вызывать плазменные снаряды из пула объектов

public class EnemyShipBattleAI : MonoBehaviour {

    public Transform leftGun;
    public Transform rightGun;
    public ListParticle enemyPlasmaShots;
    
    private float timer;
    private float deltaTime = 0.5f;
    private PlasmaShot leftPlasma;
    private PlasmaShot rightPlasma;
    private bool canShoot = false;
    
    // Use this for initialization
	void Start () 
    {
        timer = deltaTime;
        enemyPlasmaShots = FindObjectOfType<ListParticle>(); // Вообще-то не то
	}

    public void Makeshoot(bool val)
    {
        canShoot = val;
        if (!canShoot)
        {
            timer = deltaTime;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (canShoot)
        {

            if (timer >= deltaTime)
            {
                timer = 0.0f;

                if (enemyPlasmaShots.listFreeObjects.Count >= 2) // ЧТобы сразу два снаряда выпустить
                {
                    leftPlasma = enemyPlasmaShots.listFreeObjects[0].GetComponent<PlasmaShot>();
                    rightPlasma = enemyPlasmaShots.listFreeObjects[1].GetComponent<PlasmaShot>();
                    leftPlasma.gameObject.SetActive(true);
                    leftPlasma.SetCoords(leftGun.transform.position, leftGun.transform.rotation);
                    rightPlasma.gameObject.SetActive(true);
                    rightPlasma.SetCoords(rightGun.transform.position, rightGun.transform.rotation);
                }
            }

            timer += Time.deltaTime;
        }
	}
}