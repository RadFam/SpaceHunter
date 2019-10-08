using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCollision : MonoBehaviour {

    public Damagable myDamagable;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planet" || other.gameObject.tag == "Sun" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Comet")
        {
            myDamagable.TakeUltimateDamage();
        }
    }
}
