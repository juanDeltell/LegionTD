using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public GameManager gameManager;
    public Transform spawnPoint;

    public Text waveCountDownText;

    public Text enemiesAliveCountText;

    public float timeBetweenWaves = 1f;
    private float countdown;
    [SerializeField] private float initialCountdown = 55f;

    private int waveIndex = 0;
    private bool levelEnded;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive = 0;
        countdown = initialCountdown;
        levelEnded = false;
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
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
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

        EnemiesAlive = wave.count;

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
        float x = Random.Range(5, 11);
        float y = 0.5f;
        float z = Random.Range(-8, -7);
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
        return pos;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, RandomPosition(), spawnPoint.rotation);
    }

    public void SpawnEnemyIncomePitch(GameObject enemy)
    {
        Instantiate(enemy, RandomPositionOnPitch(), spawnPoint.rotation);
    }
}
