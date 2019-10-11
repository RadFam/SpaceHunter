using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipControl : MonoBehaviour {

    public GameEvent deathEvent;

    private ControlPanelCanvasScript CPCS;
    
    Damagable myDamagable;

	// Use this for initialization
	void Start () 
    {
        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlayerDeath;
        myDamagable.playerUpDel = OnPlasmaWound;

        CPCS = FindObjectOfType<ControlPanelCanvasScript>();
        CommonSceneParams CSP = FindObjectOfType<CommonSceneParams>();

        myDamagable.currentHealth = CSP.pIH;
        myDamagable.currentShield = CSP.pIS;
        myDamagable.myObject = gameObject;
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

    public void OnPlasmaWound(float v1, float v2)
    {
        CPCS.UpdateHealth(v1);
        CPCS.UpdateShield(v2);
    }
}
