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
        turret = Instantiate(turretToBuild);
        canBuild = true;
    }

    private void TryToBuild()
    {
        if (canBuild)
        {
            float dis;
            Vector3 mousePosition = Input.mousePosition;
            //Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 100));
            ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
            Debug.Log(hit.point.y);
            turret.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.point.y >= 4.8 && hit.collider.gameObject.CompareTag("Terrain"))
                {
                    canBuild = false;
                    turret.transform.position = hit.point;
                    turret.GetComponent<Turret>().enabled = true;
                    turret.GetComponent<ProjectileLauncher>().enabled = true;
                    turret = null;
                }
                else
                {
                    Debug.Log("Cant build here!");
                }
            }
        }
    }



}
