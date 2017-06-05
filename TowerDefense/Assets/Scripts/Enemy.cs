using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestructibleUnit
{
    #region Serialized Fields
    [SerializeField]
    private int maxHitPoints;
    [SerializeField]
    private int currentHitPoints;
    #endregion


    #region Unity Callbacks
    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Update()
    {

    }
    #endregion

    #region Implemented Methods

    public void DealDamage(int damageToDeal)
    {
        GameEventsManager.Instance.EnemyHit();
        currentHitPoints -= damageToDeal;
        if(damageToDeal <= 0)
        {
            DestroyUnit();
        }
    }

    public void DestroyUnit()
    {
        GameEventsManager.Instance.EnemyDestroyed();
        Destroy(gameObject);
    }

    #endregion
}
