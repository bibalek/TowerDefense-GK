using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour {

    #region Serialized Fields
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifeTime = 8f;
    #endregion

    #region Public Properties
    public Transform ProjectileTarget { get { return target; } set { target = value; } }
    public int DamageToDeal { get { return damageToDeal; } set { damageToDeal = value; } }
    #endregion

    #region Private Fields
    private Transform target;
    private Vector3 direction;
    private float currentDistance;
    private int damageToDeal;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        if(target != null)
        {
            direction = target.position - this.transform.localPosition;
        }
        currentDistance = Time.deltaTime * speed;
        if(direction.magnitude > currentDistance)
        {
            transform.Translate(direction.normalized * currentDistance, Space.World);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.parent.CompareTag("Enemy"))
        {
            IDestructibleUnit destructibleUnit = collider.gameObject.GetComponentInParent<IDestructibleUnit>();
            if (destructibleUnit != null)
            {
                destructibleUnit.DealDamage(damageToDeal);
            }
            Destroy(gameObject);
        }
    }
    #endregion

    #region Private Methods
    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    #endregion
}
