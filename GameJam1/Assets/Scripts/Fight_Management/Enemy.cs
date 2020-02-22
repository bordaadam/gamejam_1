using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Fight_Grid_Manager manager;
    [SerializeField] private float movementSpeed;
    private bool reachedPoint = false;
    private int index = 0;
    public int Index
    {
        get; set;
    }

    void Start()
    {
        manager = Fight_Grid_Manager.Instance;
    }

    void Update()
    {
        GoToNext(manager.PathVectors[Index]);

        if (reachedPoint)
        {
            reachedPoint = false;
            Index++;
            if (Index > manager.PathVectors.Count - 1) // If we reached the final destination
            {
                //TODO: put back
                ObjectPooler.Instance.Put("Enemy", gameObject);
            }
        }
    }

    private void GoToNext(Vector3 next)
    {
        transform.position = Vector3.MoveTowards(transform.position, next, movementSpeed * Time.deltaTime);
        if (transform.position == next) reachedPoint = true;
    }


}
