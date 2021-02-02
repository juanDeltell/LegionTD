using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{


    public Text dollarsText;
    public Text eurosText;
    public Text incomeText;

    // Update is called once per frame
    void Update()
    {
        //TODO no hacer asi, sino con routine o algo diferente
        dollarsText.text = PlayerStats.Money.ToString();
        eurosText.text = PlayerStats.Euros.ToString();
        incomeText.text = PlayerStats.Income.ToString();
    }
}
