using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour
{
    [SerializeField] protected float shootingTime;
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject target;
    protected SphereCollider sphereCollider;
    protected bool canShoot = true;

    [Space]
    [Header("Resources to build")]
    [SerializeField] protected int wood;
    [SerializeField] protected int stone;
    [SerializeField] protected int humanResources;

    protected void Update()
    {
        if (canShoot && target != null)
        {
            Shoot();
            StartCoroutine(Wait(shootingTime));
        }
    }

    protected void OnTriggerStay(Collider col)
    {
        if (target == null)
        {
            target = col.gameObject;
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        target = null;
    }

    protected IEnumerator Wait(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    protected private void Shoot()
    {
        if (target.GetComponent<Peasant>())
        {
            target.GetComponent<Peasant>().currentHealth -= damage;
        }
        if (target.GetComponent<Knight>())
        {
            target.GetComponent<Knight>().currentHealth -= damage;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
