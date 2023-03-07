using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Delete object after x seconds
// Will be replaced with delete after animation for slash object
public class Delete : MonoBehaviour
{
    public float timeUntilDestroy = 1f;

    void Start()
    {
        StartCoroutine(Deletion());
    }

    private IEnumerator Deletion()
    {
        yield return new WaitForSeconds(timeUntilDestroy);
        Destroy(gameObject);
    }
}
