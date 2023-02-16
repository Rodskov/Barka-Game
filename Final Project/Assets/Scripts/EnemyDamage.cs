using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int TakeDamage;

    // On contact with the player the enemy will damage the player
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStats>().TakeDamage(TakeDamage);
        }
    }
}
