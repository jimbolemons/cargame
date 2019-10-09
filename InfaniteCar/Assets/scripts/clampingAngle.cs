using UnityEngine;
using System.Collections;

public class clampingAngle : MonoBehaviour
{
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tMin = min;
        var tAngle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tMin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tAngle -= 180;
        }
        var result = !inverse ? tAngle > tMin : tAngle < tMin;
        if (!result)
            angle = min;

        inverse = false;
        tAngle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tAngle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tAngle < tmax : tAngle > tmax;
        if (!result)
            angle = max;
        return angle;
    }
}