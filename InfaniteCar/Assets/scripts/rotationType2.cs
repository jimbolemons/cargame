using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationType2 : MonoBehaviour
{
    Quaternion origRot;
    float rotateSpeed = 0.1f;
    bool restoreRot = false;


    // Start is called before the first frame update
    void Start()
    {
        origRot = transform.rotation;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A) && !restoreRot)
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
