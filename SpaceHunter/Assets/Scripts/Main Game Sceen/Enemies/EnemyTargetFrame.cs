using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTargetFrame : MonoBehaviour
{
    public Canvas myCanvas;

    private GameObject player;
    private float maxDist;
    private float currDist;
    private Vector3 pinAxis;
    
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

        Quaternion rotation = Quaternion.LookRotation(pinAxis, player.GetComponent<SpaceShipWatchEnemies2>().UpVector);
        myCanvas.transform.rotation = rotation;

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
