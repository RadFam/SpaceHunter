using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodMoveScript : MonoBehaviour
{
    public float exc;
    public Vector3 spineAngles;
    public float speedSpine;
    public float speedOrbital;

    private List<Vector3> pointsToWalk;
    private Vector3 sunPlace;
    private int currPoint;
    private int allPointNum = 64;
    private Vector3 nextPlace;
    private float distEx = 5f;

    private float step;

    // Use this for initialization
    void Start()
    {
        pointsToWalk = new List<Vector3>();

        // Наклоним ось вращения
        transform.localEulerAngles = spineAngles;

        // Рассчитаем большую и малую полуось
        sunPlace = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 a = gameObject.transform.position - sunPlace;
        float aL = a.magnitude;
        float bL = aL * exc;
        a = a.normalized;
        Vector3 b = new Vector3(-a.y, a.x, 0.0f);
        b = b.normalized;
        Vector3 c = Vector3.Cross(a, b);
        c = c.normalized;

        Vector3 x_tr = new Vector3(a.x, b.x, c.x);
        Vector3 y_tr = new Vector3(a.y, b.y, c.y);
        Vector3 z_tr = new Vector3(a.z, b.z, c.z);

        for (int i = 0; i < allPointNum; ++i)
        {
            Vector3 tmp = new Vector3(aL * Mathf.Cos(i * (2 * 3.14f) / 64), bL * Mathf.Sin(i * (2 * 3.14f) / 64), 0.0f);
            Vector3 pnt = new Vector3(Vector3.Dot(x_tr, tmp), Vector3.Dot(y_tr, tmp), Vector3.Dot(z_tr, tmp));
            pointsToWalk.Add(pnt);
        }

        currPoint = 1;
        nextPlace = pointsToWalk[currPoint];
    }

    // Update is called once per frame
    void Update()
    {
        // Самовращение вокруг оси
        transform.RotateAround(transform.position, transform.forward, speedSpine * Time.deltaTime);

        // Движение по орбите
        step = speedOrbital * Time.deltaTime; // calculate distance to move
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPlace, step);
        if (Vector3.Distance(gameObject.transform.position, nextPlace) <= distEx)
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
