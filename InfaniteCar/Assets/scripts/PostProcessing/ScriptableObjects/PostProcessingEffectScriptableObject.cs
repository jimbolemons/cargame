using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PostProcessingEffectData", menuName = "ScriptableObjects/PostProcessingEffect", order = 1)]
public class PostProcessingEffectScriptableObject : ScriptableObject
{
    public float intensity = 1f;
    public Color color = Color.white;
    public GameObject EffectObject;

}
