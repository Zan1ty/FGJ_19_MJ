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
    public string script { get; set; }
	public DroneEvent events;
	[SerializeField]
	string[] testKeys = new string[4];
	[SerializeField]
	float testForce = 50;
    ScriptReader sr;

    void Awake()
    {
        script = @"function retForces() currentRotorValues[1] = 50 currentRotorValues[2] = 50 currentRotorValues[3] = 50 currentRotorValues[4] = 50 return currentRotorValues[1], currentRotorValues[2], currentRotorValues[3], currentRotorValues[4] end return retForces()";
    }
	bool manual = true;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		if (events == null) {
          events = new DroneEvent();
		}

        sr = new ScriptReader();

        //events.AddListener(TestEventListener);
    }

    void FixedUpdate()
    {
		if(Input.GetKeyDown("space")) {
			manual = !manual;
		}
        int invert = 1;
        //TODO: REMOVE
       /* if (Input.GetKey("left shift"))
        {
            invert = -1;
        }
        float[] newForces = new float[GetEngineCount()];
        for (int i = 0; i < engines.Length; i++)
        {
            newForces[i] = Input.GetKey(testKeys[i]) ? testForce : 0;
        }
        currentForces = newForces; */
        //REMOVE ENDS

        float[] newForces = new float[4];
        newForces = sr.RotorValues(script, currentForces, rb.velocity, transform.eulerAngles, transform.position, 0f);
        currentForces = newForces;

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
		/* Dictionary<string, Vector3> vectors = new Dictionary<string, Vector3>() {
			{"up", transform.up},
			{"down", -transform.up},
			{"right", transform.right},
			{"left", -transform.right},
			{"forward", transform.forward},
			{"backward", -transform.forward}
		};*/
		// Global		
		Dictionary<string, Vector3> vectors = new Dictionary<string, Vector3>() {
			{"up", Vector3.up},
			{"down", -Vector3.up},
			{"right", Vector3.right},
			{"left", -Vector3.right},
			{"forward", Vector3.forward},
			{"backward", -transform.forward}
		};
		
		Dictionary<string, Vector3> results = new Dictionary<string, Vector3>();
		
		foreach (KeyValuePair<string, Vector3> vector in vectors) {			
			RaycastHit hit;
			if (Physics.Raycast(new Ray(transform.position, vector.Value), out hit))
			{
				results.Add(vector.Key, vector.Value * hit.distance);
				//Debug.DrawRay(transform.position, vector.Value, Color.green);
			} else {
				//Debug.DrawRay(transform.position, vector.Value, Color.red);
			}
		}
		foreach (KeyValuePair<string, Vector3> result in results) {
			Debug.Log(result.Key + ": " + result.Value);
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
