using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };


    [System.Serializable] // Allows us to modify the values inside this function.
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }
    public Text finishedWaveText;
    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;

    public float waveInterval = 5f;
    public float waveCountDown;

    private float searchCountDown = 1f;


    public SpawnState state = SpawnState.COUNTING;
    public bool wave = false;

    public Game shopReference;
    public GameObject healthBar;
    public TextMeshProUGUI waveText;
    public GameObject canvas;


    void Start()
    {
        finishedWaveText.gameObject.SetActive(false);
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountDown = waveInterval;

        shopReference = GameObject.Find("_Game").GetComponent<Game>();

        
    }


    void Update()
    {
        if (state == SpawnState.WAITING)
        {

            if (!EnemyStillAlive())
            {
               
                WaveCompleted();
                shopReference.OpenShop();
                shopReference.UnlockCursor();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                canvas.gameObject.SetActive(false);
                finishedWaveText.gameObject.SetActive(false);
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }


    void WaveCompleted()
    {
        finishedWaveText.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountDown = waveInterval;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
            finishedWaveText.gameObject.SetActive(true);
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            finishedWaveText.gameObject.SetActive(false);
            nextWave++;
        }


    }


    bool EnemyStillAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }



    // Allows us to wait before starting a new wave
    IEnumerator SpawnWave(Wave _wave)
    {
        waveText.text = "" + _wave.name;
        Debug.Log("Spawning " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
            shopReference.CloseShop();
            shopReference.LockCursor();
        }



        state = SpawnState.WAITING;
        // Wait for player to kill all..

        yield break;
    }



    public void SpawnEnemy(Transform[] _enemy)
    {
        // Spawn Enemy Here
        int randomIndex = Random.Range(0, _enemy.Count());
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy[randomIndex], _sp.position, _sp.rotation);
    }
}
