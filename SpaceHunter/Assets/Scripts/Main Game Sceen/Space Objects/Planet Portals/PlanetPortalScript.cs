using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPortalScript : MonoBehaviour
{
    public PlanetCountPortals myPlanetCtrl;
    public Collider myCheckCollider;
    public Texture changeTexture;
    protected Renderer myRenderer;
    protected int myNum;
    protected bool isPassed;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void SetParams(int num, PlanetCountPortals pcp, Vector3 position, Vector3 localAngles)
    {
        myNum = num;
        myPlanetCtrl = pcp;
        isPassed = false;
        gameObject.transform.localPosition = position;
        gameObject.transform.localEulerAngles = localAngles;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPassed)
        {
            isPassed = true;
            myPlanetCtrl.SendSignal(myNum);
            myCheckCollider.enabled = false;

            // Change texture
            myRenderer.material.SetTexture("_MainTex", changeTexture);
        }
    }


}
