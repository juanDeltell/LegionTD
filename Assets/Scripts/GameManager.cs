using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public ProfitsUI profitsUI;
    public IncomeMenu incomeMenu;

    public static bool GameIsOver;
    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return; 
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }
            
        
        if(PlayerStats.KingLife <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            incomeMenu.SelecIncomeOne();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            incomeMenu.SelecIncomeTwo();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
         //   incomeMenu.SelecIncomeThree();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            incomeMenu.SelecIncomeFour();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //incomeMenu.SelecIncomeOne();
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
           // incomeMenu.SelecIncomeTwo();
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
          //  incomeMenu.SelecIncomeThree();
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
          //  incomeMenu.SelecIncomeFour();
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
          //  incomeMenu.SelecIncomeFour();
        }

    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }

}
