using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionsMenu : MonoBehaviour
{


    [Header("Menu enlaced")]
    public GameObject thisActionsMenu;
    public GameObject turretSelectionPanel;
    public GameObject championPanel;
    public GameObject incomePanel;


    public void SelectBuild()
    {
        Debug.Log("build selected.");
        thisActionsMenu.SetActive(false);
        turretSelectionPanel.SetActive(true);
    }

    public void SelectChampion()
    {
        Debug.Log("champion selected.");
        thisActionsMenu.SetActive(false);
        championPanel.SetActive(true);
    }

    public void SelectIncome()
    {
        Debug.Log("income selected.");
        thisActionsMenu.SetActive(false);
        incomePanel.SetActive(true);
    }
}
