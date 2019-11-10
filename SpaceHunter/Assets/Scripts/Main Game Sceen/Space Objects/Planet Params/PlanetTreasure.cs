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
    // public ResourcePrizeControl planetResourcePrefab;

	// Use this for initialization
	void Start () 
    {
	    CSP = FindObjectOfType<CommonSceneParams>();
        currSceneLevel = CSP.currLevel;

        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlanetTreasure;
        myDamagable.planetChHlth = OnPlasmaWound;

        myDamagable.currentHealth = mePlanet.planetHealth;
        myDamagable.currentShield = 0;
        myDamagable.myObject = gameObject;
	}

    public void OnPlasmaWound(float val)
    { 

    }
    
    public void OnPlanetTreasure()
    {
        List<float> probabs = possiblePrizes.GetProbabilitiesByLevel(currSceneLevel);
        float prob = CSP.GetRandomFloat();
        ConstGameCtrl.PlanetSurprize prize = possiblePrizes.GetNameByProbability(currSceneLevel, prob);

        int prizeNum = (int)prize;

        if (prizeNum > 3)
        {
            TresureControl TC = Instantiate(planetTreasurePrefab);
            TC.SetMineralParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMaterial, ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMesh);
            //TC.innerMat = ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMaterial;
            //TC.innerMesh = ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMesh;
        }

    }
}
