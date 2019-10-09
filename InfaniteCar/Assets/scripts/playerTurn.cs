using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTurn : MonoBehaviour
{
    Quaternion origRot;
    float rotateSpeed = 0.1f;
    bool restoreRot = false;

   // float speed = 5;
   // float carRot = 0;

    public float xAngle;
    public float yAngle;
    public float zAngle;
    float angleOfCar;


    PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        origRot = transform.rotation;
        input = PlayerInput.instance;

    }

    // Update is called once per frame
    void FixedUpdate()
    {



        angleOfCar = transform.eulerAngles.y;
        angleOfCar = Mathf.Clamp(transform.rotation.y,-30,30);
        if (input.Left())
        {
            Debug.Log("turn left");
            transform.Rotate(xAngle, -yAngle, zAngle, Space.Self);
            angleOfCar = Mathf.Clamp(angleOfCar, -30, 30);

            // turn car to the left
        }
        

        if (input.Right())
        {
            Debug.Log("turn right");
            transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
            angleOfCar = Mathf.Clamp(angleOfCar, -30, 30);
            // turn car to right
        }

        if (((input.RightRelease()) || (input.LeftRelease())) && !restoreRot)
        {
            restoreRot = true;
        }

        if (restoreRot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, origRot, Time.time * rotateSpeed);

            if (transform.rotation == origRot)
            {
                restoreRot = false;
            }
        }
    }
}
