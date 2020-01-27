using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAreaPlasmaShots : MonoBehaviour
{
    public EnemyShipAI_Base upAI;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerPlasma")
        {
            upAI.ShipWasNearlyAttacked();
        }
    }
}
