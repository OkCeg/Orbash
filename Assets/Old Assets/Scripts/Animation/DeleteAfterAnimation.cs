using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterAnimation : MonoBehaviour
{
    public void End(string message)
    {
        if (message.Equals("Animation Ended"))
        {
            Destroy(gameObject);
        }
    }
}
