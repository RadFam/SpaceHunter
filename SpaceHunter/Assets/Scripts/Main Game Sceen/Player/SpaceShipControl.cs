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

    private Vector3 cameraGamePosition = new Vector3(0.0f, 12.0f, -11.0f);
    private Vector3 cameraGameAngles = new Vector3(30.0f, 0.0f, 0.0f);
    private Vector3 cameraVortexPosition = new Vector3(0.0f, 2.5f, -7.5f);
    private Vector3 cameraVortexAngles = new Vector3(15.0f, 0.0f, 0.0f);

    private float subtimer;
    private float vortexDeltaTime;

    Damagable myDamagable;

	// Use this for initialization
	void Start () 
    {
        myDamagable = gameObject.GetComponent<Damagable>();
        myDamagable.deathDel = OnPlayerDeath;
        myDamagable.playerUpDel = OnPlasmaWound;

        CPCS = FindObjectOfType<ControlPanelCanvasScript>();
        CSP = FindObjectOfType<CommonSceneParams>();

        //myDamagable.currentHealth = CSP.pIH;
        //myDamagable.currentShield = CSP.pIS;
        myDamagable.myObject = gameObject;

        playerCamera.transform.localPosition = cameraGamePosition;
        cameraShipDist = Vector3.Distance(playerCamera.transform.localPosition, shipObject.transform.localPosition);
        cameraMoveAlong = new Vector3(0.0f, 12.0f, -36.0f);
        cameraMoveAlong.Normalize();

        subtimer = 0.0f;
        vortexDeltaTime = 0.01f;
        //playerCamera.GetComponent<Animator>().enabled = false;
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

    public void FreezeAll()
    {
        //playerCamera.GetComponent<Animator>().enabled = false;
        SpaceShipMove ssm = gameObject.GetComponent<SpaceShipMove>();
        ssm.DisableControl();
        myDamagable.SetInvulnerable(true);


        playerCamera.transform.localPosition = cameraVortexPosition;
        playerCamera.transform.localEulerAngles = cameraVortexAngles;
        
    }

    public void UnfreezeAll()
    {
        //playerCamera.GetComponent<Animator>().enabled = true;
        //SpaceShipMove ssm = gameObject.GetComponent<SpaceShipMove>();  
        //Animator anim = playerCamera.GetComponent<Animator>();
        //anim.Play("CameraVortexOut");
        StartCoroutine(UnfreezeCoroutine());
        //ssm.EnableControl();
    }

    IEnumerator UnfreezeCoroutine()
    {
        
        //playerCamera.GetComponent<Animator>().enabled = false;
        while (Vector3.Distance(playerCamera.transform.localPosition, cameraGamePosition) >= 0.1f && Vector3.Distance(playerCamera.transform.localEulerAngles, cameraGameAngles) >= 0.1f)
        {
            if (Vector3.Distance(playerCamera.transform.localPosition, cameraGamePosition) >= 0.1f)
            {
                playerCamera.transform.localPosition = Vector3.Lerp(cameraVortexPosition, cameraGamePosition, subtimer / 0.5f);
            }
            else
            {
                playerCamera.transform.localPosition = cameraGamePosition;
            }
            if (Vector3.Distance(playerCamera.transform.localEulerAngles, cameraGameAngles) >= 0.1f)
            {
                playerCamera.transform.localEulerAngles = Vector3.Lerp(cameraVortexAngles, cameraGameAngles, subtimer / 0.5f);
            }
            else
            {
                playerCamera.transform.localEulerAngles = cameraGameAngles;
            }

            subtimer += vortexDeltaTime;

            yield return new WaitForSeconds(vortexDeltaTime);
        }

        subtimer = 0.0f;

        //playerCamera.transform.localPosition = cameraGamePosition;
        //playerCamera.transform.localEulerAngles = cameraGameAngles;

        SpaceShipMove ssm = gameObject.GetComponent<SpaceShipMove>();
        myDamagable.SetInvulnerable(false);
        ssm.EnableControl();
    }

}
