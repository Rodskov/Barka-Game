using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using TMPro;


public class PlayerStats : MonoBehaviour
{
    public float maxhealthValue = 20;
    public float maxattackValue = 5;
    public float playerCoins;
    public TextMeshProUGUI coinText;


    private float waveValueMultiplier;

    private WaveSpawner WaveSpawnerScript;
    public HealthBar healthBar;
    private GameManager GameManager;
    public bool gameOver = false;

    public AudioSource healthSound;
    public AudioSource attackBuffSound;
    public AudioSource coinSound;
    public AudioSource deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        coinText.gameObject.SetActive(true);
    }
   
    // Update is called once per frame
    void Update()
    {
        coinText.text = "" + playerCoins;
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
            AddCoins();
            coinSound.Play();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("AttackBuff"))
        {
            AttackBoost();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("HealthBuff"))
        {
            HealthBoost();
            Destroy(other.gameObject);
        }
    }
    // This calculates the amount of damage the player receives 
    public void TakeDamage(float enemyDmg)
    {
        maxhealthValue -= enemyDmg;

        healthBar.SetHealth(maxhealthValue);

        if (maxhealthValue <= 0)
        {
            deathSound.Play();
            gameOver = true;
            GameManager.gameOver();
            Debug.Log("Game Over");
            coinText.gameObject.SetActive(false);
        }
    }
    // The player receives coins from the pickups and the value is based on the current wave
    public void AddCoins()
    {
        playerCoins += waveValueMultiplier;
    }
    // The player receives attack boost from the pickups and the value is based on the current wave
    public void AttackBoost()
    {
        maxattackValue = Mathf.Round(maxattackValue + (waveValueMultiplier * 1.5f));
        Debug.Log("Attack Value:" + maxattackValue);
        attackBuffSound.Play();
    }
    // The player receives health boost from the pickups and the value is based on the current wave
    public void HealthBoost()
    {
        maxhealthValue = Mathf.Round(maxhealthValue + (waveValueMultiplier * 5f));
        Debug.Log("Health Value:" + maxhealthValue);
        healthSound.Play();
    }
    
}
