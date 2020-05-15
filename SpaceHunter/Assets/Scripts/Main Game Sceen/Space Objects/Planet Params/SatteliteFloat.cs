using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatteliteFloat : MonoBehaviour
{
    [SerializeField]
    Transform myParent;
    [SerializeField]
    float MyRadii;
    [SerializeField]
    float speedOrbital;
    [SerializeField]
    bool planetOrient;

    private List<Vector3> pointsToWalk;
    private int currPoint;
    private int allPointNum = 64;
    private Vector3 nextPlace;
    private float distEx = 0.1f;

    private float step;
    private float subangle;

    // Start is called before the first frame update
    void Start()
    {
        pointsToWalk = new List<Vector3>();
        for (int i = 0; i < allPointNum; ++i)
        {
            Vector3 tmp = new Vector3(MyRadii * Mathf.Cos(i * (2 * 3.14f) / 64), 0.0f, MyRadii * Mathf.Sin(i * (2 * 3.14f) / 64)); 
            pointsToWalk.Add(tmp);
        }

        transform.localPosition = pointsToWalk[0];
        transform.LookAt(myParent, transform.up);
        transform.RotateAround(transform.position, transform.up, 180.0f);
        if (planetOrient)
        {
            float angle_1 = Vector3.Angle(myParent.position, myParent.up) - 90.0f;
            Quaternion q = Quaternion.AngleAxis(angle_1, myParent.up);
            Vector3 orbitalUp = q * transform.up;
            transform.up = orbitalUp;
        }

        currPoint = 1;
        nextPlace = pointsToWalk[currPoint];
    }

    // Update is called once per frame
    void Update()
    {
        step = speedOrbital * Time.deltaTime; // calculate distance to move
        subangle = (step * step) / (2 * MyRadii * MyRadii);
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, nextPlace, step);
        //gameObject.transform.RotateAround(transform.position, transform.up, subangle);
        transform.LookAt(myParent, transform.up);
        transform.RotateAround(transform.position, transform.up, 180.0f);
        if (Vector3.Distance(gameObject.transform.localPosition, nextPlace) <= distEx)
        {
            currPoint++;
            if (currPoint >= allPointNum)
            {
                currPoint = 0;
            }
            nextPlace = pointsToWalk[currPoint];
        }
    }
}
