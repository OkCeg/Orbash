using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Delete object after x seconds
// Will be replaced with delete after animation for slash object
public class Deactivate : MonoBehaviour
{
    public float timeUntilDeactivation = 1f;

    public IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(timeUntilDeactivation);
        gameObject.SetActive(false);
    }
}
