using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected string tag;

    protected Fight_Grid_Manager fgm;
    protected bool reachedPoint = false;
    public int Index { get; set; } 

    protected void Update()
    {
        GoToNext(fgm.PathVectors[Index]);

        if (reachedPoint)
        {
            reachedPoint = false;
            Index++;
            if (Index > fgm.PathVectors.Count - 1) // If we reached the final destination
            {
                //TODO: put back
                ObjectPooler.Instance.Put(tag, gameObject);
            }
        }
    }

    protected void GoToNext(Vector3 next)
    {
        transform.position = Vector3.MoveTowards(transform.position, next, movementSpeed * Time.deltaTime);
        if (transform.position == next) reachedPoint = true;
    }
}
