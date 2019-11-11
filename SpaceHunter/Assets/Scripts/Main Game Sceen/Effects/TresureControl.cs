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

    public float speedUp = 3.0f;
    public float maxHeight;
    public bool canMove = false;


    //public Animator myAnim;

    void Start()
    {
        //myAnim.Play();
    }

    public void SetMineralParams(Material mat, MeshFilter mesh)
    {
        MeshFilter my_mf = (MeshFilter)myMineral.AddComponent<MeshFilter>();
        MeshRenderer my_mr = (MeshRenderer)myMineral.AddComponent<MeshRenderer>();
        my_mf = mesh;
        my_mr.material = mat;
    }

    public string ReturnTreasureName()
    {
        return myName;
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
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (transform.localPosition.z >= maxHeight)
            {
                myCollider.enabled = true;
                canMove = false;
            }
        }
    }
}
