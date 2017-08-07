using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField]
    private GameObject turretToBuild;
    [SerializeField]
    private Transform terrain;

    private bool canBuild = false;
    private GameObject turret = null;
    private int cost = 0;
    private Ray ray;
    private RaycastHit hit;

    public bool CanBuild { get { return canBuild; } set { canBuild = value; } }

    #region Unity Callbacks
    private void Update()
    {
        TryToBuild();
    }
    #endregion

    #region Public Methods
    public void SpawnTurret()
    {
        cost = turretToBuild.GetComponent<Turret>().BuildCost;
        if (ScoreManager.Instance.CurrentScore >= cost)
        {
            turret = Instantiate(turretToBuild);
            canBuild = true;
            UpgradeManager.Instance.CanUpgrade = false;
        }
        else
        {
            GameEventManager.Instance.NotEnoughMoney();
        }  
    }
    #endregion

    #region Private Methods
    private void TryToBuild()
    {
        if (canBuild)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
            }

            turret.transform.position = hit.point;

            CheckMouseClick();
        }
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.point.y >= 4.8 && hit.collider.gameObject.CompareTag("Terrain"))
            {
                BuildTower();
            }
            else
            {
                GameEventManager.Instance.FailedBuild();
            }
        }
    }

    private void BuildTower()
    {
        canBuild = false;
        SetNewTurretSettings();
        ScoreManager.Instance.SubtractScore(cost);
        if (UpgradeManager.Instance.SelectedTurret != null)
        {
            ShopManager.Instance.RefreshShopCanvas();
        }
        UpgradeManager.Instance.CanUpgrade = true;
    }

    private void SetNewTurretSettings()
    {
        turret.transform.position = hit.point;
        turret.GetComponent<Turret>().enabled = true;
        turret.GetComponent<ProjectileLauncher>().enabled = true;
        turret.GetComponent<Collider>().enabled = true;
        turret = null;
    }
    #endregion
}
