﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    #region Serialized Fields
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireCooldown;
    [SerializeField]
    private Transform barrel;

    #endregion

    private Turret turretScript;
    private float fireRange;
    private bool canShoot = true;
    private Collider currentTarget;

    #region Unity Callbacks
    private void Start()
    {
        turretScript = GetComponent<Turret>();
        fireRange = turretScript.FireRange;
    }

    private void Update()
    {
        if (canShoot && (currentTarget = turretScript.CurrentTarget) != null)
        {
            Shoot();
            StartCoroutine(Cooldown(fireCooldown));
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator Cooldown(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        SetProjectileTransform(projectile);
    }

    private void SetProjectileTransform(GameObject projectile)
    {
        SimpleProjectile simpleProjectile = projectile.GetComponent<SimpleProjectile>();
        simpleProjectile.ProjectileTarget = currentTarget.transform;
        projectile.transform.forward = barrel.forward;
        projectile.transform.position = barrel.position;
    }

    #endregion
}