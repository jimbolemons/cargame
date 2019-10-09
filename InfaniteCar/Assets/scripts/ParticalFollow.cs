using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalFollow : MonoBehaviour
{
    private ParticleSystem ps;
    //public Transform relativeTo;
    public Vector3 moveTime;
    public Tile tile;
    

    void Start()
    {

        ps = GetComponent<ParticleSystem>();

       //var main = ps.main;
       // main.simulationSpace = ParticleSystemSimulationSpace.Custom;
       // main.customSimulationSpace = relativeTo;
    }

    void FixedUpdate()
    {
       
         tile = TileMover.instance.GetCurrentTile();


        // relativeTo = tile.transform;
        //ps.transform.position = tile.transform.position;

         var main = ps.main;
         //main.simulationSpace = ParticleSystemSimulationSpace.Custom;       
         main.customSimulationSpace = this.transform;
    }
}