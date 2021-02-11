using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShootRayCast : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private string turretLayerName = "Turret";
    [SerializeField] private string EnemyLayerName = "Enemy";
    [SerializeField] private string NodeLayerName = "Enviroment";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ignore UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;

            //layerMask = 1 << LayerMask.NameToLayer(layerTurret);
            int layerMask = (1 << 8) | (1 << 9) | (1 << 10);
            //  layerMask = (layerMask | (1 << LayerMask.NameToLayer(layerTurretName)));
          
            // This would cast rays only against colliders in layer 10.
            // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
            //layerMask1 = ~layerMask1;
            
            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, layerMask))
            {
                GameObject hit = _hitInfo.collider.gameObject;
                Debug.Log("IMPACT with: " + hit);

                //impact wit Node
                if (hit.GetComponent<Node>())
                {
                    hit.GetComponent<Node>().RayCastOnClick();
                }
                //impact wit Turret
                else if (hit.GetComponent<Turret>())
                {
                    hit.GetComponent<Turret>().RayCastOnClick();
                }
                //impact wit Enemy
                else if (hit.GetComponent<Unit>())
                {
                    hit.GetComponent<Unit>().RayCastOnClick();
                }
            }
        }
    }
}
