using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildren : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
