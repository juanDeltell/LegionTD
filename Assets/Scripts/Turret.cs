using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Unit targetEnemy;

    
    [Header("Use Bullets (Default)")]
    
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    private Node nodeOfThisTurret;

    [Header("Use Laser")]

    public float slowAmount = .5f;
    public bool useLaser = false;
    public int damageOverTime = 30;//damage per second
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public float turnSpeed = 5f;
    public float range = 3f;
    public float startHealth = 100;
    public float health;
    public Transform firePoint;
    public Transform partToRotate;
    public Image healthBar;


    public void setNodeOfThisTurret(Node node)
    {
        nodeOfThisTurret = node ;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)//found enemy nearest 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;  
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Unit>();
        }
        else
        {
            target = null;
        }
    }

    public void RayCastOnClick()
    {
        nodeOfThisTurret.RayCastOnClick();
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        //target look on
        LookOnTarget();

        if (useLaser) {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0f)//time to shoot
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        Vector3 offset = new Vector3(0, 0.25f, 0);
        lineRenderer.SetPosition(1 , target.position + offset);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * .25f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
       
        if(bullet != null)
        {
            bullet.Seek(target);
        }
        // Debug.Log("shoot!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void TakeDamage(float amount)
    {

        
        if (transform.name == "King")
        {
            PlayerStats.KingLife -= amount; 
        }
        if (PlayerStats.KingLife <= 0)
        {
            GameOver();    
        }

        health -= amount;

        if (health / startHealth >= 0.4)
        {
            healthBar.GetComponent<Image>().color = Color.green;
        }
        else if (health / startHealth >= 0.25 && health / startHealth < 0.4)
        {
            healthBar.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            healthBar.GetComponent<Image>().color = Color.red;
        }

        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //PlayerStats.Money += reward;
        Destroy(gameObject);
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

    }
}
