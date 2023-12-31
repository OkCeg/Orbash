using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boss's slash attack that fires a projectile going from the boss's position to the opposite corner
public class BossSlash : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private int attackCount; // How many slashes the boss will do
    [SerializeField] private float waitTime; // The wait time between slashes
    [SerializeField] private float distanceFromCorner; // To determine the position for boss teleport

    private int previousQuadrant = 0; // The quadrant where the boss attacked last
    private int currentQuadrant = 0; // The current quadrant to attack in
    private Vector2[] corners; // The array of the corner positions of the arena
    private Vector2[] vectorsFromCorner; // The array of how far the boss should be from each corner
    private Quaternion[] rotations; // The array of how much to rotate each projectile

    // Cached WaitForSeconds
    private WaitForSeconds cachedWait;

    private void Awake()
    {
        corners = ClosestQuadrant.Instance.corners;
        vectorsFromCorner = new Vector2[] {
            new Vector2(-distanceFromCorner, -distanceFromCorner),
            new Vector2(distanceFromCorner, -distanceFromCorner),
            new Vector2(distanceFromCorner, distanceFromCorner),
            new Vector2(-distanceFromCorner, distanceFromCorner),
        };
        rotations = new Quaternion[] {
            Quaternion.Euler(0, 0, Mathf.Atan2(-20, -15) * Mathf.Rad2Deg - 90),
            Quaternion.Euler(0, 0, Mathf.Atan2(-20, 15) * Mathf.Rad2Deg - 90),
            Quaternion.Euler(0, 0, Mathf.Atan2(20, 15) * Mathf.Rad2Deg - 90),
            Quaternion.Euler(0, 0, Mathf.Atan2(20, -15) * Mathf.Rad2Deg - 90)
        };
    }

    public IEnumerator SlashAttack()
    {
        // Here instead of in Awake() to account for when waitTime changes
        cachedWait = new WaitForSeconds(waitTime);

        for (int i = 0; i < attackCount; i++)
        {
            // Set the currentQuadrant to an available quadrant
            FindValidQuadrant();

            // Teleport to the attacking quadrant
            transform.position = corners[currentQuadrant - 1] + vectorsFromCorner[currentQuadrant - 1];

            // Rotate and apply velocity to the projectile
            // Note: currentQuadrant+1 is necessary for the velocity to find the opposite
            //       quadrant while taking into account for the array index starting from 0
            ObjectPool.SharedInstance.CreateBossSlash(transform.position, rotations[currentQuadrant - 1], corners[(currentQuadrant + 1) % 4]);

            yield return cachedWait;
        }

        BossAttackPattern.NextAttackReady = true;
    }

    // Find the quadrant to attack in
    // Updates the value of currentQuadrant
    private void FindValidQuadrant()
    {
        // Set the previous quadrant
        // currentQuadrant is updated in this method
        previousQuadrant = currentQuadrant;

        // Find the quadrant closest to the player
        int closestQuadrantNum = ClosestQuadrant.Instance.CalculateClosestQuadrant();

        // The boss cannot attack in quadrant closest to the player.
        // If the boss did not attack previously or the location is unavailable:
        // 40% opposite quadrant, 30% for each of the other two quadrants
        // If the boss attacked previously and that location is available:
        // 20% same quadrant, 40% for each of the other two quadrants
        float randomizer = Random.Range(0, 1f);
        if (previousQuadrant == 0 || previousQuadrant == closestQuadrantNum)
        {
            if (randomizer < 0.4f)
            {
                // Opposite quadrant from player
                currentQuadrant = (closestQuadrantNum + 2) % 4;
            }
            else if (randomizer < 0.7f)
            {
                // Other1 quadrant
                currentQuadrant = (closestQuadrantNum + 3) % 4;
            }
            else
            {
                // Other2 quadrant
                currentQuadrant = (closestQuadrantNum + 1) % 4;
            }
        }
        else
        {
            if (randomizer < 0.2f)
            {
                // Same quadrant as previous attack
                currentQuadrant = previousQuadrant;
            }
            else if (randomizer < 0.6f)
            {
                // Other1 quadrant
                currentQuadrant = (previousQuadrant + 1) % 4;

                // mod 4 is necessary here because quadrant 4 is being represented as 0 for currentQuadrant
                if (currentQuadrant == closestQuadrantNum % 4) 
                {
                    currentQuadrant = (previousQuadrant + 2) % 4;
                }
            }
            else
            {
                // Other2 quadrant
                currentQuadrant = (previousQuadrant + 3) % 4;

                // mod 4 is necessary here because quadrant 4 is being represented as 0 for currentQuadrant
                if (currentQuadrant == closestQuadrantNum % 4)
                {
                    currentQuadrant = (previousQuadrant + 2) % 4;
                }
            }
        }

        // To correct mod 4 making quadrant 4 into 0
        if (currentQuadrant == 0)
        {
            currentQuadrant = 4;
        }
    }
}
