using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TurretBluePrint 
{
    public GameObject prefab;
    public int cost;
    public float health;
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public float upgradeHealth;

    [Header("only recolector")]
    public float speed;
    public float capacity;
    


    public int GetSellAmount()
    {
        return cost / 2;
    }

    public int GetSellAmountUpgraded()
    {
        return (cost + upgradeCost) /2;
    }

}
