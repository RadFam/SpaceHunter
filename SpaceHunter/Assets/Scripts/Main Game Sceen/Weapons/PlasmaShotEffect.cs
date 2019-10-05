using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShotEffect : MonoBehaviour {

    public LayerMask layerMask;
    
    private bool canFly;
    private Vector3 oldPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float shotSpeed = 200.0f;
    private RaycastHit hit;
    private PlasmaShot PS;
    private Damager myDamager;

    void Awake()
    {
        PS = gameObject.GetComponent<PlasmaShot>();
        myDamager = gameObject.GetComponent<Damager>();
    }

    public void SetPosition(Vector3 setPos, Quaternion setRot)
    {
        gameObject.transform.position = setPos;
        gameObject.transform.rotation = setRot;
        
        oldPosition = gameObject.transform.position;
        canFly = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (canFly)
        {
            gameObject.transform.Translate(gameObject.transform.forward * shotSpeed * Time.deltaTime, Space.World);

            if (Physics.Linecast(oldPosition, gameObject.transform.position, out hit))
            {
                //Debug.Log("Plasma shot hit collider: " + hit.collider.gameObject.layer);
                if (hit.collider.gameObject.layer == layerMask.value)
                {
                    //Debug.Log("Plasma shot hit: " + layerMask);
                    myDamager.MakeDamage(hit.collider.gameObject.GetComponent<Damagable>());
                    EndScript();
                }  
            }

            oldPosition = gameObject.transform.position;
        }
	}

    public void EndScript()
    {
        canFly = false;
        PS.timer = 3.1f;
    }
}
