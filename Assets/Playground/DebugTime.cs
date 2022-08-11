using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTime : MonoBehaviour
{
    public void Say()
    {
        Debug.Log(Time.time);
    }
}
