using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField]
    float range;
    [SerializeField]
    float searchCooldown;
    [SerializeField]
    GameObject partRotatingInYAxis;
    [SerializeField]
    private int buildCost;

    #region Public Properties
    public float FireRange { get { return range; } }
    public Collider CurrentTarget { get { return closestEnemy; } }
    public int BuildCost { get { return buildCost; } }

    #endregion

    private Collider[] enemies;
    private Collider closestEnemy;
    private float minimumDistance = Mathf.Infinity;
    private float currentDistance;
    private int enemyLayer;
    private bool canSearch = true;

    void Start()
    {
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        TryToSearchForEnemy();
        TryToRotateTowardsEnemy();
    }

    private void OnMouseDown()
    {
        UIManager.Instance.ChangeUpgradePanelVisibility();
    }

    private void TryToSearchForEnemy()
    {
        if (canSearch)
        {
            SearchForNewTarget();
            StartCoroutine(Cooldown(searchCooldown));
        }
    }

    private void TryToRotateTowardsEnemy()
    {
        if (closestEnemy != null && EnemyInRange())
        {
            RotateTowardEnemy();
        }
    }

    private bool EnemyInRange()
    {
        if(Vector3.Distance(transform.position, closestEnemy.transform.position) > range)
        {
            SearchForNewTarget();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RotateTowardEnemy()
    {
        Quaternion lookRotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
        partRotatingInYAxis.transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }

    private IEnumerator Cooldown(float time)
    {
        canSearch = false;
        yield return new WaitForSeconds(time);
        canSearch = true;
    }

    private void SearchForNewTarget()
    {
        closestEnemy = null;
        minimumDistance = Mathf.Infinity;
        enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach (Collider col in enemies)
        {
            if (col.transform.parent.CompareTag("Enemy"))
            {
                FindClosestEnemy(col);
            }
        }
    }

    private void FindClosestEnemy(Collider collider)
    {
        currentDistance = Vector3.Distance(transform.position, collider.transform.position);
        if (currentDistance < minimumDistance)
        {
            minimumDistance = currentDistance;
            closestEnemy = collider;
        }
    }
}
