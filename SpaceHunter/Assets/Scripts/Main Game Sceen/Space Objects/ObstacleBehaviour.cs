using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    private float basicSize;
    private List<Transform> pointsOfLeaving = new List<Transform>();
    private Vector3 velocityDir;
    
    // Use this for initialization
	void Start () 
    {
        int childs = transform.childCount;
        for (int i = 1; i < childs; ++i)
        {
            pointsOfLeaving.Add(transform.GetChild(i));
        }

        velocityDir = new Vector3(0.0f, 0.0f, 0.0f);

        Vector3 baseSizes = gameObject.GetComponent<Collider>().bounds.size;
        basicSize = baseSizes.x;
	}

    public float GetMyRadii()
    {
        return gameObject.GetComponent<Collider>().bounds.size.x;
    }

    public Vector3 GetLeavePoint(Vector3 wayPoint)
    {
        Vector3 ans = pointsOfLeaving[0].position;
        Vector3 tmp_1 = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 tmp_2 = new Vector3(0.0f, 0.0f, 0.0f);

        float tmpMax = basicSize;
        float distance;
        int ind = 0;
        for (int i = 0; i < pointsOfLeaving.Count; ++i )
        {
            tmp_1 = pointsOfLeaving[i].position - wayPoint;
            tmp_2 = pointsOfLeaving[i].position - transform.position;

            if (tmp_1.magnitude != 0.0f)
            {
                distance = (Vector3.Cross(tmp_1, tmp_2).magnitude) / (tmp_1.magnitude);
                if ((distance > basicSize) && (Vector3.Dot(tmp_2, velocityDir) <= 0))
                {
                    if (distance >= tmpMax)
                    {
                        ind = i;
                    }
                }
            }
        }

        ans = pointsOfLeaving[ind].position;

        return ans;
    }
}
