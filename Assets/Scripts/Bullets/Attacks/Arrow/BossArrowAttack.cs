using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boss using the arrow attack
public class BossArrowAttack : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private float attackNum; // To determine the position for boss teleport
    [SerializeField] private float distanceFromCorner; // To determine the position for boss teleport
    [SerializeField] private float timeUntilFire; // The wait time until the arrow fires
    [SerializeField] private float timeUntilNextAttackPrep; // The time between attacks

    private int currentQuadrant = 0; // The current quadrant to attack in
    private Vector2[] corners; // The array of the corner positions of the arena
    private Vector2[] vectorsFromCorner; // The array of how far the boss should be from each corner

    // Cached wait
    private WaitForSeconds waitUntilNextAttack;

    private void Awake()
    {
        corners = ClosestQuadrant.Instance.corners;
        vectorsFromCorner = new Vector2[] {
            new Vector2(-distanceFromCorner, -distanceFromCorner),
            new Vector2(distanceFromCorner, -distanceFromCorner),
            new Vector2(distanceFromCorner, distanceFromCorner),
            new Vector2(-distanceFromCorner, distanceFromCorner),
        };

        waitUntilNextAttack = new WaitForSeconds(timeUntilNextAttackPrep + timeUntilFire);
    }

    public IEnumerator ArrowAttack()
    {
        for (int i = 0; i < attackNum; i++)
        {
            // Set the currentQuadrant to an available quadrant
            FindValidQuadrant();

            // Teleport to the attacking quadrant
            transform.position = corners[currentQuadrant - 1] + vectorsFromCorner[currentQuadrant - 1];

            // Create the object
            ObjectPool.SharedInstance.CreateArrow(transform.position, timeUntilFire);

            yield return waitUntilNextAttack;
        }

        BossAttackPattern.NextAttackReady = true;
    }

    // Find the quadrant to attack in
    // Updates the value of currentQuadrant
    private void FindValidQuadrant()
    {
        // Find the quadrant closest to the player
        int closestQuadrantNum = ClosestQuadrant.Instance.CalculateClosestQuadrant();

        // The boss cannot attack in quadrant closest to the player:
        // 40% opposite quadrant, 30% for each of the other two quadrants
        float randomizer = Random.Range(0, 1f);
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

        // To correct mod 4 making quadrant 4 into 0
        if (currentQuadrant == 0)
        {
            currentQuadrant = 4;
        }
    }
}
