using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKill : MonoBehaviour
{
    public float delay = 0f;

    public GameObject coin;
    public GameObject attackBuff;
    public GameObject healthBuff;

    public PlayerStats playerDamage;
    private float DamageToEnemy;

    // Initializes the player's damage
    void Start()
    {
        playerDamage = GameObject.Find("Player").GetComponent<PlayerStats>();
        DamageToEnemy = playerDamage.maxattackValue;
    }
    // If the attack hits the enemy, damage is dealt
    // If the enemy dies, pickups are dropped where the enemies died
    // The pickups dropped are based on a random number generated
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().DamageToEnemy(DamageToEnemy);

            int chanceNum = Random.Range(1, 12);
            if (chanceNum >= 1 && chanceNum <= 6)
            {
                Vector3 coinSpawnPos = new Vector3(other.transform.position.x, 50, other.transform.position.z);
                Instantiate(coin, coinSpawnPos, Quaternion.identity);
            }
            else if (chanceNum == 7 || chanceNum == 8)
            {
                Vector3 attackBuffSpawnPos = new Vector3(other.transform.position.x, 50, other.transform.position.z);
                Instantiate(attackBuff, attackBuffSpawnPos, Quaternion.identity);
            }
            else if(chanceNum == 9 || chanceNum == 10)
            {
                Vector3 healthBuffSpawnPos = new Vector3(other.transform.position.x, 50, other.transform.position.z);
                Instantiate(healthBuff, healthBuffSpawnPos, Quaternion.identity);
            }
        }
    }


}
