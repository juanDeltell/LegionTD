using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Euros;
    public static int Income;
    public int startMoney = 400;
    public int startIncome = 0;
    public int startEuros = 0;
    public static float KingLife;
    public float startKingLife = 20;
    public static int rounds;

    public List<GameObject> PlayerTurrets;


    [Header("King")]
    public Turret king;

    void Start()
    {
        Money = startMoney;
        Euros = startEuros;
        Income = startIncome;
        KingLife = startKingLife;
        king.health = KingLife;
        rounds = 0;
    }

}
