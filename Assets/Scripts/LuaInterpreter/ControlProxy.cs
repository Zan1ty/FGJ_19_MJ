using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class ScriptReader
{
    public List<float> ForcesValues(string script, float[] forces)
    {
        List<float> vals = new List<float>();
        Script s = new Script();
        s.Globals["numbers"] = forces;
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
}
