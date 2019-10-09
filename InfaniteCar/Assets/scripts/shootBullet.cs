using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
    public GameObject bullet;
    PlayerInput input;
    public List<bulletController> bullets = new List<bulletController>();
    List<bulletController> bulletsToRemove = new List<bulletController>();   
    public float currentSpeed = .01f;
    public Transform barrel;
    public float thrust = 50;
    //public float time = 1;
    // public int numberOfBullets = 10;

    void Start()
    {
        input = PlayerInput.instance;
    }    
    void FixedUpdate()
    {
     // create bullets based on player rotation
     CreateBullet();
     //moves bullets
     MoveBullet();
     //updates bullets 
     UpdateBullets();
     //deletes bullets
     DeleteBullets();        
    }

    void CreateBullet()
    {
        //checks for space bar input
        if (input.Action())
        {
            AddBullet();
        }        
    }

    void MoveBullet()
    {
        foreach (bulletController obj in bullets)
        {
            //obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + currentSpeed);
           // obj.transform.position += obj.transform.forward * -currentSpeed;
        }
    }

    void UpdateBullets()
    {
        foreach (bulletController obj in bullets)
        {
            //check to see how long it has been alive 
            // if it has been alive for more then 3 seconds kill it
            if (obj.markedForDeath == true)
            {
                bulletsToRemove.Add(obj);
            }
            //if bullet gets too far away kill it
            if (obj.transform.position.z > 20) {
                //Debug.Log("kill me");
                bulletsToRemove.Add(obj);
            }
          //if (bullet.collision == true)
          // {
          //    bulletsToRemove.Add(obj); 
          // }        
         
            //check to see if bullet hits somthing
            //if it has mark for death and do damage 
        }
    }

    void DeleteBullets()
    {
        if (bulletsToRemove.Count > 0)
        {
            //destroys all bullets that are in the bullets to remove list 
            foreach (bulletController obj in bulletsToRemove)
            {
                bullets.Remove(obj);
                Destroy(obj.gameObject);
            }
            //clears bullets to remove after destorying all bullets
            bulletsToRemove.Clear();
        }
    }

    void AddBullet()
    {
        // Debug.Log("pew");
        // creates bullet based on the position of the barrel obj and adds them to the bullets list
        bulletController obj = Instantiate(bullet, barrel.position, barrel.rotation, null).GetComponent<bulletController>();       
        bullets.Add(obj);
      //  obj.GetComponent<Rigidbody>().AddForce(transform.forward * thrust,ForceMode.Force);
       
    }
}
