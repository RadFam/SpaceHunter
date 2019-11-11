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
        Debug.Log("Start opening my planet treasure");
        
        List<float> probabs = possiblePrizes.GetProbabilitiesByLevel(currSceneLevel);
        //float prob = CSP.GetRandomFloat();
        float prob = 0.7f;
        ConstGameCtrl.PlanetSurprize prize = possiblePrizes.GetNameByProbability(currSceneLevel, prob);

        int prizeNum = (int)prize;

        if (prizeNum > 3)
        {
            TresureControl TC = Instantiate(planetTreasurePrefab);
            TC.SetMineralParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMaterial, ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMesh);
            TC.transform.SetParent(gameObject.transform);
            TC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            TC.transform.localScale = new Vector3(1.0f, 1.0f, 3.0f);
            TC.myCollider.enabled = false;
            TC.StartMove(myRadii);
            //TC.innerMat = ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMaterial;
            //TC.innerMesh = ConstGameCtrl.instance.GetPrizeParams(prizeNum).prizeMesh;
        }

    }
}
