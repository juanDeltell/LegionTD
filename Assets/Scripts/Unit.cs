using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    [Header("Unit")]
    [SerializeField] private float startSpeed = 3f;
    [SerializeField] public float speed;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private float vision = 3f;
    [SerializeField] public string TagEnemiesName = "Ally";
    [SerializeField] private Image healthBar;

    [Header("Unit: shoot")]
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float turnSpeed = 5f;
    [SerializeField] public Transform firePoint;
    [SerializeField] public Transform partToRotate;
    [SerializeField] private float damage = 20;
    [SerializeField] public float fireRate = 1f;
    [SerializeField] public float fireCountDown = 0f;
    [SerializeField] public float range = 1f;

    [Header("Unit: enemy")]

    [SerializeField] private bool isDead = false;
    [SerializeField] public int reward = 50;
    [SerializeField] private GameObject enemyDeathEffect;

    [SerializeField] private ProfitsUI profitsUIEnemy;

    GameManager gameManager;

    [Header("Tower: Laser")]
    [SerializeField] private float slowAmount = .5f;
    [SerializeField] private bool useLaser = false;
    [SerializeField] private int damageOverTime = 30;//damage per second
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Light impactLight;
    
    

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        
    }

    public void RayCastOnClick()
    {
        Debug.Log("click on Enemy.");
    }
    public void TakeDamage(float amount)
    {
        if (transform.name == "King")
        {
            PlayerStats.KingLife -= amount;
        }
        /*
         if (PlayerStats.KingLife <= 0)
        {
            GameOver();
        }
        */
        health -= amount;

        if (health / startHealth >= 0.4)
        {
            healthBar.GetComponent<Image>().color = Color.green;
        }
        else if(health / startHealth >= 0.25 && health / startHealth < 0.4)
        {
            healthBar.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            healthBar.GetComponent<Image>().color = Color.red;
        }

        healthBar.fillAmount = health / startHealth;
        
        if (health<=0 && !isDead)
        {
            Die();
        }
    }



    public void Slow(float slowPct)
    {

        speed = startSpeed * (1 - slowPct);
    }

    void Die()
    {
        if (TagEnemiesName == "Ally")//entonces es un enemy
        {
            isDead = true;
            PlayerStats.Money += reward;

            profitsUIEnemy.EnemyDieSetTargetProfitsText(this);

            GameObject effect = (GameObject)Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            
            WaveSpawner.EnemiesAlive--;
        }
        else if(TagEnemiesName == "Enemy")//entonces es un turret
        {
            Debug.Log("alliado muerto");
        }
        
        Destroy(gameObject);
    }

    




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
