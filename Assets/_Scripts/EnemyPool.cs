using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit
{
    public string tagName;
    public int currentHealth;
    public int maxHealth;
    public bool active;
    public GameObject enemyInstance;
}


public class EnemyPool : MonoBehaviour
{
    public GameObject myPrefab;
    public Transform spawnPoints;

    public int WaveSizeIncrease;
    public float SecsBetweenWaves;
    public int startingHealth;
    public int healthIncreasePerWave;
    public int damageTakenPerHit;

    private int WaveSize;
    private EnemyUnit CurentEnemy;
    private int TotalCount = 0;
    private float time = 0f;
    private int currentHealth = 0;

    List<EnemyUnit> EnemyClassList;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        WaveSize = 1;

        EnemyClassList = new List<EnemyUnit>();

        TotalCount = TotalCount + 1;

        CurentEnemy = new EnemyUnit();
        CurentEnemy.enemyInstance = Instantiate(myPrefab, spawnPoints.position, spawnPoints.rotation) as GameObject;
        CurentEnemy.tagName = "Enemy" + TotalCount;
        CurentEnemy.currentHealth = currentHealth;
        CurentEnemy.maxHealth = currentHealth;
        CurentEnemy.active = true;

        EnemyClassList.Add(CurentEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

        if (time > SecsBetweenWaves)
        {
            time = 0f;
            WaveSize += WaveSizeIncrease;
            spawnWave();
        }
    }

    public void hitMe(GameObject test)
    {
        foreach (EnemyUnit x in EnemyClassList)
        {
            if (x.enemyInstance == test)
            {
                x.currentHealth -= damageTakenPerHit;
                if (x.currentHealth <= 0)
                {
                    //x.active = false;
                    x.enemyInstance.SetActive(false);
                }
            }
        }

    }

    void spawnWave()
    {
        int count = 0;
        int EnemiesDead = 0;
        bool reuse = false;

        currentHealth += healthIncreasePerWave;

        // how many active from last wave left
        foreach (EnemyUnit x in EnemyClassList)
        {
            if (x.active == true)
            {
                count += 1;
            }
        }
        EnemiesDead = WaveSize - count;

        // create more enemies to match wave size
        for (int i = 0; i < EnemiesDead; i++)
        {
            TotalCount = TotalCount + 1;
            reuse = false;

            // if an inactive pooled exists
            foreach (EnemyUnit y in EnemyClassList)
            {
                if (y.active == false)
                {
                    CurentEnemy = y;  // grab from pool
                    reuse = true;
                }
            }


            CurentEnemy = new EnemyUnit();
            CurentEnemy.enemyInstance = Instantiate(myPrefab, spawnPoints.position, spawnPoints.rotation) as GameObject;
            CurentEnemy.tagName = "Enemy" + TotalCount;

            CurentEnemy.currentHealth = currentHealth;
            CurentEnemy.maxHealth = currentHealth;
            CurentEnemy.active = true;

            if (reuse == false)
            {
                EnemyClassList.Add(CurentEnemy);
            }



        }
    }
}