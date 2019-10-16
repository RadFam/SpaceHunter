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

    public List<Vector3> posToWalk;
    private float cParam;
    private int pointCount;
    private Vector3 pointCoord;

    void Awake()
    {
        cParam = Mathf.Sqrt(bigSemiAxis * bigSemiAxis - smallSemiAxis * smallSemiAxis);
        //Vector3 ellipseCenterVect = Vector3.Normalize(systemCenter - transform.position)*cParam;
        initPosition = transform.position;
        Vector3 ellipseCenter = Vector3.Normalize(systemCenter - transform.position) * bigSemiAxis;
        posToWalk = new List<Vector3>();
        for (int i = 0; i < 100; ++i)
        {
            Vector3 tmp = new Vector3(ellipseCenter.x + smallSemiAxis * Mathf.Sin(i * 2 * 3.14f / 100), ellipseCenter.y, ellipseCenter.z + bigSemiAxis * Mathf.Cos(i * 2 * 3.14f / 100));
            posToWalk.Add(tmp);
        }

        pointCount = 1;
        pointCoord = posToWalk[pointCount];
    }
    
    // Use this for initialization
	void Start () 
    {
        //transform.position = initPosition;
        //transform.eulerAngles = new Vector3(polarAxisInclinationX, 0.0f, polarAxisInclinationZ);

        // By default, Sun is in point (0, 0, 0)
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.RotateAround(transform.position, transform.up, selfRotationSpeed * Time.deltaTime);

        // !!!!!!!!!!!!!!!!!!
        // Commented, while treasure part is making....
        
        /*
        float step = orbitalRevolutionSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, pointCoord, step);

        if (Vector3.Distance(transform.position, pointCoord) < 1.0f)
        {
            pointCount++;
            if (pointCount >= 100)
            {
                pointCount = 0;
            }
            pointCoord = posToWalk[pointCount];
        }
        */
	}
}
