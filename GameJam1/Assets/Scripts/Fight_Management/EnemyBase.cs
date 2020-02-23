using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected string tag;
    [SerializeField] protected float damage;

    protected Fight_Grid_Manager fgm;
    protected GameManager gm;
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
                // ENEMY REACHED THE FINAL TILE!
                gm.CurrentHealth -= damage;
                gm.RemainingEnemy--;
                //Mate's code
                gm.gameObject.GetComponent<UI_Handler>().UpdateHealthUI();
                ObjectPooler.Instance.Put(tag, gameObject);
                //EOF Mate's code
                FillUpHealth();
            }
        }

        // If the enemy dies...
        if(currentHealth < 0)
        {
            gm.RemainingEnemy--;
            FillUpHealth();
            ObjectPooler.Instance.Put(tag, gameObject);
        }


    }

    protected void GoToNext(Vector3 next)
    {
        transform.position = Vector3.MoveTowards(transform.position, next, movementSpeed * Time.deltaTime);
        if (transform.position == next) reachedPoint = true;
    }

    private void FillUpHealth()
    {
        if(GetComponent<Peasant>())
        {
            GetComponent<Peasant>().currentHealth = maxHealth;
        }

        if (GetComponent<Knight>())
        {
            GetComponent<Knight>().currentHealth = maxHealth;
        }
    }

    protected void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<HangingTrap>())
        {
            Debug.Log("IM AFRAID ");
            movementSpeed /= 2;
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<HangingTrap>())
        {
            Debug.Log("NOT AFRAID ANYMORE");
            movementSpeed *= 2;
        }
    }
}
