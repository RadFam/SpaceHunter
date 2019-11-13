using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronFlare : MonoBehaviour {

    public GameObject myParent;
    
    public float radii;
    public float speed;

    private Vector3 rotPos;
    private Vector3 rotVct;
    private Vector3 initPos;

    public int elecType;
    
    // Use this for initialization
	void Start () 
    {
        rotPos = myParent.transform.position;

        if (elecType == 0)
        {
            rotVct = Vector3.Normalize(myParent.transform.forward);
            initPos = rotPos+Vector3.Normalize(myParent.transform.up) * radii;
        }
        if (elecType == 1)
        {
            rotVct = Vector3.Normalize(myParent.transform.forward * 2 + myParent.transform.right);
            initPos = rotPos+Vector3.Normalize(myParent.transform.right * (-2) + myParent.transform.forward) * radii;
        }
        if (elecType == 2)
        {
            rotVct = Vector3.Normalize(myParent.transform.up + myParent.transform.right * 2);
            initPos = rotPos+Vector3.Normalize(myParent.transform.up * (-2) + myParent.transform.right) * radii;
        }

        gameObject.transform.position = initPos;
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.RotateAround(rotPos, rotVct, speed * Time.deltaTime);
	}
}
