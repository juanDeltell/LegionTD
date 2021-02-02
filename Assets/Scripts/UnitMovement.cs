using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;

    private Unit unit;

    private bool thereIsTargetToAttack = false;

    // Start is called before the first frame update
    void Start()
    {

        unit = GetComponent<Unit>();
        target = WayPoints.points[0];

        //turret
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        
        //mover si no hay enemigos
        if (!thereIsTargetToAttack)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWayPoint();
            }
        }
        else//movemos hacia el enemigo y atacamos
        {
            //Debug.Log("hay enemigo.Esta en rango?");
            //target look on
            LookOnTarget();
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToEnemy <= unit.range)
            {
                     
               // UnitAttack.readyToShoot();
                if (unit.fireCountDown <= 0f)//time to shoot
                {
                    Shoot();
                    unit.fireCountDown = 1f / unit.fireRate;
                }
                unit.fireCountDown -= Time.deltaTime;
                

            }
            else
            {
                //Debug.Log("no, me acerco a el");
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);
            }
        }


    }


    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(unit.bulletPrefab, unit.firePoint.position, unit.firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        // Debug.Log("shoot!");
    }

    bool UpdateTarget()
    {
        GameObject[] objetives = GameObject.FindGameObjectsWithTag(unit.TagEnemiesName);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestUnitToAttack = null;
        
        foreach (GameObject objetive in objetives)
        {
            float distanceToNextObjetive = Vector3.Distance(transform.position, objetive.transform.position);
            if (distanceToNextObjetive < shortestDistance)//found enemy nearest 
            {
                shortestDistance = distanceToNextObjetive;
                nearestUnitToAttack = objetive;
            }
        }

        if (nearestUnitToAttack != null /*&& shortestDistance <= range*/)    //es nuestro nuevo objetivo
        {
            target = nearestUnitToAttack.transform;
            thereIsTargetToAttack = true;
        }
        else
        {
            thereIsTargetToAttack = false;
            // target = null;
        }
        return thereIsTargetToAttack;
    }


    void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(unit.partToRotate.rotation, lookRotation, Time.deltaTime * unit.turnSpeed).eulerAngles;
        unit.partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }



    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            //EndPath();esto ya no hace falta...pq siempre habra un rey 
            return;
        }
        wavePointIndex++;
        target = WayPoints.points[wavePointIndex];
    }

    void EndPath()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);   
    }

}
