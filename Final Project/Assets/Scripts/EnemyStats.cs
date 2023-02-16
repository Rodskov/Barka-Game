using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHealth;
    public float enemyAttackValue;
    private AudioSource[] sounds;
    private AudioSource enemyHitSound;
    private AudioSource enemyDeathSound;
    public ParticleSystem bloodParticles;
    public ParticleSystem deathParticles;

    // Sound effects are initialized
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        enemyHitSound = sounds[1];
        enemyDeathSound = sounds[2];

    }
    // When the player attacks the enemy, the enemy will be damaged
    public void DamageToEnemy(float dmg)
    {
        enemyHealth -= dmg;
        enemyHitSound.Play();
        Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 2);
        if(enemyHealth <= 0)

        // If the enemy's health reaches 0, the enemy game object is destroyed
        {
            enemyDeathSound.Play();
            Destroy(gameObject);
        }
    }
}
