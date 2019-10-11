using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSceneParams : MonoBehaviour {

    private float playerInitHealth;
    private float playerInitShield;
    private float playerFuelVolume;

    private float lastFuelVolume;

    private List<EnemyShipAI> enemies;
    private ControlPanelCanvasScript CPCS;

    public float pIH
    {
        get { return playerInitHealth; }
    }
    public float pIS
    {
        get { return playerInitShield; }
    }
    public float pFV
    {
        get { return playerFuelVolume; }
    }
    
    // Use this for initialization
    void Awake()
    {
        playerInitHealth = 30;
        playerInitShield = 30;
        playerFuelVolume = 360;

        lastFuelVolume = playerFuelVolume;
    }

	void Start () 
    {
		CPCS = FindObjectOfType<ControlPanelCanvasScript>();

        CPCS.UpdateHealth(playerInitHealth);
        //CPCS.UpdateShield(playerInitShield);
	}
	
	// Update is called once per frame
	void Update () 
    {
        CPCS.UpdateFuel(lastFuelVolume);

        lastFuelVolume -= Time.deltaTime;

        if (lastFuelVolume <= 0.1f)
        {
            // End Game
        }
	}
}
