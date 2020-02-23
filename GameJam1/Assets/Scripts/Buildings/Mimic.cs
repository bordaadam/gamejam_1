using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : BuildingBase
{
    [Range(0.0f, 100f)]
    [SerializeField] private float chanceToCatch;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Peasant>() || col.GetComponent<Knight>())
        {
            int random = Random.Range(0, 101);

            if (random < chanceToCatch)
            {
                Catch(col);
            }
            
        }

    }

    void Update()
    {
        // no need to implement
    }

    protected void OnTriggerStay(Collider col)
    {
        // no need to implement
    }

    protected void OnTriggerExit(Collider col)
    {
        // no need to implement
    }

    void Catch(Collider col)
    {
        // Play anim

        if (col.GetComponent<Peasant>())
        {
            ObjectPooler.Instance.Put("Peasant", col.gameObject);
        }
        if (col.GetComponent<Knight>())
        {
            ObjectPooler.Instance.Put("Knight", col.gameObject);
        }

        GameManager.Instance.HumanResources++;
    }

    void Shoot()
    {
        // no need to implement
    }

    protected override void PlaySound()
    {
        throw new System.NotImplementedException();
    }

    protected override void TakeResource()
    {
        throw new System.NotImplementedException();
    }
}
