
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CarDataScriptableObject", order = 1)]
public class CarDataScriptableObject : ScriptableObject
{
    public string CarType;
    [Range(0.0f, 5.0f)]
    public float Speed;

    [Range(.5f, 5.0f)]
    public float Grip;

    [Range(0.01f, .1f)]
    public float Acceleration;

    public GameObject MeshObject;

   // [Range(100f, 2000f)]
    public int CarPrice;

}
