using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script that lets the boss use the ParticleExplosion attack
public class BossParticleExplosion : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private int attackNum;
    [SerializeField] private int explosionBulletCount;
    [SerializeField] private float explosionInitialAngle;
    [SerializeField] private float timeUntilNextExplosion;

    private GameObject player;
    private Vector2 currentPos;
    private Vector2 playerPos;
    private WaitForSeconds waitUntilNextExplosion; // Cached WaitForSeconds
    private Vector2 positionToMoveTo; // Set where the boss should move to
    private bool moveOn = false; // Whether the boss should move

    private Vector2 smoothVelocity = Vector2.zero; // For smooth damping

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waitUntilNextExplosion = new WaitForSeconds(timeUntilNextExplosion);
    }

    public IEnumerator ExplosionAttack()
    {
        for (int i = 0; i < attackNum; i++)
        {
            currentPos = transform.position;
            playerPos = player.transform.position;

            FindWhereToMove();
            moveOn = true;

            yield return waitUntilNextExplosion;
            ObjectPool.SharedInstance.CreateParticleExplosion(transform.position, explosionBulletCount, explosionInitialAngle);
        }

        moveOn = false;
        BossAttackPattern.NextAttackReady = true;
    }

    // Must be at least a certain distance away from the player and where the explosion was previously fired
    private void FindWhereToMove()
    {
        bool correct = true;
        for (int i = 0; i < 100; i++)
        {
            positionToMoveTo = new Vector2(Random.Range(-12, 12f), Random.Range(-17, 17f));
            // Check if the new position is far enough away from the old position
            if ((currentPos - positionToMoveTo).sqrMagnitude > 15 * 15)
            {
                // Check if the new position is far enough away from the player position
                if ((playerPos - positionToMoveTo).sqrMagnitude > 15 * 15)
                {
                    correct = false;
                    break;
                }
            }
        }
        Debug.Log(correct);
        Debug.Log("reg: " + (currentPos - positionToMoveTo).magnitude);
        Debug.Log("player: " + (playerPos - positionToMoveTo).magnitude);
    }

    private void Update()
    {
        if (moveOn)
        {
            // Dividing the time by 2 is necessary to ensure that the boss makes it to the new position in time
            // Otherwise, the boss might create a ParticleExplosion and be too close to the previous position or the player
            transform.position = Vector2.SmoothDamp(transform.position, positionToMoveTo, ref smoothVelocity, timeUntilNextExplosion / 2);
        }
    }
}
