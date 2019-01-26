using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class DroneWheelEngine : MonoBehaviour, DroneEngine
{
    WheelCollider wc;
	public float currentForce {get; set;}
	
    void Start()
    {
        wc = GetComponent<WheelCollider>();
    }

    void FixedUpdate()
    {
		//	wc.motorTorque = Input.GetAxis("Vertical") * currentForce;
    }
}
