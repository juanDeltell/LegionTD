using UnityEngine;
using UnityEngine.UI;

public class TurretSelectionPanel : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;
    public TurretBluePrint king;
    public TurretBluePrint recolector;

    [Header("Menu enlaced")]
    public GameObject actionsMenu;
    public GameObject turretSelectionPanel;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret selected.");
        buildManager.SelectToBuild(standardTurret);
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher selected.");
        buildManager.SelectToBuild(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer selected.");
        buildManager.SelectToBuild(laserBeamer);
    }
    public void SelectKing()
    {
        Debug.Log("King selected.");
        buildManager.SelectToBuild(king);
    }

    public void SelectRecolector()
    {
        Debug.Log("Recolector selected.");
        buildManager.BuildRecolector(recolector);
    }

    public void SelectCancel()
    {
        Debug.Log("Cancel selected.");
        buildManager.DeselectNode();
        buildManager.DeselectTurretToBuild();

    }

    public void SelectBack()
    {
        Debug.Log("back selected.");
        turretSelectionPanel.SetActive(false);
        actionsMenu.SetActive(true);
    }

}
