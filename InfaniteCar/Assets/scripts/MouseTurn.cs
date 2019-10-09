using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTurn : MonoBehaviour    
{
    float speed = 5;
    float carRot = 0;   
    float max = 190;
    float min = 170;
    public float offset = 0;
    public float width = 2;
    public float Acceleration = .01f;
    public float BrakePower = .03f;
    public float turnAmount = .003f;
    Vector3 currentRotation;
    
    void Start()
    {
        //gets the starting position of the car on the very first frame
        //Vector3 startingPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        //gets the x position of the mouse
        carRot = Input.GetAxis("Mouse X") ;

        //uses the position of the mouse to rotate the car
        transform.Rotate(0, carRot * speed, 0, Space.Self);

        // sets the variable currentrot to be the current rotation of the car
        Vector3 currentRot = transform.localRotation.eulerAngles;

        //clamps the degrees of what the car can turn
        currentRot.y = Mathf.Clamp(currentRot.y, min, max);

        //uses the clamp to tell the car how far it is alowed to rotate
        transform.localRotation = Quaternion.Euler(currentRot);

        if (currentRot.y < 180)
        {
            //Debug.Log("left");
            //angle will come out as a number 170-180 subtract 180 and you get -1 through -10 multiply by -1 to get 1-10 and then multiply that by the speed
            float left = ((currentRot.y - 180)* -1) * turnAmount;
            offset = Mathf.Clamp(offset - left, -width, width);
        }
        else if (currentRot.y > 180)
        {
           // Debug.Log("right");
            //angle will come out as a number 180-190 subtract 180 and you get 1-10 and then multiply that by the speed
            float right = (currentRot.y - 180) * turnAmount;
            offset = Mathf.Clamp(offset + right, -width, width);
        }
        //applies movement to the player based on how far they are turning left or right
        transform.position = new Vector3(offset,transform.position.y , transform.position.z);               
    }
}
