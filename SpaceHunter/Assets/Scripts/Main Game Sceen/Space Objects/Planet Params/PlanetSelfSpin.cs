using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelfSpin : MonoBehaviour {

    private PlanetMoveScript PMS;

    private float pAIX;
    private float pAIZ;
    private float sRS;
    private Vector3 initPos;

	// Use this for initialization
	void Start () 
    {
	    PMS = GetComponentInParent<PlanetMoveScript>();

        pAIX = PMS.polarAxisInclinationX;
        pAIZ = PMS.polarAxisInclinationZ;
        sRS = PMS.selfRotationSpeed;

        initPos = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localPosition = initPos;
        transform.localEulerAngles = new Vector3(-90+pAIX, 0.0f, pAIZ);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        transform.RotateAround(gameObject.transform.localPosition, gameObject.transform.up, sRS * Time.deltaTime);
	}
}
