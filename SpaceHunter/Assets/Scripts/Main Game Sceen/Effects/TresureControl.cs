using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureControl : MonoBehaviour {

    public Material innerMat;
    public MeshFilter innerMesh;

    public GameObject myMineral;
    public Collider myCollider;

    public int myNum;
    public string myName;
    public string mySecondName;

    public float speedUp = 3.0f;
    public float maxHeight;
    public bool canMove = false;

    private Vector3 initPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;

    //public Animator myAnim;

    void Start()
    {
        //myAnim.Play();
    }

    public void SetMineralParams(Material mat, MeshFilter mesh, string name, string nameSecond)
    {
        MeshFilter my_mf = (MeshFilter)myMineral.AddComponent<MeshFilter>();
        MeshRenderer my_mr = (MeshRenderer)myMineral.AddComponent<MeshRenderer>();
        my_mf.mesh = mesh.sharedMesh;
        my_mr.material = mat;
        myMineral.transform.localScale = new Vector3(100.0f, 100.0f, 900.0f);

        myName = name;
        mySecondName = nameSecond;
    }

    public string ReturnTreasureName()
    {
        return myName;
    }

    public string ReturnTreasureSecondName()
    {
        return mySecondName;
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

    void Update()
    {
        if (canMove)
        {
            float distCovered = (Time.time - startTime)*speedUp;
            float fractionOfJourney = distCovered/journeyLength;
            gameObject.transform.localPosition = Vector3.Lerp(initPos, endPos, fractionOfJourney);
            
            if (transform.localPosition.y >= maxHeight)
            {
                myCollider.enabled = true;
                canMove = false;
            }
        }
    }
}
