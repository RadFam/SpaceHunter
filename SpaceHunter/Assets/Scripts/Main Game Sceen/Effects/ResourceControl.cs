using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceControl : MonoBehaviour {

    public Collider myCollider;

    public int myNum;
    public string myName;

    public float speedUp = 3.0f;
    public float maxHeight;
    public bool canMove = false;

    private Vector3 initPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;
    
    // Use this for initialization
	void Start () {
		
	}

    public void SetParams(string name)
    {
        myName = name;
    }

    public void SelfDisable()
    {
        // Тут, возможно, надо издать какой-нибудь звук

        // Само отключение элемента
        Destroy(gameObject);
    }

    public void StartMove(float val)
    {
        maxHeight = val;
        initPos = gameObject.transform.localPosition;
        endPos = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + maxHeight, gameObject.transform.localPosition.z);
        journeyLength = maxHeight;
        startTime = Time.time;

        canMove = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (canMove)
        {
            float distCovered = (Time.time - startTime) * speedUp;
            float fractionOfJourney = distCovered / journeyLength;
            gameObject.transform.localPosition = Vector3.Lerp(initPos, endPos, fractionOfJourney);

            if (transform.localPosition.y >= maxHeight)
            {
                myCollider.enabled = true;
                canMove = false;
            }
        }	
	}
}
