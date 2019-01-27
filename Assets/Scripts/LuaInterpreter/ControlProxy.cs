using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class ScriptReader
{
    public float[] RotorValues(string script, float[] forces, Vector3 velocity, Vector3 orientation, Vector3 position, float acceleration)
    {
        float[] vals = new float[4];
        Script s = new Script();
        s.Globals["currentRotorValues"] = forces;
        s.Globals["currentVelocityValues"] = Vector3ToArr(velocity);
        s.Globals["currentOrientationValues"] = Vector3ToArr(orientation);
        s.Globals["currentPositionValues"] = Vector3ToArr(position);
        s.Globals["currentAccelerationValue"] = acceleration;
        try
        {
            DynValue res = s.DoString(script);
            if (res.Tuple != null)
                for (int i = 0; i < res.Tuple.Length; i++)
                {
                    vals[i] = (float)res.Tuple[i].Number;
                }   
        } catch (ScriptRuntimeException ex)
        {
    
        }
        return vals;
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

    public Vector3 PositionValue(string script, Vector3 vals)
    {
        Script s = new Script();
        float[] arr = new float[3];
        s.Globals["currentPositionValues"] = Vector3ToArr(vals);
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

    public float AccelerationValue(string script, float val)
    {
        Script s = new Script();
        float ret;
        s.Globals["accelerationValue"] = val;
        try
        {
            DynValue res = s.DoString(script);
            return (float)res.Number;
        }
        catch (ScriptRuntimeException ex)
        {
            Debug.Log(ex);
            return 0f;
        }
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
