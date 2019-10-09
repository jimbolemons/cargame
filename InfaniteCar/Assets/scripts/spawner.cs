using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject myPrefab;
    public float speed = 20f;
    
    private float p;
    // Start is called before the first frame update

     void Awake()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true; 
    }
    void Start()

    {
        movingBlock Block = Instantiate(myPrefab, new Vector3(1, -2, 30), Quaternion.identity).GetComponent<movingBlock>();
        Block._spawner = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p -= Time.deltaTime;
        while (p <= 0)
        {
           
            p = .29f;
        }
    }

    private void OnTriggerEnter(Collider coolider)
    {
        Debug.Log("collision");
        movingBlock Block = Instantiate(myPrefab, new Vector3(1, -2, 30), Quaternion.identity).GetComponent<movingBlock>();
       // Block._spawner = this;
    }
}
