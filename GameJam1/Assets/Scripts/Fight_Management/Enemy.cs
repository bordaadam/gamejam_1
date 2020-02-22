using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Fight_Grid_Manager manager;
    [SerializeField] private float movementSpeed;
    private bool reachedPoint = false;
    private int index = 0;

    void Start()
    {
        manager = Fight_Grid_Manager.Instance;
    }

    void Update()
    {
        GoToNext(manager.PathVectors[index]);

        if (reachedPoint)
        {
            reachedPoint = false;
            index++;
            if (index > manager.PathVectors.Count - 1) // If we reached the final destination
            {
                Destroy(this);
            }
        }
    }

    private void GoToNext(Vector3 next)
    {
        transform.position = Vector3.MoveTowards(transform.position, next, movementSpeed * Time.deltaTime);
        if (transform.position == next) reachedPoint = true;
    }

}
