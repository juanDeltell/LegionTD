using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float damage = 50;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
       // _target.transform.position = target.position * .25f;  poruqe hay un offset
        
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)//we have hit something
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized *  distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(effectIns, 3f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
       
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            //Debug.Log("daño en area a : ");

            if(collider.gameObject.tag == "Enemy")
            {
               // Debug.Log("Enemy " + collider);
                Damage(collider.transform);
            } 
            if(collider.tag == "enemy")
            {
               // Debug.Log("enemy " + collider);
                Damage(collider.transform);
            } 
           /* if (collider.tag == "Map")
            {
                Debug.Log("es un Map: " + collider);
            }*/
         

        }
        //Debug.Log("fin.                 ");
    }

    void Damage(Transform enemy)
    {
        Unit e = enemy.GetComponent<Unit>();
        if(e != null)
        {
            e.TakeDamage(damage);
            return;
        }
        Turret t = enemy.GetComponent<Turret>();
        if (t != null)
        {
            t.TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
