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
	GameObject[] engines = new GameObject[4];
	public float[] currentForces {get; set;}
	public DroneEvent events;
	[SerializeField]
	string[] testKeys = new string[4];
	[SerializeField]
	float testForce = 50;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		if (events == null) {
          events = new DroneEvent();
		}
		
        //events.AddListener(TestEventListener);
    }

    void Update()
    {
		int invert = 1;
		if(Input.GetKey("left shift")) {
			invert = -1;
		}
		//TODO: REMOVE
		float[] newForces = new float[GetEngineCount()];	
        for(int i=0; i < engines.Length; i++) {
			newForces[i] = Input.GetKey(testKeys[i]) ? testForce : 0;
	    }
		currentForces = newForces;
		//REMOVE ENDS
       for(int i=0; i < engines.Length; i++) {
			engines[i].GetComponent<DroneEngine>().currentForce = (currentForces.Length > i ? currentForces[i] : 0) * invert;
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
