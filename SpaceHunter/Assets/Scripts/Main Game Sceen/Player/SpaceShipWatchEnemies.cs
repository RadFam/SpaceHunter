using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipWatchEnemies : MonoBehaviour
{
    [SerializeField]
    private float myDistanceView = 250.0f;
    [SerializeField]
    private float widthAngle = 90.0f;
    [SerializeField]
    private float heightAngle = 90.0f;
    [SerializeField]
    private float timeOfEnemiesRewatch = 1.0f;

    private float timer = 0.0f;
    private List<Transform> myEnemies = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        FindActualEnemies();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Draw all actual enemies

        timer += Time.deltaTime;
        if (timer >= timeOfEnemiesRewatch)
        {
            timer = 0;
            FindActualEnemies();
        }
    }

    void FindActualEnemies()
    {
        Vector3 tmpVect;
        float tmpWideAng;
        float tmpHghtAng;
        RaycastHit hit;

        myEnemies.Clear();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < allEnemies.Length; ++i)
        {
            tmpVect = allEnemies[i].transform.position - gameObject.transform.position;
            tmpWideAng = Mathf.Abs(Vector3.Angle(gameObject.transform.forward,tmpVect));
            tmpHghtAng = Mathf.Abs(Vector3.Angle(gameObject.transform.up, tmpVect));
            if (tmpVect.magnitude <= myDistanceView && tmpWideAng < widthAngle && tmpHghtAng < heightAngle)
            {
                if(Physics.Linecast(gameObject.transform.position, allEnemies[i].transform.position, out hit, 32))
                {
                    myEnemies.Add(allEnemies[i].transform);
                }
            }
        }
    }

    private void DrawActualEnemies()
    {

    }
}
