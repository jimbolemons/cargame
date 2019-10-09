using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{
  

    private void OnTriggerStay(Collider trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {

            GameManager.instance.AICars.RemoveDriver(trig.gameObject.GetComponent<AIDriver>());               

        }

        if (trig.gameObject.tag != "Enemy")
        {

            //isdead = true;
            //Debug.Log(trig.gameObject.tag);
        }
    }
}
