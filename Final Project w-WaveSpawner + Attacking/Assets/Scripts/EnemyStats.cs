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

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        enemyHitSound = sounds[1];
        enemyDeathSound = sounds[2];

    }
    public void DamageToEnemy(float dmg)
    {
        enemyHealth -= dmg;
        enemyHitSound.Play();
        Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 2);
        if(enemyHealth <= 0)
        {
            enemyDeathSound.Play();
            Destroy(gameObject);
        }
    }
}
