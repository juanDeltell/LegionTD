using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingsLifeUI : MonoBehaviour
{
    public Text kingsLifeText;

    // Update is called once per frame
    void Update()
    {
        //TODO no hacer asi, sino con routine o algo diferente
        kingsLifeText.text = PlayerStats.KingLife.ToString();
    }
}
