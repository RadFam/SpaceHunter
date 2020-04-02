using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloating : MonoBehaviour
{
    public Transform defendObject;
    [SerializeField]
    float floatRadii;
    [SerializeField]
    float rotateSpeed;

    Vector3 orbitalCenter;
    Vector3 orbitalUp;
    
    // Start is called before the first frame update
    void Start()
    {
        orbitalCenter = defendObject.position;
        orbitalUp = defendObject.up;

        transform.position = new Vector3(defendObject.position.x - floatRadii, defendObject.position.y, defendObject.position.z);
        transform.LookAt(defendObject, transform.up);
        transform.RotateAround(transform.position, transform.up, 180.0f);
    }

    // Update is called once per frame
    void Update()
    {
        orbitalCenter = defendObject.position;
        orbitalUp = defendObject.up;
        transform.RotateAround(orbitalCenter, orbitalUp, rotateSpeed * Time.deltaTime);
    }
}
