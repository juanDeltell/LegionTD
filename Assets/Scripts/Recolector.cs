using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolector : MonoBehaviour
{

    public float speed;
    public float startSpeed = 1f;
    public int capacity;
    public int startCapacity = 1;


    private Transform target;
    BuildManager buildManager;
    private TurretBluePrint turretBluePrint;

    private int wavePointsIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = WayPointsIncome1.pointsRecolector[0];
        buildManager = BuildManager.instance;
        speed = startSpeed;
        capacity = startCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized* speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            //   StartCoroutine(GetNextWayPoint());
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavePointsIndex >= WayPointsIncome1.pointsRecolector.Length - 1)
        {
            wavePointsIndex = 0;
            target = WayPointsIncome1.pointsRecolector[wavePointsIndex];
            EarnMoney();
            return;
        }

        if (wavePointsIndex == 1)//is on ATM
        {
            // yield return new WaitForSeconds(3f);
            TakeMoneyFromATM();
        }
        wavePointsIndex++;
        target = WayPointsIncome1.pointsRecolector[wavePointsIndex];
        
    }

    void EarnMoney()
    {
        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffect, transform.position, Quaternion.identity);
        Destroy(sellEffect, 5f);
        PlayerStats.Euros += capacity;
    }

    void TakeMoneyFromATM()
    {
        GameObject getMonyFromATMEffect = (GameObject)Instantiate(buildManager.sellEffect, transform.position, Quaternion.identity);
        Destroy(getMonyFromATMEffect, 5f);
    }

}


