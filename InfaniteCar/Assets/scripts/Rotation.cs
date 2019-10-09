using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 RotationAmount;
    void FixedUpdate(){
    	this.transform.eulerAngles += RotationAmount *Time.fixedDeltaTime;
    }
}
