using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitAttack : MonoBehaviour
{
    private Unit enemy;
    private Transform target;

    public void readyToShoot()
    {
        if (enemy.fireCountDown <= 0f)//time to shoot
        {
            Shoot();
            enemy.fireCountDown = 1f / enemy.fireRate;
        }
        enemy.fireCountDown -= Time.deltaTime;
    }



    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(enemy.bulletPrefab, enemy.firePoint.position, enemy.firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
        // Debug.Log("shoot!");
    }

}
