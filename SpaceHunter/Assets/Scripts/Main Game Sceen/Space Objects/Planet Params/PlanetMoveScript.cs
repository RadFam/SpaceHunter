using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMoveScript : MonoBehaviour {

    public Vector3 initPosition;
    
    public float polarAxisInclinationX;
    public float polarAxisInclinationZ;
    public float selfRotationSpeed;

    public Vector3 systemCenter;
    public Vector3 equilipticUp;
    public float bigSemiAxis;
    public float smallSemiAxis;
    public float orbitalRevolutionSpeed;
    
    // Use this for initialization
	void Start () 
    {
        transform.position = initPosition;
        transform.eulerAngles = new Vector3(polarAxisInclinationX, 0.0f, polarAxisInclinationZ);

        // By default, Sun is in point (0, 0, 0)
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.RotateAround(transform.position, transform.up, selfRotationSpeed * Time.deltaTime);
        
	}
}
