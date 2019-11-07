using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurn2 : MonoBehaviour
{
    PlayerInput input;

    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    float tiltAroundX;
    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;


        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, tiltAroundZ, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (input.Right())
        {
            tiltAroundX = 1 * tiltAngle;
        }
        else
        {
            tiltAroundX = 0 * tiltAngle;
        }
        if (input.Left())
        {
            tiltAroundX = -1 * tiltAngle;
        }
        else
        {
            tiltAroundX = 0 * tiltAngle;
        }

    }
}
