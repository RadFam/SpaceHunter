using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTreasure : MonoBehaviour 
{
    public PlanetTypePrize possiblePrizes;
    public PlanetByType mePlanet;
    public int currSceneLevel;

    private Damagable myDamagable;
    private CommonSceneParams CSP;

    public TresureControl planetTreasurePrefab;
    public ResourceControl healthPrefab;
    public ResourceControl shieldPrefab;
    public ResourceControl fuelPrefab;
    public ResourceControl moneyPrefab;

    public float myRadii;
    // public ResourcePrizeControl planetResourcePrefab;

	// Use this for initialization
	void Start () 
    {
	    CSP = FindObjectOfType<CommonSceneParams>();
        currSceneLevel = CSP.currLevel;

        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlanetTreasure;
        myDamagable.planetChHlth = OnPlasmaWound;

        myDamagable.currentHealth = 5; // mePlanet.planetHealth;
        myDamagable.currentShield = 0;
        myDamagable.myObject = gameObject;

        ObstacleBehaviour OB = gameObject.GetComponent<ObstacleBehaviour>();
        myRadii = OB.GetMyRadii();
	}

    public void OnPlasmaWound(float val)
    { 

    }
    
    public void OnPlanetTreasure()
    {   
        List<float> probabs = possiblePrizes.GetProbabilitiesByLevel(currSceneLevel);
        //float prob = CSP.GetRandomFloat();
        float prob = 0.7f;
        ConstGameCtrl.PlanetSurprize prize = possiblePrizes.GetNameByProbability(currSceneLevel, prob);

        int prizeNum = (int)prize;

        if (prizeNum == 0) // Gold
        {
            ResourceControl RC = Instantiate(moneyPrefab);
            RC.SetParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeName);
            RC.transform.SetParent(gameObject.transform);
            RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            RC.myCollider.enabled = false;
            RC.StartMove(myRadii / 2);
        }

        if (prizeNum == 1) // Health
        {
            ResourceControl RC = Instantiate(healthPrefab);
            RC.SetParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeName);
            RC.transform.SetParent(gameObject.transform);
            RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            RC.myCollider.enabled = false;
            RC.StartMove(myRadii / 2);
        }

        if (prizeNum == 2) // Shield
        {
            ResourceControl RC = Instantiate(shieldPrefab);
            RC.SetParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeName);
            RC.transform.SetParent(gameObject.transform);
            RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            RC.myCollider.enabled = false;
            RC.StartMove(myRadii / 2);
        }

        if (prizeNum == 3) // Fuel
        {
            ResourceControl RC = Instantiate(fuelPrefab);
            RC.SetParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeName);
            RC.transform.SetParent(gameObject.transform);
            RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            RC.myCollider.enabled = false;
            RC.StartMove(myRadii / 2);
        }

        if (prizeNum > 3) // Minerals
        {
            TresureControl TC = Instantiate(planetTreasurePrefab);
            TC.SetMineralParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMaterial, ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMesh, ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeName);
            TC.transform.SetParent(gameObject.transform);
            TC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            TC.myCollider.enabled = false;
            TC.StartMove(myRadii/2);
        }

    }
}
