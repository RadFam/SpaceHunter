using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    private int healthPoints = 10;
    private bool invulnerableAfterDamage = false;

    public delegate void DeathDelegate();
    public DeathDelegate deathDel;

    public int currentHealth
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }

    void OnEnable()
    {
        invulnerableAfterDamage = false;
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerableAfterDamage)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
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
