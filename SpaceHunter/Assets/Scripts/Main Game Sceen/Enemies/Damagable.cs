using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    
    [SerializeField]
    float healthPoints = 10.0f;
    [SerializeField]
    float shieldPoints = 0.0f;

    [SerializeField]
    private float maxHealthPoints;
    [SerializeField]
    private float maxShieldPoints;

    private bool invulnerableAfterDamage = false;

    public GameObject myObject;
    public delegate void DeathDelegate();
    public DeathDelegate deathDel;
    public delegate void PlayerUpdateDelegate(float v1, float v2);
    public PlayerUpdateDelegate playerUpDel;
    public delegate void PlanetChangeHealth(float val);
    public PlanetChangeHealth planetChHlth;
    public delegate void EnemyChangeHealth(float val);
    public EnemyChangeHealth enemyChHlth;
    public delegate void PlasmaAreaDetector();
    public PlasmaAreaDetector plasmaNearFlight;

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

    public float maxHealth
    {
        get { return maxHealthPoints; }
        set { maxHealthPoints = value; }
    }
    public float maxShield
    {
        get { return maxShieldPoints; }
        set { maxShieldPoints = value; }
    }

    void OnEnable()
    {
        invulnerableAfterDamage = false;
    }

    public void SetInvulnerable(bool val)
    {
        invulnerableAfterDamage = val;
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
            else if (minusOfShield > shieldPoints && shieldPoints == 0)
            {
                healthPoints -= minusOfShield * 2.0f;
            }
            else if (minusOfShield > shieldPoints && shieldPoints > 0)
            {
                minusOfShield -= shieldPoints;
                shieldPoints = 0.0f;
                healthPoints -= minusOfShield; // was ... * 2.0f
            }

            //Debug.Log("My Current health points are: " + healthPoints.ToString());
            if (healthPoints <= 0.0f)
            {
                invulnerableAfterDamage = true;
                // Запускаем процедуру гибели объекта
                deathDel();
            }

            if (myObject.tag == "Player")
            {
                playerUpDel(healthPoints, shieldPoints);
                ControlPanelCanvasScript CPCS = FindObjectOfType<ControlPanelCanvasScript>();
                CPCS.UpdateHealth(healthPoints);
                CPCS.UpdateShield(shieldPoints);
            }

            if (myObject.tag == "Planet" || myObject.tag == "Comet")
            {
                planetChHlth(healthPoints);
            }

            if (myObject.tag == "Enemy")
            {
                enemyChHlth(healthPoints/maxHealthPoints);
            }

            if (myObject.tag == "PlasmaArea")
            {
                plasmaNearFlight();
            }
        }
    }

    public void RestoreHealth() // Specially for player
    {
        healthPoints += maxHealthPoints / 4;
        healthPoints = Mathf.Min(healthPoints, maxHealthPoints);
    }

    public void RestoreShield() // Specially for player
    {
        //Debug.Log("Shield Points: " + shieldPoints);
        shieldPoints += maxShieldPoints / 4;
        shieldPoints = Mathf.Min(shieldPoints, maxShieldPoints);
        //Debug.Log("Shield Points: " + shieldPoints);
    }

    public void TakeUltimateDamage()
    {
        healthPoints = 0;
        shieldPoints = 0;

        ControlPanelCanvasScript CPCS = FindObjectOfType<ControlPanelCanvasScript>();
        CPCS.UpdateHealth(healthPoints);
        CPCS.UpdateShield(shieldPoints);

        invulnerableAfterDamage = true;
        deathDel();
    }
}
