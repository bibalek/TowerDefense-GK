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
    [SerializeField]
    private int livesToSubstract;
    [SerializeField]
    private GameObject destroyEffect;
    #endregion

    #region Private Fields
    private Collider[] colliders;
    #endregion

    #region Public Properties
    public int LivesToSubstract { get { return livesToSubstract; } }
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        currentHitPoints = maxHitPoints;
    }

    private void Update()
    {

    }
    #endregion

    #region Implemented Methods

    public void DealDamage(int damageToDeal)
    {
        GameEventManager.Instance.EnemyHit();
        currentHitPoints -= damageToDeal;
        if (currentHitPoints <= 0)
        {
            destroyEffect.SetActive(true);
            foreach(Collider col in colliders)
            {
                col.enabled = false;
            }
            DestroyUnit();
        }
    }

    public void DestroyUnit()
    {
        GameEventManager.Instance.EnemyDestroyed();
        Destroy(gameObject, 0.8f);
    }

    #endregion
}
