using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject cube;
    public Transform center;
    public bool orbitSwitch;
    public Vector3 axis = Vector3.right;    
    public Vector3 desiredPosition;
    public float radius;
    public float radius2;
    public float radiusSpeed;
    public float rotationSpeed = 80.0f;

    void Start()
    {
        //cube = GameObject.FindWithTag("Cube");
        center = cube.transform;       
        
        //radius = .5f;
        if (orbitSwitch)
        { transform.position = (transform.position - center.position).normalized * -radius + center.position; }
        if (!orbitSwitch)
        { transform.position = (transform.position - center.position).normalized * radius + center.position; }

        }

    void Update()
    {
        
       
            transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            desiredPosition = (transform.position - center.position).normalized * radius2 + center.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        
        
           

        
    }
}