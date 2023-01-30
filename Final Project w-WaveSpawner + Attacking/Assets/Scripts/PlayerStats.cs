using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxhealthValue = 20;
    public float currentHealth;
    public float maxattackValue = 5;
    public float playerCoins;
    private float waveValueMultiplier;

    private WaveSpawner WaveSpawnerScript;

    public HealthBar healthBar;

    public float healthdamage = 2;

    public Game restartReference;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealthValue;
        WaveSpawnerScript = GameObject.Find("SpawnManager").GetComponent<WaveSpawner>();
        healthBar.SetMaxHealth(maxhealthValue);
    }

    // Update is called once per frame
    void Update()
    {
        waveValueMultiplier = WaveSpawnerScript.nextWave + 1;
    }
    

    // For item pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            playerCoins += waveValueMultiplier;
            Debug.Log("Coins: " + playerCoins);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("AttackBuff"))
        {
            maxattackValue = Mathf.Round(maxattackValue + (waveValueMultiplier * 1.5f));
            Debug.Log("Attack Value:" + maxattackValue);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("HealthBuff"))
        {
            maxhealthValue = Mathf.Round(maxhealthValue + (waveValueMultiplier * 5f));
            Debug.Log("Health Value:" + maxhealthValue);
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            restartReference.GameOver();
        }
    }
}
