using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{

    public spawner _spawner;

    //start and end markers
    public Vector3 start;
    public Vector3 end;

    // speed in units/sec

    public float blockSpeed;

    //time when movment started
    private float startTime;

    // total distance between start and end
    private float journeyLength;

 
    void Start()
    {

        
        startTime = Time.time;

        journeyLength = Vector3.Distance(start, end);
        
    }

    void FixedUpdate()
    {

        blockSpeed = _spawner.speed;

        float disCovered = (Time.time - startTime) * blockSpeed;

        float fracJourney = disCovered / journeyLength;

        transform.position = Vector3.Lerp(start, end, fracJourney);

        if (this.transform.position == end)
        {

            Destroy(this.gameObject);
        }
    }
}
