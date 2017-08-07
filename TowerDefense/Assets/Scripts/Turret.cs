using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    float range;
    [SerializeField]
    float searchCooldown;
    [SerializeField]
    GameObject partRotatingInYAxis;
    [SerializeField]
    private int buildCost;
    [SerializeField]
    private List<Upgrade> commonTurretDamageUpgrades;
    [SerializeField]
    private List<Upgrade> commonTurretFireCooldownUpgrades;
    #endregion

    #region Public Properties
    public float FireRange { get { return range; } }
    public Collider CurrentTarget { get { return closestEnemy; } }
    public int BuildCost { get { return buildCost; } }
    public int CurrentDamageLevel { get { return currentDamageLevel; } set { currentDamageLevel = value; } }
    public int CurrentFireCooldownLevel { get { return currentFireCooldownLevel; } set { currentFireCooldownLevel = value; } }
    public List<Upgrade> CommonTurretDamageUpgrades { get { return commonTurretDamageUpgrades; } }
    public List<Upgrade> CommonTurretFireCooldownUpgrades { get { return commonTurretFireCooldownUpgrades; } }
    #endregion

    #region Private Properties
    private Collider[] enemies;
    private Collider closestEnemy;
    private float minimumDistance = Mathf.Infinity;
    private float currentDistance;
    private int enemyLayer;
    private bool canSearch = true;
    private ProjectileLauncher projectileLauncherScript;
    private int currentDamageLevel = 0;
    private int currentFireCooldownLevel = 0;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        projectileLauncherScript = GetComponent<ProjectileLauncher>();
        SetCurrentUpgradesValues();
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        TryToSearchForEnemy();
        TryToRotateTowardsEnemy();
    }

    private void OnMouseDown()
    {
        if (UpgradeManager.Instance.CanUpgrade)
        {
            UIManager.Instance.ChangeUpgradePanelVisibility(this);
        }
    }
    #endregion

    #region Public Methods
    public void SetCurrentUpgradesValues()
    {
        projectileLauncherScript.TurretDamage = commonTurretDamageUpgrades[currentDamageLevel].UpgradeValue;
        projectileLauncherScript.FireCooldown = commonTurretFireCooldownUpgrades[currentFireCooldownLevel].UpgradeValue;
    }
    #endregion

    #region Private Methods
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
        if (Vector3.Distance(transform.position, closestEnemy.transform.position) > range)
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
    #endregion
}
