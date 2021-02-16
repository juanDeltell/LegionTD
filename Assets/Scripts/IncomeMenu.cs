using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeMenu : MonoBehaviour
{
    [Header("Menu enlaced")]
    [SerializeField] private GameObject ActionsMenu;

    [SerializeField] private GameObject thisIncomeMenu;

    [SerializeField] private GameObject incomeMEnuUI;

    [SerializeField] private GameObject pitchSurface;

    [SerializeField] private GameObject IncomeOne;
    
    [SerializeField] private GameObject IncomeTwo;
    
    [SerializeField] private GameObject IncomeThree;
    
    [SerializeField] private GameObject IncomeFour;

    public void SelecIncomeOne()
    {
        
        if (PlayerStats.Euros < 1)
        {
            Debug.Log("Not enough euros (income 1 = 1€ ).");
            return;
        }
        PlayerStats.Euros -= 1;
        PlayerStats.Income += 1;

        WaveSpawner w = new WaveSpawner();
        w.SpawnEnemyIncomePitch(IncomeOne);
        Debug.Log(" 1 income added.");

    }

    public void SelecIncomeTwo()
    {
        if (PlayerStats.Euros < 2)
        {
            Debug.Log("Not enough euros (income 2 = 2€ ).");
            return;
        }
        PlayerStats.Euros -= 2;
        PlayerStats.Income += 2;
        Debug.Log(" 2 income added.");
    }

    public void SelecIncomeThree()
    {
        if (PlayerStats.Euros < 3)
        {
            Debug.Log("Not enough euros (income 3 = 3€ ).");
            return;
        }
        PlayerStats.Euros -= 3;
        PlayerStats.Income += 3;
        Debug.Log(" 3 income added.");
    }

    public void SelecIncomeFour()
    {
        if (PlayerStats.Euros < 4)
        {
            Debug.Log("Not enough euros (income 4 = 4€ ).");
            return;
        }
        PlayerStats.Euros -= 4;
        PlayerStats.Income += 4;
        Debug.Log(" 4 income added.");
    }

    public void SelectBack()
    {
        Debug.Log("back selected.");
        thisIncomeMenu.SetActive(false);
        ActionsMenu.SetActive(true);
    }
}
