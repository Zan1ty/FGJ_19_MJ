using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneForceEngine : MonoBehaviour, DroneEngine
{
    Rigidbody rb;
	public float currentForce {get; set;}
	[SerializeField]
	Vector3 unitVector = new Vector3(0,0,1);
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
		rb.AddRelativeForce(unitVector * currentForce);
    }
}
