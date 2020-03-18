using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTargetFrame : MonoBehaviour
{
    public Canvas myCanvas;
    public Transform cameraPlaneOrient;

    private GameObject player;
    private float maxDist;
    private float currDist;
    private Vector3 pinAxis;
    private float coeff = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        maxDist = player.GetComponent<SpaceShipWatchEnemies2>().SightDist;
        SetFrameParams();
    }

    // Update is called once per frame
    void Update()
    {
        SetFrameParams();
    }

    private void SetFrameParams()
    {
        pinAxis = player.transform.position - gameObject.transform.position;
        currDist = pinAxis.magnitude;

        coeff = 2.0f;
        if (currDist / maxDist <= 0.75)
        {
            coeff = 4.0f;
        }
        if (currDist / maxDist <= 0.5)
        {
            coeff = 6.0f;
        }
        if (currDist / maxDist <= 0.25)
        {
            coeff = 8.0f;
        }

        myCanvas.transform.LookAt(myCanvas.transform.position + cameraPlaneOrient.rotation*Vector3.back, cameraPlaneOrient.rotation*Vector3.up);
        myCanvas.transform.localScale = Vector3.one * coeff * currDist / maxDist;
        

        if (currDist <= maxDist)
        {
            if (!myCanvas.gameObject.activeSelf)
            {
                myCanvas.gameObject.SetActive(true);
            }
        }
        else
        {
            if (myCanvas.gameObject.activeSelf)
            {
                myCanvas.gameObject.SetActive(false);
            }
        }
    }
}
