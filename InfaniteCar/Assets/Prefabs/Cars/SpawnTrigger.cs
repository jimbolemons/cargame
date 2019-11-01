using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player") return;
        AICarsManager.policeSpawned = false;

        Debug.Log("spawning new enemy......." +AICarsManager.policeSpawned);
    }
}
