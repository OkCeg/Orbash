using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Delete object after x seconds
// Will be replaced with delete after animation for slash object
public class Deactivate : MonoBehaviour
{
    public float timeUntilDeactivation = 1f;
    private WaitForSeconds yieldInterval;

    private void Start()
    {
        yieldInterval = new WaitForSeconds(timeUntilDeactivation);
    }

    public IEnumerator Deactivation()
    {
        yield return yieldInterval;
        gameObject.SetActive(false);
    }
}
