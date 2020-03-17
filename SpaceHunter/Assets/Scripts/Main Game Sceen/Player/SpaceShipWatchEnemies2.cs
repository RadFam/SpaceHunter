using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipWatchEnemies2 : MonoBehaviour
{
    [SerializeField]
    private float sightDist = 500.0f;
    public Transform cameraTransform;

    public float SightDist
    {
        get { return sightDist; }
    }

    public Vector3 UpVector
    {
        get { return cameraTransform.up; }
    }
}
