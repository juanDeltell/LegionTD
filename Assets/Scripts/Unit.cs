using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    [Header("Unit")]
    public float startSpeed = 3f;
    public float speed;
    public float startHealth = 100;
    public float health;
    public float vision = 3f;
    public string TagEnemiesName = "Ally";
    public Image healthBar;

    [Header("Unit: shoot")]
    public GameObject bulletPrefab;
    public float turnSpeed = 5f;
    public Transform firePoint;
    public Transform partToRotate;
    public float damage = 20;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float range = 1f;

    [Header("Unit: enemy")]
  
    private bool isDead = false;
    public int reward = 50;
    public GameObject enemyDeathEffect;

    public ProfitsUI profitsUIEnemy;

    GameManager gameManager;

    [Header("Tower: Laser")]
    public float slowAmount = .5f;
    public bool useLaser = false;
    public int damageOverTime = 30;//damage per second
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    
    

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        
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
