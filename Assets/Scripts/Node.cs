using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color notEnoughMonyColor;
    [SerializeField] private Renderer rend;
    [SerializeField] private Color startColor;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretOnLastRound;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string layerTurretName;
    [SerializeField] private Turret king;

    [HideInInspector] public TurretBluePrint turretBluePrint;
    [HideInInspector] public bool isUpgraded;
    public bool isTurretOn;

    WaveSpawner waveSpawner;
    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

        isUpgraded = false;
        isTurretOn = false;

        
        if (this.GetComponent<Node>().name == "Node (100000)")//King
        {
            isTurretOn = true;
            king.setNodeOfThisTurret(this);
        }
           
}

    public void RayCastOnClick()
    {
       
        if (turret != null)
        {
            Debug.Log("hemos llegado aqui??? lo que buskmos");
            buildManager.SelectNode(this);
            return;
        }
        
        if (!buildManager.CanBuild)
                return;
        //    Debug.Log("hemos llegado aqui???  4");
            BuildTurret(buildManager.GetTurretToBuild());
    }


    void BuildTurret(TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that. Money left:" + PlayerStats.Money);
            return;
        }
        PlayerStats.Money -= blueprint.cost;


        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        
        turret = _turret;

        turretBluePrint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        buildManager.profitsUI.SetTargetLoosesText(this);

        turret.GetComponent<Turret>().setNodeOfThisTurret(this);
        
        turretOnLastRound = turret;

        isTurretOn = true;
        Debug.Log("Turret built. Player money left: " + PlayerStats.Money);
    }



    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that. Money left:" + PlayerStats.Money);
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCost;
        
        buildManager.profitsUI.SetTargetLoosesText(this);
        //get rid of the old turret
        Destroy(turret);

        //building the upgrade one
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, transform.position, Quaternion.identity);

        turret = _turret;
        turretOnLastRound = turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

       

        isUpgraded = true;
        Debug.Log("Turret upgraded!. Player money left: " + PlayerStats.Money);
    }

    public void SellTurret()
    {
        if (isUpgraded)
        {
            PlayerStats.Money += turretBluePrint.GetSellAmountUpgraded();
        }
        else
        {
            PlayerStats.Money += turretBluePrint.GetSellAmount();
        }

        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);


        Destroy(turret);
        turret = null;
        turretOnLastRound = null;
        turretBluePrint = null;
        isTurretOn = false;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if(buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMonyColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }


    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    public void NewRound()
    {
        Destroy(turret);
       
        GameObject _turret = (GameObject)Instantiate(turretOnLastRound, transform.position, Quaternion.identity);

        turret = _turret;

    }

    // Update is called once per frame
    void Update()
    {/*
        if (waveSpawner.levelEnded)
        {
            NewRound();
            Debug.Log("estamos en el update de node");
        }
       */     
    }
}
