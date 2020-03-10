using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Цель этого скрипта - в случае стрельбы вызывать плазменные снаряды из пула объектов

public class EnemyShipBattleAI : MonoBehaviour {

    public List<Transform> weapon_weak = new List<Transform>();
    public List<Transform> weapon_average = new List<Transform>();
    public List<Transform> weapon_strong = new List<Transform>();

    public ListParticle enemyPlasmaShots;

    private float timer_1, timer_2, timer_3;
    [SerializeField]
    private float deltaTime_1 = 0.5f;
    [SerializeField]
    private float deltaTime_2 = 0.0f;
    [SerializeField]
    private float deltaTime_3 = 0.0f;
    private List<PlasmaShot> plasma_weak = new List<PlasmaShot>();
    private List<PlasmaShot> plasma_average = new List<PlasmaShot>();
    private List<PlasmaShot> plasma_strong = new List<PlasmaShot>();
    private PlasmaShot leftPlasma;
    private PlasmaShot rightPlasma;
    private bool canShoot = false;
    
    // Use this for initialization
	void Start () 
    {
        timer_1 = deltaTime_1;
        timer_2 = deltaTime_2;
        timer_3 = deltaTime_3;
        enemyPlasmaShots = FindObjectOfType<ListParticle>(); // Вообще-то не то

        for (int i = 0; i < weapon_weak.Count; ++i)
        {
            plasma_weak.Add(new PlasmaShot());
        }
        for (int i = 0; i < weapon_average.Count; ++i)
        {
            plasma_average.Add(new PlasmaShot());
        }
        for (int i = 0; i < plasma_strong.Count; ++i)
        {
            plasma_strong.Add(new PlasmaShot());
        }
    }

    public void Makeshoot(bool val)
    {
        canShoot = val;
        if (!canShoot)
        {
            timer_1 = deltaTime_1;
            timer_2 = deltaTime_2;
            timer_3 = deltaTime_3;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (canShoot)
        {

            if (weapon_weak.Count > 0 & timer_1 >= deltaTime_1)
            {
                timer_1 = 0.0f;

                if (enemyPlasmaShots.listFreeObjects_weak.Count >= weapon_weak.Count)
                {
                    for (int i = 0; i < weapon_weak.Count; ++i)
                    {
                        plasma_weak[i] = enemyPlasmaShots.listFreeObjects_weak[i].GetComponent<PlasmaShot>();
                        plasma_weak[i].gameObject.SetActive(true);
                        plasma_weak[i].SetCoords(weapon_weak[i].transform.position, weapon_weak[i].transform.rotation);
                    }
                }
            }
            if (weapon_average.Count > 0 & timer_2 >= deltaTime_2)
            {
                timer_2 = 0.0f;

                if (enemyPlasmaShots.listFreeObjects_average.Count >= weapon_average.Count)
                {
                    for (int i = 0; i < weapon_weak.Count; ++i)
                    {
                        plasma_average[i] = enemyPlasmaShots.listFreeObjects_average[i].GetComponent<PlasmaShot>();
                        plasma_average[i].gameObject.SetActive(true);
                        plasma_average[i].SetCoords(weapon_average[i].transform.position, weapon_average[i].transform.rotation);
                    }
                }
            }
            if (weapon_strong.Count > 0 & timer_3 >= deltaTime_3)
            {
                timer_3 = 0.0f;

                if (enemyPlasmaShots.listFreeObjects_strong.Count >= weapon_strong.Count)
                {
                    for (int i = 0; i < weapon_weak.Count; ++i)
                    {
                        plasma_strong[i] = enemyPlasmaShots.listFreeObjects_strong[i].GetComponent<PlasmaShot>();
                        plasma_strong[i].gameObject.SetActive(true);
                        plasma_strong[i].SetCoords(weapon_strong[i].transform.position, weapon_strong[i].transform.rotation);
                    }
                }
            }

            /*
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
            */

            timer_1 += Time.deltaTime;
            timer_2 += Time.deltaTime;
            timer_3 += Time.deltaTime;
        }
	}
}