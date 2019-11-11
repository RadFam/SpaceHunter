using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    private float healthPoints = 10.0f;
    private float shieldPoints = 0.0f;
    private bool invulnerableAfterDamage = false;

    public GameObject myObject;
    public delegate void DeathDelegate();
    public DeathDelegate deathDel;
    public delegate void PlayerUpdateDelegate(float v1, float v2);
    public PlayerUpdateDelegate playerUpDel;
    public delegate void PlanetChangeHealth(float val);
    public PlanetChangeHealth planetChHlth;

    public float currentHealth
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }
    public float currentShield
    {
        get { return shieldPoints; }
        set { shieldPoints = value; }
    }

    void OnEnable()
    {
        invulnerableAfterDamage = false;
    }

    public void TakeDamage(float damage)
    {
        if (!invulnerableAfterDamage)
        {
            float halfDamage_1 = damage / 2.0f;
            float halfDamage_2 = damage / 2.0f;
            healthPoints -= halfDamage_1;

            float minusOfShield = halfDamage_2 / 2.0f;
            if (minusOfShield < shieldPoints)
            {
                shieldPoints -= minusOfShield;
            }
            else if (minusOfShield == shieldPoints)
            {
                shieldPoints = 0.0f;
            }
            else if (minusOfShield > shieldPoints)
            {
                minusOfShield -= shieldPoints;
                shieldPoints = 0.0f;
                healthPoints -= minusOfShield * 2.0f;
            }

            Debug.Log("My Current health points are: " + healthPoints.ToString());

            if (myObject.tag == "Player")
            {
                playerUpDel(healthPoints, shieldPoints);
            }

            if (myObject.tag == "Planet")
            {
                planetChHlth(healthPoints);
            }

            if (healthPoints <= 0.0f)
            {
                invulnerableAfterDamage = true;
                // Запускаем процедуру гибели объекта
                deathDel();
            }
        }
    }

    public void TakeUltimateDamage()
    {
        healthPoints = 0;
        invulnerableAfterDamage = true;
        deathDel();
    }
}
