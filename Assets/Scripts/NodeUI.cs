using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;
    public Button upgradeButton;
    public Text upgradeCost;
    public Text sellAmount;
    public ProfitsUI profitsUI;

    public void SetTarget(Node _target)
    {
        Debug.Log("rarta");
        target = _target;
        transform.position = target.GetBuildPosition();
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX.";
            upgradeButton.interactable = false;
        }
        
        ui.SetActive(true);
        if (target.isUpgraded)
        {
            sellAmount.text = "50% ($" + target.turretBluePrint.GetSellAmountUpgraded() +")";
        }
        else{
            sellAmount.text = "50% ($"+ target.turretBluePrint.GetSellAmount() + ")";
        }
           
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        profitsUI.SetTargetProfitsText(target);
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
