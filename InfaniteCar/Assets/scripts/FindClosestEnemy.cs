using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{
   
    void FixedUpdate()
    {
        FindClosest();
    }
    void FindClosest()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
         Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;

            }


        }


    }
}
