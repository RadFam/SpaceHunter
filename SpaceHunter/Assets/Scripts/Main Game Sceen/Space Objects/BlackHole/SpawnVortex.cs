using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVortex : MonoBehaviour
{
    public GameObject vortexMesh;
    [SerializeField]
    Transform respawnPoint;
    [SerializeField]
    Transform exitPoint;

    public Transform GetRespawn()
    {
        return respawnPoint;
    }

    public Transform GetExit()
    {
        return exitPoint;
    }

    void Update()
    {
        vortexMesh.transform.RotateAround(transform.position, transform.forward, -10.0f * Time.deltaTime);
    }
}
