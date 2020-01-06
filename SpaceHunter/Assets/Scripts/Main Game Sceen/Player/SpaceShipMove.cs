using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMove : MonoBehaviour {

    public GameObject ctrlObject;
    public GameObject shipObject;

    /*
    public GameObject leftGun;
    public GameObject rightGun;
    public ListParticle allPlasmaShots;

    private float timerShot;
    private float deltaTimeShot = 0.1f;
    */
 
    private float speed = 15.0f;
    private float rotspeed = 40.0f;
    public Rigidbody rb;
    public bool isDead = false;

    private bool mooveU = false;
    private bool mooveUR = false;
    private bool mooveL = false;
    private bool mooveLR = false;

    private int counterUp = 0;
    private int counterLeft = 0;

    private float specTime = 0.01f;
    private Quaternion initSelfRotate;
    
    // Use this for initialization
	void Start () 
    {
        //timerShot = deltaTimeShot;
        isDead = false;
        initSelfRotate = shipObject.transform.localRotation;
        rb.velocity = rb.transform.forward * speed;
        //StartCoroutine(CorrectorPlayerOrient());
	}
	
	// Update is called once per frame
	void Update () 
    {    
        if (!isDead)
        {
            // События от клавиатуры
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!mooveUR)
                {
                    ctrlObject.transform.RotateAround(ctrlObject.transform.position, ctrlObject.transform.right, -20.0f * Time.deltaTime); // was -0.5f
                    if (!mooveU)
                    {
                        //StopCoroutine(UpsideNormalisationCoroutine());
                        mooveU = true;
                        //StartCoroutine(UpsideCoroutine());
                    }
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!mooveU)
                {
                    ctrlObject.transform.RotateAround(ctrlObject.transform.position, ctrlObject.transform.right, 20.0f * Time.deltaTime); // was 0.5f
                    if (!mooveUR)
                    {
                        //StopCoroutine(UpsideNormalisationCoroutine());
                        mooveUR = true;
                        //StartCoroutine(UpsideRCoroutine()); 
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                mooveU = false;
                //StopCoroutine(UpsideNormalisationCoroutine());
                //StopCoroutine(UpsideCoroutine());
                //StartCoroutine(UpsideNormalisationCoroutine());
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                mooveUR = false;
                //StopCoroutine(UpsideNormalisationCoroutine());
                //StopCoroutine(UpsideRCoroutine());
                //StartCoroutine(UpsideNormalisationCoroutine());
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!mooveLR)
                {
                    ctrlObject.transform.RotateAround(ctrlObject.transform.position, ctrlObject.transform.up, -1.0f * rotspeed * Time.deltaTime); // was -0.5f
                    if (!mooveL)
                    {
                        //StopCoroutine(LeftsideNormalisationCoroutine());
                        mooveL = true;
                        //StartCoroutine(LeftsideCoroutine());
                    }
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!mooveL)
                {
                    ctrlObject.transform.RotateAround(ctrlObject.transform.position, ctrlObject.transform.up, rotspeed * Time.deltaTime); // was 0.5f
                    if (!mooveLR)
                    {
                        //StopCoroutine(LeftsideNormalisationCoroutine());
                        mooveLR = true;
                        //StartCoroutine(LeftsideRCoroutine());
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                mooveL = false;
                //StopCoroutine(LeftsideNormalisationCoroutine());
                //StopCoroutine(LeftsideCoroutine());
                //StartCoroutine(LeftsideNormalisationCoroutine());
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                mooveLR = false;
                //StopCoroutine(LeftsideNormalisationCoroutine());
                //StopCoroutine(LeftsideRCoroutine());
                //StartCoroutine(LeftsideNormalisationCoroutine());
            }

            // Corrector
            if (specTime >= 0.01f)
            {
                specTime = 0.0f;
                // down self-rotation
                if (mooveUR && !mooveU && (counterUp > -5))
                {
                    shipObject.transform.Rotate(-1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp--;
                }

                // up self-rotation
                if (mooveU && !mooveUR && (counterUp < 5))
                {
                    shipObject.transform.Rotate(1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp++;
                }

                // left self-rotation
                if (mooveL && !mooveLR && (counterLeft > -5))
                {
                    shipObject.transform.Rotate(0.0f, -1.6f, 0.0f, Space.Self);
                    counterLeft--;
                }

                // right self-rotation
                if (mooveLR && !mooveL && (counterLeft < 5))
                {
                    shipObject.transform.Rotate(0.0f, 1.6f, 0.0f, Space.Self);
                    counterLeft++;
                }

                // normalize up/down orientation
                if (!mooveU && !mooveUR && (counterUp != 0))
                {
                    //Debug.Log("counterUp_before: " + counterUp.ToString());
                    int sign = counterUp / Mathf.Abs(counterUp);
                    shipObject.transform.Rotate(sign * (-1.6f), 0.0f, 0.0f, Space.Self);
                    counterUp = counterUp - sign;
                    //Debug.Log("counterUp_after: " + counterUp.ToString());
                }

                // normalize left/rigth orientation
                if (!mooveL && !mooveLR && (counterLeft != 0))
                {
                    //Debug.Log("counterLeft_before: " + counterLeft.ToString());
                    int sign = counterLeft / Mathf.Abs(counterLeft);
                    shipObject.transform.Rotate(0.0f, sign * (-1.6f), 0.0f, Space.Self);
                    counterLeft = counterLeft - sign;
                    //Debug.Log("counterLeft_after: " + counterLeft.ToString());
                }

                if (!mooveL && !mooveLR && !mooveU && !mooveUR && (counterLeft == 0) && (counterUp == 0))
                {
                    shipObject.transform.localRotation = initSelfRotate;
                }
            }

            specTime += Time.deltaTime;

            rb.velocity = rb.transform.forward * speed;
        }
        if (isDead && (Mathf.Abs(speed) >= 0.25f))
        {
            rb.velocity = rb.transform.forward * speed;
            speed -= 0.25f;
        }
        if (isDead && (Mathf.Abs(speed) < 0.25f))
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            speed = 0.0f;
        }
	}

    public IEnumerator UpsideCoroutine()
    {      
        while (counterUp < 5)
        {
            shipObject.transform.Rotate(1.6f, 0.0f, 0.0f, Space.Self);
            counterUp++;
            yield return new WaitForSeconds(0.01f);
        }

        //mooveU = false;
    }

    public IEnumerator UpsideRCoroutine()
    {
        while (counterUp > -5)
        {
            shipObject.transform.Rotate(-1.6f, 0.0f, 0.0f, Space.Self);
            counterUp--;
            yield return new WaitForSeconds(0.01f);
        }

        //mooveUR = false;
    }

    public IEnumerator UpsideNormalisationCoroutine()
    {
        while (counterUp != 0)
        {
            Debug.Log("UpsideNormalisationCoroutine    counterUp: " + counterUp.ToString());
            int sign = counterUp / Mathf.Abs(counterUp);
            shipObject.transform.Rotate(sign*(-1.6f), 0.0f, 0.0f, Space.Self);
            counterUp = counterUp - sign;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator LeftsideCoroutine()
    {
        while (counterLeft > -5)
        {
            shipObject.transform.Rotate(0.0f, -1.6f, 0.0f, Space.Self);
            counterLeft--;
            yield return new WaitForSeconds(0.01f);
        }

        //mooveL = false;
    }

    public IEnumerator LeftsideRCoroutine()
    {
        while (counterLeft < 5)
        {
            shipObject.transform.Rotate(0.0f, 1.6f, 0.0f, Space.Self);
            counterLeft++;
            yield return new WaitForSeconds(0.01f);
        }

        //mooveLR = false;
    }

    public IEnumerator LeftsideNormalisationCoroutine()
    {
        while (counterLeft != 0)
        {
            Debug.Log("LeftsideNormalisationCoroutine  (before)  counterLeft: " + counterLeft.ToString());
            int sign = counterLeft / Mathf.Abs(counterLeft);
            shipObject.transform.Rotate(0.0f, sign * (-1.6f), 0.0f, Space.Self);
            counterLeft = counterLeft - sign;
            Debug.Log("LeftsideNormalisationCoroutine  (after)  counterLeft: " + counterLeft.ToString());
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator CorrectorPlayerOrient()
    {
        // normalize up/down orientation
        if (!mooveU && !mooveUR && (counterUp != 0))
        {
            int sign = counterUp / Mathf.Abs(counterUp);
            shipObject.transform.Rotate(sign * (-1.6f), 0.0f, 0.0f, Space.Self);
            counterUp = counterUp - sign;
        }

        // normalize left/rigth orientation
        if (!mooveL && !mooveLR && (counterLeft != 0))
        {
            int sign = counterLeft / Mathf.Abs(counterLeft);
            shipObject.transform.Rotate(0.0f, sign * (-1.6f), 0.0f, Space.Self);
            counterLeft = counterLeft - sign;
        }
        yield return new WaitForSeconds(0.01f);
    }
}
