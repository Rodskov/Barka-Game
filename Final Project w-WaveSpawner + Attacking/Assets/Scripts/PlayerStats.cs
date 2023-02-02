using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxhealthValue = 20;
    public float maxattackValue = 5;
    public float playerCoins;
    private float waveValueMultiplier;

    private WaveSpawner WaveSpawnerScript;
    public HealthBar healthBar;
    private Game restartReference;

    public AudioSource healthSound;
    public AudioSource attackBuffSound;
    public AudioSource coinSound;


    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        waveValueMultiplier = WaveSpawnerScript.nextWave + 1;
    }
    
    public void OnStart()
    {
        WaveSpawnerScript = GameObject.Find("SpawnManager").GetComponent<WaveSpawner>();
        healthBar.SetMaxHealth(maxhealthValue);
    }

    // For item pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            playerCoins += waveValueMultiplier;
            Debug.Log("Coins: " + playerCoins);
            coinSound.Play();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("AttackBuff"))
        {
            maxattackValue = Mathf.Round(maxattackValue + (waveValueMultiplier * 1.5f));
            Debug.Log("Attack Value:" + maxattackValue);
            attackBuffSound.Play();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("HealthBuff"))
        {
            maxhealthValue = Mathf.Round(maxhealthValue + (waveValueMultiplier * 5f));
            Debug.Log("Health Value:" + maxhealthValue);
            healthSound.Play();
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(float enemyDmg)
    {
        maxhealthValue -= enemyDmg;

        healthBar.SetHealth(maxhealthValue);

        if (maxhealthValue <= 0)
        {
            restartReference.GameOver();
        }
    }
}
