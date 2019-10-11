using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public float damage = 1.0f;
    public LayerMask layerMask;

    public void MakeDamage(Damagable objectToDamage)
    {
        objectToDamage.TakeDamage(damage);
    }
}
