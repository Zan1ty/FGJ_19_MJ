using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEngine : MonoBehaviour
{
    Rigidbody rb;
	public float currentForce {get; set;}
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
		//Debug.Log(gameObject.name + ": " + currentForce);
		rb.AddRelativeForce(new Vector3(0,1,0) * currentForce);            
    }
}
