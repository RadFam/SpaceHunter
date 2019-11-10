using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureControl : MonoBehaviour {

    public Material innerMat;
    public MeshFilter innerMesh;

    public GameObject myMineral;

    public int myNum;
    public string myName;

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
}
