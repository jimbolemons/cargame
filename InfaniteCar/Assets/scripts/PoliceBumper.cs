﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PoliceBumperside
{
    Left, Right, Front, Back
}

public class PoliceBumper : MonoBehaviour
{
    public PoliceBumperside policeBumperside = PoliceBumperside.Front;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Building") return;


        switch (policeBumperside)
        {
            case PoliceBumperside.Front:
                // kill the car
                Debug.Log("bump Frount");
                break;
            case PoliceBumperside.Right:
                //push the car to the left
                Debug.Log("bump Right");
               // this.GetComponent<AIDriver>().Bump(10);
                break;
            case PoliceBumperside.Left:
                //push the car to the right
                Debug.Log("bump Left");
                //this.GetComponent<AIDriver>().Bump(10);
                break;
            case PoliceBumperside.Back:
                Debug.Log("bump Back");
                //push the car forward/maybe spin it out

                break;

        }


    }
}