using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")){
            rb.AddForce(10,0,10);
        }
    }
}
