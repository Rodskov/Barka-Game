using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public AudioSource burnSound;
    public float time = 2f;
    public float damage = 1f;

    // If the player falls unto the lava, the player will take damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            PlayerStats currentHealth = other.GetComponent<PlayerStats>();
            StartCoroutine(TakeDamage(time, currentHealth));
        }
    }
    // The damage is set into intervals
    IEnumerator TakeDamage(float time, PlayerStats currentHealth)
    {
        burnSound.Play();
        currentHealth.TakeDamage(damage);
        GetComponent<BoxCollider>().isTrigger = false;
        yield return new WaitForSeconds(time);
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
