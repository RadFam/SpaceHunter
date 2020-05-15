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

        float angle_1 = Vector3.Angle(defendObject.position, defendObject.up) - 90.0f;
        Quaternion q = Quaternion.AngleAxis(angle_1, defendObject.up);
        orbitalUp = q * orbitalUp;

        transform.position = new Vector3(defendObject.position.x - floatRadii, defendObject.position.y, defendObject.position.z);
        transform.LookAt(defendObject, transform.up);
        transform.RotateAround(transform.position, transform.up, 180.0f);
        transform.RotateAround(transform.position, transform.forward, angle_1);
        transform.right = defendObject.forward;
    }

    // Update is called once per frame
    void Update()
    {
        orbitalCenter = defendObject.position;
        //orbitalUp = defendObject.up;
        transform.position = new Vector3(transform.position.x, defendObject.position.y, transform.position.z);
        transform.RotateAround(orbitalCenter, orbitalUp, rotateSpeed * Time.deltaTime);
        transform.LookAt(defendObject, -1.0f*transform.up);

    }
}
