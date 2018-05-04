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
	private List<Transform> spawnPoints;

    public int WaveSizeIncrease;
    public float SecsBetweenWaves;
    public int startingHealth;
    public int healthIncreasePerWave;
    public int damageTakenPerHit;
	private AudioSource deathSound;


	public GameObject spawnPointsContainer;
    private int WaveSize;
    private EnemyUnit CurentEnemy;
    private int TotalCount = 0;
    private float time = 0f;
    private int currentHealth = 0;
	public GameObject player;

    private List<EnemyUnit> EnemyClassList = new List<EnemyUnit>();

    // Use this for initialization
    void Start()
    {

		spawnPoints = new List<Transform>(spawnPointsContainer.GetComponentsInChildren<Transform> ());
		spawnPoints.RemoveAt (0);

		deathSound = this.GetComponent<AudioSource> ();

        currentHealth = startingHealth;
        WaveSize = 1;
        //EnemyClassList = new List<EnemyUnit>();
		int randomPick = Random.Range(0, spawnPoints.Count - 1);
        TotalCount = TotalCount + 1;

        CurentEnemy = new EnemyUnit();
        CurentEnemy.enemyInstance = Instantiate(myPrefab, spawnPoints[randomPick].position, spawnPoints[randomPick].rotation) as GameObject;
        CurentEnemy.enemyInstance.SetActive(true);
        CurentEnemy.tagName = "Enemy" + TotalCount;
        CurentEnemy.enemyInstance.name = CurentEnemy.tagName;
        CurentEnemy.currentHealth = currentHealth;
        CurentEnemy.maxHealth = currentHealth;
        CurentEnemy.active = true;

		CurentEnemy.enemyInstance.transform.position = spawnPoints [randomPick].position;
		Debug.Log (CurentEnemy.enemyInstance.transform.position - spawnPoints [randomPick].position);
        EnemyClassList.Add(CurentEnemy);

		Vector2 direction = player.transform.position - CurentEnemy.enemyInstance.transform.position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		CurentEnemy.enemyInstance.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

        if (time > SecsBetweenWaves)
        {
            time = 0f;
            spawnWave();
        }
    }

    public void hitMe(GameObject test)
    {

        for (int i = 0; i < EnemyClassList.Count; i++)
        {
            if (EnemyClassList[i].enemyInstance.name == test.name)
            {
                EnemyClassList[i].currentHealth -= damageTakenPerHit;
                if (EnemyClassList[i].currentHealth <= 0)
                {


					deathSound.PlayOneShot (deathSound.clip);
					Debug.Log ("death");
                    EnemyClassList[i].active = false;
                    EnemyClassList[i].enemyInstance.SetActive(false);
                }
            }
        }


    }

    void spawnWave()
    {
        int EnemiesLeftAlive = 0;
        int EnemiesToRespawn = 0;
        bool reuse = false;

        currentHealth += healthIncreasePerWave;
        WaveSize += WaveSizeIncrease;


        for (int i = 0; i < EnemyClassList.Count; i++)
        {
            if (EnemyClassList[i].active == true)
            {
                EnemiesLeftAlive += 1;
            }
        }

        EnemiesToRespawn = WaveSize - EnemiesLeftAlive;

        // create more enemies to match wave size
        for (int i = 0; i < EnemiesToRespawn; i++)
        {
            TotalCount = TotalCount + 1;
            reuse = false;

            // if an inactive pooled exists

            for (int y = 0; y < EnemyClassList.Count; y++)
            {
                if (EnemyClassList[y].active == false)
                {
					

					int randomPick = Random.Range(0, spawnPoints.Count - 1);
                    // reuse from pool
                    Destroy(EnemyClassList[y].enemyInstance);
                    EnemyClassList[y].enemyInstance = Instantiate(myPrefab, spawnPoints[randomPick].position, spawnPoints[randomPick].rotation) as GameObject;
                    EnemyClassList[y].enemyInstance.SetActive(true);
                    EnemyClassList[y].tagName = "Enemy" + TotalCount;
                    EnemyClassList[y].enemyInstance.name = "Enemy" + TotalCount;
                    EnemyClassList[y].currentHealth = currentHealth;
                    EnemyClassList[y].maxHealth = currentHealth;
                    EnemyClassList[y].active = true;
					EnemyClassList[y].enemyInstance.transform.position = spawnPoints [randomPick].position;


					Debug.Log (EnemyClassList[y].enemyInstance.transform.position - spawnPoints [randomPick].position);

					// rotate the zombie to the player
					Vector2 direction = player.transform.position - EnemyClassList[y].enemyInstance.transform.position;
					float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
					Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
					EnemyClassList[y].enemyInstance.transform.rotation = rotation;

                    reuse = true;
                    break;
                }
            }

            if (reuse == false)
            {
				int randomPick = Random.Range(0, spawnPoints.Count - 1);

                CurentEnemy = new EnemyUnit();

                CurentEnemy.enemyInstance = Instantiate(myPrefab, spawnPoints[randomPick].position, spawnPoints[randomPick].rotation) as GameObject;
                CurentEnemy.enemyInstance.SetActive(true);
                CurentEnemy.tagName = "Enemy" + TotalCount;
                CurentEnemy.enemyInstance.name = "Enemy" + TotalCount;
                CurentEnemy.currentHealth = currentHealth;
                CurentEnemy.maxHealth = currentHealth;
                CurentEnemy.active = true;

				CurentEnemy.enemyInstance.transform.position = spawnPoints [randomPick].position;
				Debug.Log (CurentEnemy.enemyInstance.transform.position - spawnPoints [randomPick].position);
                
				EnemyClassList.Add(CurentEnemy);

				// rotate the zombie to the player
				Vector2 direction = player.transform.position - CurentEnemy.enemyInstance.transform.position;
				float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
				CurentEnemy.enemyInstance.transform.rotation = rotation;
            }

        }
    }
}