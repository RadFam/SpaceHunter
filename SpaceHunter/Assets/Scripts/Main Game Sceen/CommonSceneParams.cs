using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSceneParams : MonoBehaviour {

    //public GameEventListener gameEL;

    private float playerInitHealth;
    private float playerInitShield;
    private float playerFuelVolume;

    private float lastFuelVolume;

    private List<EnemyShipAI_Base> enemies;
    private ControlPanelCanvasScript CPCS;
    private bool canFly;

    private string endMessage_01 = "Топливо закончилось\nПора домой";
    private string endMessage_02 = "Миссия провалилась\nПора возвращаться";

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
        //playerInitHealth = 30;
        //playerInitShield = 30;
        //playerFuelVolume = 360;

        //lastFuelVolume = playerFuelVolume;
    }

	void Start () 
    {
        playerInitHealth = ConstGameCtrl.instance.GetMaxPlayerParam(ConstGameCtrl.PlayerShipUpgrades.health);
        playerInitShield = ConstGameCtrl.instance.GetMaxPlayerParam(ConstGameCtrl.PlayerShipUpgrades.shield);
        playerFuelVolume = ConstGameCtrl.instance.GetMaxPlayerParam(ConstGameCtrl.PlayerShipUpgrades.fuel);

        lastFuelVolume = playerFuelVolume;

        Random.InitState(42);
		CPCS = FindObjectOfType<ControlPanelCanvasScript>();

        CPCS.UpdateHealth(playerInitHealth);
        CPCS.UpdateShield(0);

        canFly = true;
	}

    public float GetRandomFloat()
    {
        return Random.Range(0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (canFly)
        {
            CPCS.UpdateFuel(lastFuelVolume);

            lastFuelVolume -= Time.deltaTime;

            if (lastFuelVolume <= 0.1f)
            {
                // Отключить игрока
                canFly = false;
                SpaceShipControl ssc = FindObjectOfType<SpaceShipControl>();
                ssc.FreezeAll();
                EndGame(endMessage_01);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Return))
            {
                ControlPanelCanvasScript cpcs = FindObjectOfType<ControlPanelCanvasScript>();
                cpcs.CloseEndMessage();
            }
        }
	}

    public void AddFuel(int val)
    {
        lastFuelVolume = Mathf.Min(lastFuelVolume+val, playerFuelVolume);
    }

    public void OnPlayerDeath()
    {
        canFly = false;
        //SpaceShipControl ssc = FindObjectOfType<SpaceShipControl>();
        //ssc.FreezeAll();
        EndGame(endMessage_02);
    }

    public void EndGame(string msg)
    {
        ControlPanelCanvasScript cpcs = FindObjectOfType<ControlPanelCanvasScript>();
        cpcs.ShowEndMessage(msg);
    }
}
