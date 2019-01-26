using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class ScriptReader
{
    public List<float> RotorValues(string script, float[] forces)
    {
        List<float> vals = new List<float>();
        Script s = new Script();
        s.Globals["currentRotorValues"] = forces;
        try
        {
            DynValue res = s.DoString(script);
            if (res.Tuple != null)
                for (int i = 0; i < res.Tuple.Length; i++)
                {
                    vals.Add((float)res.Tuple[i].Number);
                }   
        } catch (ScriptRuntimeException ex)
        {
            Debug.Log(ex);
        }
        return vals.Count > 0 ? vals : new List<float> { 2, 2, 2, 2 };
    }

    public Vector3 VelocityValues(string script, Vector3 vals)
    {
        Script s = new Script();
        float[] arr = new float[3];
        s.Globals["currentVelocityValues"] = Vector3ToArr(vals);
        try
        {
            DynValue res = s.DoString(script);
            if (res.Tuple != null)
                for (int i = 0; i < 3; i++)
                {
                    arr[i] = (float)res.Tuple[i].Number;   
                }
        } catch (ScriptRuntimeException ex)
        {
            Debug.Log(ex);
        }

        return ArrToVector3(arr);
    }

    public Vector3 OrientationValues(string script, Vector3 vals)
    {
        Script s = new Script();
        float[] arr = new float[3];
        Debug.Log(Vector3ToArr(vals));
        s.Globals["currentOrientationValues"] = Vector3ToArr(vals);
        try
        {
            DynValue res = s.DoString(script);
            if (res.Tuple != null)
                for (int i = 0; i < 3; i++)
                {
                    arr[i] = (float)res.Tuple[i].Number;
                }
        } catch (ScriptRuntimeException ex)
        {
            Debug.Log(ex);
        }

        return ArrToVector3(arr);
    }

    float[] Vector3ToArr(Vector3 vec)
    {
        return new float[] { vec.x, vec.y, vec.z };
    }

    Vector3 ArrToVector3(float[] arr)
    {
        return new Vector3(arr[0], arr[1], arr[2]);
    }
}
