using UnityEngine;
using System.Collections;


public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager on scene!.");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;
    public Node recolectorNode;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    private TurretBluePrint turretToBuild;
    private Node selectedNode;


    public NodeUI nodeUI;
    public ProfitsUI profitsUI;
    public Recolector recolector;

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();   
            return;
        }
        selectedNode = node;
        DeselectTurretToBuild();
        nodeUI.SetTarget(node);
        
        ////////¿¿¿????profitsUI.Show();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
        //profitsUI.Hide();
    }

    public void DeselectTurretToBuild()
    {
        turretToBuild = null;
    }

    public void SelectToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void BuildRecolector(TurretBluePrint turret)
    {
       // turretToBuild = turret;
        building(turret);
        //DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }





    private void building(TurretBluePrint turret)
    {

        if (PlayerStats.Money < turret.cost)
        {
            Debug.Log("Not enough money to build that. Money left:" + PlayerStats.Money);
            return;
        }
        PlayerStats.Money -= turret.cost;


        GameObject _turret = (GameObject)Instantiate(turret.prefab, recolectorNode.transform.position, Quaternion.identity);

        GameObject effect = (GameObject)Instantiate(buildEffect, getRecolectorInitialPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        profitsUI.SetTargetLoosesText(recolectorNode);

        Debug.Log("Recolector built. Player money left: " + PlayerStats.Money);

    }

    private Vector3 getRecolectorInitialPosition()
    {
        Vector3 pos = new Vector3(0, 0.25f, 0);
        return pos;
    }
}
