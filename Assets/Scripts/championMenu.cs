using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class championMenu : MonoBehaviour
{


    [Header("Menu enlaced")]
    public GameObject ActionsMenu;
    public GameObject thisChampionPanel;
    


    public void SelectAttack()
    {
        Debug.Log("vas a atacar.");
        
    }

    public void SelectSpell()
    {
        Debug.Log("spell selected.");
      
    }

    public void SelectBooster()
    {
        Debug.Log("booster selected.");
    }

    public void SelectCancel()
    {
        Debug.Log("cancel selected.");
    }

    public void SelectBack()
    {
        Debug.Log("back selected.");
        thisChampionPanel.SetActive(false);
        ActionsMenu.SetActive(true);
    }
}
