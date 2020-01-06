using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipControl : MonoBehaviour {

    public GameEvent deathEvent;

    public GameObject shipObject;
    public ParticleSystem deathExplode;
    public GameObject playerCamera;

    private ControlPanelCanvasScript CPCS;
    private CommonSceneParams CSP;

    private double cameraShipDist;
    private double cameraShipDistBound = 60.0f;
    private Vector3 cameraMoveAlong;
    
    Damagable myDamagable;

	// Use this for initialization
	void Start () 
    {
        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlayerDeath;
        myDamagable.playerUpDel = OnPlasmaWound;

        CPCS = FindObjectOfType<ControlPanelCanvasScript>();
        CSP = FindObjectOfType<CommonSceneParams>();

        myDamagable.currentHealth = CSP.pIH;
        myDamagable.currentShield = CSP.pIS;
        myDamagable.myObject = gameObject;

        playerCamera.transform.localPosition = new Vector3(0.0f, 12.0f, -11.0f);
        cameraShipDist = Vector3.Distance(playerCamera.transform.localPosition, shipObject.transform.localPosition);
        cameraMoveAlong = new Vector3(0.0f, 12.0f, -36.0f);
        cameraMoveAlong.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerDeath()
    {
        // Send signal to all enemies to resume patrolling state
        deathEvent.Raise();

        // Play explode animation
        shipObject.SetActive(false);
        gameObject.GetComponent<SpaceShipMove>().isDead = true;
        gameObject.GetComponent<SpaceShipShoot>().isDead = true;
        StartCoroutine(CameraRebound());
        deathExplode.Play();

        // Start final titers, go to menu scene
    }

    public void OnPlasmaWound(float v1, float v2)
    {
        CPCS.UpdateHealth(v1);
        CPCS.UpdateShield(v2);
    }

    IEnumerator CameraRebound()
    {
        while (cameraShipDist <= cameraShipDistBound)
        {
            playerCamera.transform.Translate(cameraMoveAlong * 3f, Space.Self);

            cameraShipDist = Vector3.Distance(playerCamera.transform.localPosition, shipObject.transform.localPosition);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
