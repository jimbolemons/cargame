using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class randomOnOff : MonoBehaviour
{
    private ParticleSystem particle;
    bool on = true;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (on)
        {

        }
        else if (!on)
        {

        }

        
    }

    void Play()
    {
        particle.Play();
        particle.enableEmission = true;
        
        
    }
    void Stop()
    {
        particle.Stop();
        particle.enableEmission = false;
    }
}
