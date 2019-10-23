using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {

            GameManager.instance.AICars.RemoveDriver(trig.gameObject.GetComponent<AIDriver>());               

        }

        if (trig.gameObject.tag == "Police")
        {
            Debug.Log("splush");
            AIDriver driver = trig.GetComponent<AIDriver>();

            driver.HitOil();

            //GameManager.instance.AICars.RemoveDriver(trig.gameObject.GetComponent<AIDriver>());

            Destroy(this.gameObject);
            //isdead = true;
            //Debug.Log(trig.gameObject.tag);
        }
    }
}
