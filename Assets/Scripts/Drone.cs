using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DroneEvent : UnityEvent<Vector3, Vector3> {}

public class Drone : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField]
	DroneEngine[] engines = new DroneEngine[4];
	public float[] currentForces {get; set;}
	public DroneEvent events;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		if (events == null) {
          events = new DroneEvent();
		}
		
        events.AddListener(TestEventListener);
    }

    void Update()
    {
		//TODO: REMOVE
		float multiplier = 50;
		float[] newForces = new float[4];
		newForces[0] = Input.GetKey("g") ? multiplier : 0;
		newForces[1] = Input.GetKey("h") ? multiplier : 0;
		newForces[2] = Input.GetKey("k") ? multiplier : 0;
		newForces[3] = Input.GetKey("l") ? multiplier : 0;
		currentForces = newForces;
		//REMOVE ENDS
       for(int i=0; i < engines.Length; i++) {
		Debug.Log("FORCE" + i + ": " + currentForces[i]);
		engines[i].currentForce = currentForces.Length > i ? currentForces[i] : 0;
	   }
	   events.Invoke(transform.eulerAngles, rb.velocity);
    }
	
	int GetEngineCount() {
		return engines.Length;
	}
	
	void TestEventListener(Vector3 orientation, Vector3 velocity) {
	   Debug.Log("VELOCITY " + velocity + " ORIENTION " + orientation);		
	}
}
