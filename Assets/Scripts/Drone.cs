using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DroneEvent : UnityEvent< Vector3, Vector3, Dictionary<string, Vector3>> {}

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
    ScriptReader sr;
	bool manual = false;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		if (events == null) {
          events = new DroneEvent();
		}

        sr = new ScriptReader();

        //events.AddListener(TestEventListener);
    }

    void Update()
    {
		if(Input.GetKeyDown("space")) {
			manual = !manual;
		}
        int invert = 1;
        if (manual) {
			if (Input.GetKey("left shift"))
			{
				invert = -1;
			}
			float[] newForces = new float[GetEngineCount()];
			for (int i = 0; i < engines.Length; i++)
			{
				newForces[i] = Input.GetKey(testKeys[i]) ? testForce : 0;
			}
			currentForces = newForces;
        } else {
			float[] newForces = new float[4];
			newForces = sr.RotorValues(@"function retForces() return currentRotorValues[1] + 1 * 50, currentRotorValues[2] + 1 * 50, currentRotorValues[3] + 1 * 50, currentRotorValues[4] + 1 * 50 end return retForces()", currentForces, rb.velocity, transform.eulerAngles, transform.position, 0f);
			currentForces = newForces;
		}

        for(int i=0; i < engines.Length; i++) {
			engines[i].GetComponent<DroneEngine>().currentForce = (currentForces.Length > i ? currentForces[i] : 0) * invert;
	    }
	    events.Invoke(GetGyroscopeData(), GetAccelerometerData(), GetLaserDistanceData());
    }
	
	public Vector3 GetGyroscopeData() {
		return transform.eulerAngles;
	}
	
	public Vector3 GetAccelerometerData() {
		return rb.velocity;
	}
	
	public Dictionary<string, Vector3> GetLaserDistanceData() {
		// Local 
		/* Dictionary<string, Ray> rays = new Dictionary<string, Ray>() {
			{"up", new Ray(transform.position, transform.up)},
			{"down", new Ray(transform.position, -transform.up)},
			{"right", new Ray(transform.position, transform.right)},
			{"left", new Ray(transform.position, -transform.right)},
			{"forward", new Ray(transform.position, transform.forward)},
			{"backward", new Ray(transform.position, -transform.forward)}
		};*/
		// Global		
		Dictionary<string, Ray> rays = new Dictionary<string, Ray>() {
			{"up", new Ray(transform.position, Vector3.up)},
			{"down", new Ray(transform.position, -Vector3.up)},
			{"right", new Ray(transform.position, Vector3.right)},
			{"left", new Ray(transform.position, -Vector3.right)},
			{"forward", new Ray(transform.position, Vector3.forward)},
			{"backward", new Ray(transform.position, -Vector3.forward)}
		};
		
		Dictionary<string, Vector3> results = new Dictionary<string, Vector3>();
		
		foreach (KeyValuePair<string, Ray> ray in rays) {			
			RaycastHit hit;
			if (Physics.Raycast(ray.Value, out hit))
			{
				Debug.Log(ray.Key + ": " + (-transform.up * hit.distance));
			}
		}
		return results;
	}
	
	public int GetEngineCount() {
		return engines.Length;
	}
	
	void TestEventListener(Vector3 orientation, Vector3 velocity) {
	   Debug.Log("VELOCITY " + velocity + " ORIENTION " + orientation);		
	}
}
