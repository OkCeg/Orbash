using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test whether re-enabling object/script triggers Start()
// Test results: only called once in lifetime of script, can never be called again
public class EnableTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("hi!");
    }
}
