using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class randomOnOff : MonoBehaviour
{
    private ParticleSystem particle;
    public static bool speedOn = false;
    bool on = false;
    bool timeOnNow = true;
    float timeOn;
    float timeOff;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        timeOn = Random.Range(.01f, .5f);
    }

    void Update()
    {
        if (speedOn)
        {
            if (timeOnNow)
            {
                if (timeOn > 0)
                {
                    on = true;
                    timeOn -= Time.deltaTime;
                    //Debug.Log("wtf");
                }
                else
                {

                    timeOn = Random.Range(.01f, .5f);
                    timeOnNow = false;
                }
            }


            if (!timeOnNow)
            {
                if (timeOff > 0)
                {
                    //Debug.Log("wtf2");
                    on = false;
                    timeOff -= Time.deltaTime;
                }
                else
                {
                    timeOff = Random.Range(.01f, .5f);
                    timeOnNow = true;
                }
            }
            Debug.Log(timeOnNow);
            Debug.Log(timeOff);

        }
            if (Input.GetKey(KeyCode.X))
               speedOn = true;
             else
            //speedOn = false;

            if (on)
            {
                Play();
            }
            else if (!on)
            {
                Stop();
            }
        if (!speedOn)
        {
            Stop();
        }
        
        
    }

    void Play()
    {
        particle.Play();
       // particle.enableEmission = true;
        
        
    }
    void Stop()
    {
        particle.Stop();
        //particle.enableEmission = false;
    }
}
