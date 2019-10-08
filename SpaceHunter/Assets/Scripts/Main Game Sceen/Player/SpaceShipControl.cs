using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipControl : MonoBehaviour {

    public GameEvent deathEvent;
    
    Damagable myDamagable;

	// Use this for initialization
	void Start () 
    {
        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.currentHealth = 20;
        myDamagable.deathDel = OnPlayerDeath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerDeath()
    {
        // Send signal to all enemies to resume patrolling state
        deathEvent.Raise();

        // Play explode animation

        // Start final titers, go to menu scene
    }
}
