using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one WaveSpaner on scene!.");
            return;
        }
        instance = this;
    }
    [SerializeField] public static int EnemiesAlive = 0;

    [SerializeField] private Wave[] waves;
  
    public List<GameObject> incomeWaves;
  
    private GameManager gameManager;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Text waveCountDownText;

    [SerializeField] private Text enemiesAliveCountText;

    [SerializeField] private float timeBetweenWaves = 1f;
    [SerializeField] private float countdown;
    [SerializeField] private float initialCountdown = 55f;

    private int waveIndex = 0;
    public bool levelEnded;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive = 0;
        countdown = initialCountdown;
        levelEnded = true;
    }
    public int  getEnemiesAlive()
    {
        return EnemiesAlive;
    }

    public void setEnemiesAlive(int enemies)
    {
        EnemiesAlive = enemies;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesAliveCountText.text = EnemiesAlive.ToString();

        if (EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            //TODO controlar que no se intente acceder al siguiente del array
            gameManager.WinLevel();
            this.enabled = false;
        }
        if (levelEnded)
        {
            levelEnded = false;
            PlayerStats.Money += PlayerStats.Income;
            Debug.Log("Level ended, income added to your money. (+ $" + PlayerStats.Income + ")");
            //need to reestart all the actives turrets
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            SpawnIncomeOnWave();
            countdown = timeBetweenWaves;
            levelEnded = true;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countdown);

        
    }

    IEnumerator SpawnWave()
    {
        //Debug.Log("Wave " + waveIndex  + " incoming!");
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

       // EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(5, 11);
        float y = 0.5f;
        float z = Random.Range(-8, -7);
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
        return pos;
    }

    Vector3 RandomPositionOnPitch()
    {
        float x = Random.Range(20.5f, 25);
        float y = 0.5f;
        float z = Random.Range(-3.5f, -8.5f);
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
        return pos;
    }

    void SpawnEnemy(GameObject enemy)
    {
        EnemiesAlive++;
        GameObject enemyToSpawn = (GameObject)Instantiate(enemy, RandomPosition(), spawnPoint.rotation);
        enemyToSpawn.GetComponent<UnitMovement>().setCanMove(true);
    }

    public void SpawnEnemyIncomePitch(GameObject enemy)
    {
        
        Debug.Log("spwawning from income..."+ enemy);
        GameObject enemyToSpawn = (GameObject)Instantiate(enemy, RandomPositionOnPitch(), spawnPoint.rotation);
        enemyToSpawn.GetComponent<UnitMovement>().setCanMove(false);
        incomeWaves.Add(enemyToSpawn);
    }


    /// <summary>
    /// this metod is adding the income new units to the new round.
    /// It instantiate a new obj from the tipe of the list and then destroy the original one.
    /// For last it clears the list of new incomes.
    /// </summary>
    public void SpawnIncomeOnWave()
    {
        foreach (GameObject enemy in incomeWaves)
        {
            SpawnEnemy(enemy);
            Destroy(enemy);
        }

        incomeWaves.Clear();
    }
}
