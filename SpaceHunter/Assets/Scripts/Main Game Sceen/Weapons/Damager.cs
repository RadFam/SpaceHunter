using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public int damage = 1;
    public LayerMask layerMask;

    public void MakeDamage(Damagable objectToDamage)
    {
        objectToDamage.TakeDamage(damage);
    }
}
