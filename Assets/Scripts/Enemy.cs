using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 3f;
    [HideInInspector]
    public float speed;
    public float startHealth = 100;
    private float health;

    [SerializeField]
    private float damage = 20;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float range = 1f;
    public int reward = 50;

    public string alliesTag = "Ally";

    public Image healthBar;

    public GameObject bulletPrefab;
    public float turnSpeed = 5f;
    public Transform firePoint;
    public Transform partToRotate;

    public GameObject enemyDeathEffect;
    GameManager gameManager;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }


    
    public void TakeDamage(float amount)
    {
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

        isDead = true;
        PlayerStats.Money += reward;

       //profitsUI.EnemyDieSetTargetProfitsText(this);

        GameObject effect = (GameObject)Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
     //   WaveSpawner.setEnemiesAlive(WaveSpawner.getEnemiesAlive()--);
        Destroy(gameObject);
        
    }

    




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
