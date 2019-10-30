using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ReplacementShader : MonoBehaviour
{
    public Shader RepShader;
   void OnEnable()
    {
        if (RepShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(RepShader, "");
        }
    }
    void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
   
}
