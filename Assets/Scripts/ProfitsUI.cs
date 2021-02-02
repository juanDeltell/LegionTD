using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProfitsUI : MonoBehaviour
{
    public Text moneyText;

    private Node target;
    private Unit targetEnemy;

    public GameObject ui;
    public GameObject MoneyEuroGO;

    public Color profitsColor = new Color(248, 185, 0);
    public Color loosesColor = Color.red;

    Vector3 offset = new Vector3(-5, 0, 0);

    public void SetTargetProfitsText(Node _target)
    {
        Show();
        target = _target;
        transform.position = target.GetBuildPosition() + offset;
        ShowProfits();
        moneyText.color = profitsColor;
        if (!target.isUpgraded)
        {
            moneyText.text = "+ $" + target.turretBluePrint.GetSellAmount().ToString();
            Debug.Log("basica");
        }
        else
        {
            moneyText.text = "+ $" + target.turretBluePrint.GetSellAmountUpgraded().ToString();
            Debug.Log("upgrade");
        }
        Invoke("Hide", 1);
    }

    public void SetTargetLoosesText(Node _target)
    {
        Show();
        target = _target;
        transform.position = target.GetBuildPosition() + offset;
        moneyText.color = loosesColor;
        if (target.isTurretOn)
        {
            moneyText.text = "- $" + target.turretBluePrint.upgradeCost.ToString();
        }
        else
        {
            moneyText.text = "- $" + target.turretBluePrint.cost.ToString();
        }

        Invoke("Hide", 1);
    }

    public void EnemyDieSetTargetProfitsText(Unit _target)
    {
        Show();
        targetEnemy = _target;
        transform.position = targetEnemy.transform.position + offset;
        ShowProfits();
        moneyText.color = profitsColor;
       
        moneyText.text = "+ $" + targetEnemy.reward.ToString();
        Debug.Log("enemy die, getting reward");
       
        Invoke("Hide", 1);
    }

    public void IncomeProfitsText(GameObject _target)
    {
        Show();
        MoneyEuroGO = _target;
        transform.position = MoneyEuroGO.transform.position + offset;
        ShowProfits();
        moneyText.color = profitsColor;

        moneyText.text = "INCOME: + $" + PlayerStats.Euros.ToString();
        Debug.Log("getting income: " + PlayerStats.Euros);

        Invoke("Hide", 1);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Show()
    {
        gameObject.GetComponent<Animator>().Rebind();

        ui.SetActive(true);
        
        gameObject.GetComponent<Animator>().Play("money", -1, 0);
    }

    public void ShowProfits()
    {
        moneyText.gameObject.SetActive(true);
    }

    public void HideProfits()
    {
        moneyText.gameObject.SetActive(false);
    }

}
