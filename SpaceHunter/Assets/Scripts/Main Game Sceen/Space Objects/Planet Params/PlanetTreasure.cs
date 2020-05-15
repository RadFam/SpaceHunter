using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTreasure : MonoBehaviour 
{
    public PlanetTypePrize possiblePrizes;
    public PlanetByType mePlanet;
    public ParticleSystem cometExplode;
    public ParticleSystem cometTail;
    public int currSceneLevel;

    private Damagable myDamagable;
    private CommonSceneParams CSP;
    private Animator anim;

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
        anim = GetComponent<Animator>();

	    CSP = FindObjectOfType<CommonSceneParams>();
        //currSceneLevel = CSP.currLevel;
        //Debug.Log("ConstGameCtrl.instance: " + ConstGameCtrl.instance);
        currSceneLevel = ConstGameCtrl.instance.CurrentLevel;

        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlanetTreasure;
        myDamagable.planetChHlth = OnPlasmaWound;

        myDamagable.currentHealth = 5; // mePlanet.planetHealth;
        myDamagable.currentShield = 0;
        myDamagable.myObject = gameObject;

        ObstacleBehaviour OB = gameObject.GetComponent<ObstacleBehaviour>();
        if (OB)
        {
            myRadii = OB.GetMyRadii();
        }
        else
        {
            myRadii = 0;
        }
	}

    public void OnPlasmaWound(float val)
    { 

    }
    
    public void OnPlanetTreasure()
    {   

        if (gameObject.tag == "Planet")
        {
            List<float> probabs = possiblePrizes.GetProbabilitiesByLevel(currSceneLevel);
            //float prob = CSP.GetRandomFloat();
            float prob = 0.75f;
            ConstGameCtrl.PlanetSurprize prize = possiblePrizes.GetNameByProbability(currSceneLevel, prob);

            int prizeNum = (int)prize;
            Debug.Log("Prize num: " + prizeNum);

            if (prizeNum == 0) // Gold
            {
                ResourceControl RC = Instantiate(moneyPrefab);
                RC.SetParams("Money");
                RC.transform.SetParent(gameObject.transform);
                RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                RC.myCollider.enabled = false;
                RC.StartMove(myRadii / 2);
            }

            if (prizeNum == 1) // Health
            {
                ResourceControl RC = Instantiate(healthPrefab);
                RC.SetParams("Health");
                RC.transform.SetParent(gameObject.transform);
                RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                RC.myCollider.enabled = false;
                RC.StartMove(myRadii / 2);
            }

            if (prizeNum == 2) // Shield
            {
                ResourceControl RC = Instantiate(shieldPrefab);
                RC.SetParams("Shield");
                RC.transform.SetParent(gameObject.transform);
                RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                RC.myCollider.enabled = false;
                RC.StartMove(myRadii / 2);
            }

            if (prizeNum == 3) // Fuel
            {
                ResourceControl RC = Instantiate(fuelPrefab);
                RC.SetParams("Fuel");
                RC.transform.SetParent(gameObject.transform);
                RC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                RC.myCollider.enabled = false;
                RC.StartMove(myRadii / 2);
            }

            if (prizeNum > 3) // Minerals
            {
                TresureControl TC = Instantiate(planetTreasurePrefab);
                TC.SetMineralParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeMaterial, ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeMesh, ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeName, System.Enum.GetName(typeof(ConstGameCtrl.PlanetSurprize), prizeNum));
                TC.transform.SetParent(gameObject.transform);
                TC.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                TC.myCollider.enabled = false;
                TC.StartMove(myRadii / 2);
            }
        }

        if (gameObject.tag == "Comet")
        {
            List<float> probabs = possiblePrizes.GetProbabilitiesByLevel(currSceneLevel);
            //float prob = CSP.GetRandomFloat();
            float prob = 0.95f;
            ConstGameCtrl.PlanetSurprize prize = possiblePrizes.GetNameByProbability(currSceneLevel, prob);

            int prizeNum = (int)prize;
            //Debug.Log("Prize num: " + prizeNum);

            ResourceControl RC;
            TresureControl TC;

            if (prizeNum == 0) // Gold
            {
                RC = Instantiate(moneyPrefab);
                RC.SetParams("Money");
                RC.transform.position = gameObject.transform.position;
                RC.myCollider.enabled = true;
            }

            if (prizeNum == 1) // Health
            {
                RC = Instantiate(healthPrefab);
                RC.SetParams("Health");
                RC.transform.position = gameObject.transform.position;
                RC.myCollider.enabled = true;
            }

            if (prizeNum == 2) // Shield
            {
                RC = Instantiate(shieldPrefab);
                RC.SetParams("Shield");
                RC.transform.position = gameObject.transform.position;
                RC.myCollider.enabled = true;
            }

            if (prizeNum == 3) // Fuel
            {
                RC = Instantiate(fuelPrefab);
                RC.SetParams("Fuel");
                RC.transform.position = gameObject.transform.position;
                RC.myCollider.enabled = true;
            }

            if (prizeNum > 3) // Minerals
            {
                TC = Instantiate(planetTreasurePrefab);
                TC.SetMineralParams(ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeMaterial, ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeMesh, ConstGameCtrl.instance.GetPrizeParams(prizeNum-4).prizeName, System.Enum.GetName(typeof(ConstGameCtrl.PlanetSurprize), prizeNum));
                TC.transform.position = gameObject.transform.position;
                TC.myCollider.enabled = true;
            }

            Collider myColl = GetComponent<Collider>();
            myColl.enabled = false;

            Invoke("OnRealDeath", 3f);
            cometTail.Stop();
            anim.SetTrigger("OnDeath");
            cometExplode.Play();
        }
    }

    public void OnRealDeath()
    {
        Destroy(gameObject);
    }
}
