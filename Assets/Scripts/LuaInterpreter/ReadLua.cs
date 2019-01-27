using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoonSharp.Interpreter;

public class ReadLua : MonoBehaviour
{
    [SerializeField]
    private float[] forces;
    [SerializeField]
    private Vector3 vel;
    [SerializeField]
    private Vector3 ori;
    ScriptReader sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = new ScriptReader();
    }

    // Update is called once per frame

    void Update()
    {
      //  List<float> rVals = sr.RotorValues(@"function retForces() return currentRotorValues[1], currentRotorValues[2], currentRotorValues[3], currentRotorValues[4] end return retForces()", forces);
        //Vector3 vVal = sr.VelocityValues(@"function retVelocity() return currentVelocityValues[1], currentVelocityValues[2], currentVelocityValues[3] end return retVelocity()", vel);
        //Vector3 oVal = sr.OrientationValues(@"function retOrientation() return currentOrientationValues[1], currentOrientationValues[2], currentOrientationValues[3] end return retOrientation()", ori);

      //  Debug.Log(rVals[2]);
       // Debug.Log(vVal);
       // Debug.Log(oVal);
    }
}
