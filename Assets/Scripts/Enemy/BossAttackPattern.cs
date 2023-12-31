using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control how the boss will make its next attack
public class BossAttackPattern : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private float timeBetweenAttacks;

    // Controlled by all attack scripts
    // Disabled while an attack is going on and re-enabled once an attack finishes
    public static bool NextAttackReady;

    // Store the previous two attacks the boss used
    private string previousAttack;
    private string previousPreviousAttack;

    // The list of possible boss attacks
    private string[] attackList;
    private BossSlash bossSlash;
    private BossArrowAttack bossArrow;
    private BossParticleExplosion bossParticleExplosion;

    // For choosing an attack
    private int numberOfPossibleAttacks; // The number of possible attacks
    private float rerollProbability; // For selecting the probability of rerolling previous attacks
    private string selectedAttack; // The chosen attack

    // Cached wait time between attacks
    private WaitForSeconds waitUntilAttack;

    private void Start()
    {
        NextAttackReady = true;

        bossSlash = GetComponent<BossSlash>();
        bossArrow = GetComponent<BossArrowAttack>();
        bossParticleExplosion = GetComponent<BossParticleExplosion>();
        attackList = new string[]
        {
            "BossSlash",
            "BossArrow",
            "BossParticleExplosion"
        };

        numberOfPossibleAttacks = attackList.Length;
    }

    private void FixedUpdate()
    {
        if (NextAttackReady)
        {
            StartCoroutine(ChooseAttack());
            NextAttackReady = false;
        }
    }

    // Choose an attack
    // If the attack was performed most recently: accept with 30% chance, otherwise reroll
    // If the attack was performed second-most recently: accept with 60% chance, otherwise reroll
    private IEnumerator ChooseAttack()
    {
        yield return waitUntilAttack;

        for (int i = 0; i < 100; i++)
        {
            rerollProbability = Random.Range(0, 1f);
            selectedAttack = attackList[Random.Range(0, numberOfPossibleAttacks)];

            // Continue if it is a new attack
            if (selectedAttack != previousAttack && selectedAttack != previousPreviousAttack)
            {
                break;
            }

            // Continue 30% of the time if previous attack
            // Reroll otherwise
            if (selectedAttack == previousAttack && rerollProbability < 0.3f)
            {
                break;
            }

            // Continue 60% of the time if previous previous attack
            // Reroll otherwise
            if (selectedAttack == previousPreviousAttack && rerollProbability < 0.6f)
            {
                break;
            }
        }

        switch (selectedAttack)
        {
            case "BossSlash":
                StartCoroutine(bossSlash.SlashAttack());
                break;
            case "BossArrow":
                StartCoroutine(bossArrow.ArrowAttack());
                break;
            case "BossParticleExplosion":
                StartCoroutine(bossParticleExplosion.ExplosionAttack());
                break;
            default:
                Debug.Log("Invalid attack!");
                break;
        }

        previousPreviousAttack = previousAttack;
        previousAttack = selectedAttack;
    }
}
