using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAreaPlasmaShots : MonoBehaviour
{
    public EnemyShipAI_Base upAI;

    protected Damagable myDamage;
    private void Start()
    {
        myDamage = GetComponent<Damagable>();
        myDamage.plasmaNearFlight = NearPlasmaDetect;
    }

    public void NearPlasmaDetect()
    {
        upAI.ShipWasNearlyAttacked();
    }
}
