using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count = 5;
        public float rate = 2f;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1f;

	// Use this for initialization
	void Start ()
    {
        waveCountdown = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //StartNewCountdown();
                Debug.Log("Wave Completed");
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0f)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
	}

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown >= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // spawn
        Instantiate(_enemy, transform.position, transform.rotation);
        Debug.Log("Spawning Enemy");
    }


}
