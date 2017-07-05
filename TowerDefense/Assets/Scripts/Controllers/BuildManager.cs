using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private GameObject turretToBuild;
    [SerializeField]
    private Transform terrain;

    private bool canBuild = false;
    private GameObject turret = null;
    private int cost = 0;

    #region Unity Callbacks
    private void Start()
    {

    }

    private void Update()
    {
        TryToBuild();
    }
    #endregion

    Ray ray;
    RaycastHit hit;

    public void SpawnTurret()
    {
        cost = turretToBuild.GetComponent<Turret>().BuildCost;
        if (ScoreManager.Instance.CurrentScore >= cost)
        {
            turret = Instantiate(turretToBuild);
            canBuild = true;
        }
        else
        {
            Debug.Log("nem");
        }  
    }

    private void TryToBuild()
    {
        if (canBuild)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }

            turret.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.point.y >= 4.8 && hit.collider.gameObject.CompareTag("Terrain"))
                {
                    canBuild = false;
                    turret.transform.position = hit.point;
                    turret.GetComponent<Turret>().enabled = true;
                    turret.GetComponent<ProjectileLauncher>().enabled = true;
                    turret.GetComponent<Collider>().enabled = true;
                    turret = null;
                    ScoreManager.Instance.SubtractScore(cost);
                }
                else
                {
                    Debug.Log("Cant build here!");
                }
            }
        }
    }



}
