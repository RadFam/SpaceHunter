using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometInnerRotation : MonoBehaviour
{
    public GameObject cometCore;
    public GameObject cometSpl_01;
    public GameObject cometSpl_02;
    public GameObject cometSpl_03;
    public GameObject cometSpl_04;
    public GameObject cometSpl_05;

    private List<float> spinSpeed = new List<float> { 15.0f, 13.0f, 10.0f, 13.5f, 15.0f, 16.0f};
    private Vector3 spinCore = new Vector3(1.0f, 3.0f, 4.0f);
    private Vector3 spinSpl_01 = new Vector3(3.0f, 2.0f, 4.0f);
    private Vector3 spinSpl_02 = new Vector3(1.0f, -1.0f, 4.0f);
    private Vector3 spinSpl_03 = new Vector3(1.0f, 2.0f, 1.0f);
    private Vector3 spinSpl_04 = new Vector3(0.0f, 1.0f, 1.0f);
    private Vector3 spinSpl_05 = new Vector3(-1.0f, 1.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        cometCore.transform.RotateAround(cometCore.transform.position, spinCore, spinSpeed[0] * Time.deltaTime);
        cometSpl_01.transform.RotateAround(cometSpl_01.transform.position, spinSpl_01, spinSpeed[1] * Time.deltaTime);
        cometSpl_02.transform.RotateAround(cometSpl_02.transform.position, spinSpl_02, spinSpeed[2] * Time.deltaTime);
        cometSpl_03.transform.RotateAround(cometSpl_03.transform.position, spinSpl_03, spinSpeed[3] * Time.deltaTime);
        cometSpl_04.transform.RotateAround(cometSpl_04.transform.position, spinSpl_04, spinSpeed[4] * Time.deltaTime);
        cometSpl_05.transform.RotateAround(cometSpl_05.transform.position, spinSpl_05, spinSpeed[5] * Time.deltaTime);
    }
}
