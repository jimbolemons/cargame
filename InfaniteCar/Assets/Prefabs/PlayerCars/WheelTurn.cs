using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelTurn : MonoBehaviour
{
    

    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    float tiltAroundX;
    PlayerInput input;
    void Start()
    {
        input = PlayerInput.instance;            
    }

    void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        float tiltAroundZ = Input.GetAxis("Horizontal");
        transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, 0, tiltAroundX), Time.deltaTime * smooth);
        // Debug.Log(tiltAroundZ);
        if (sceneName == "MainScene")
        {
           
            if (input.Right())
            {
                //Debug.Log("Right1"); // this is getting called 
                tiltAroundX = 1 * tiltAngle; // why the fuck does this not work, it is identical to the left fucntion but does nothing
                //Debug.Log("Right2");// this is getting called 
            }else if (input.Left())
            {
                //Debug.Log("left");
                tiltAroundX = -1 * tiltAngle;
            }          
            else
            {
               // Debug.Log("not Right");

                tiltAroundX = 0 * tiltAngle;
            }

           
        }
        


    }
}
