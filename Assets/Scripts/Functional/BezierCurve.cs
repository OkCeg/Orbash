using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Draw a cubic Bezier curve
// See Zotov's video for more info: https://www.youtube.com/watch?v=11ofnLOE8pw
public class BezierCurve : MonoBehaviour
{
    [SerializeField] private Transform[] bezierPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * bezierPoints[0].position;
        }
    }
}
