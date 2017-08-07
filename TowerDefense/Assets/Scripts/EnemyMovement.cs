using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    #endregion

    #region PrivateVariables
    private Transform nextPathNode;
    private int currentNodeIndex = 0;
    private Vector3 direction;
    private float currentDistance;
    private GameObject path;
    private Enemy enemy;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        path = WaveManager.Instance.Path;
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (nextPathNode == null)
        {
            SearchForNextNode();
        }
        else
        {
            GoToNextNode();
        }
    }
    #endregion

    #region Private Methods
    private void GetNextPathNode()
    {
        if (currentNodeIndex < path.transform.childCount)
        {
            nextPathNode = path.transform.GetChild(currentNodeIndex);
            currentNodeIndex++;
        }
        else
        {
            nextPathNode = null;
        }
    }

    private void SearchForNextNode()
    {
        GetNextPathNode();
        if (nextPathNode == null)
        {
            EndPath();
        }
    }

    private void GoToNextNode()
    {
        direction = nextPathNode.position - transform.localPosition;
        currentDistance = speed * Time.deltaTime;
        if (direction.magnitude < currentDistance)
        {
            NextNodeReached();
        }
        else
        {
            FollowCurrentNextNode();
        }
    }

    private void NextNodeReached()
    {
        nextPathNode = null;
    }

    private void FollowCurrentNextNode()
    {
        transform.Translate(direction.normalized * currentDistance, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void EndPath()
    {
        GameEventManager.Instance.PlayerHit();
        GameManager.Instance.SubstractLives(enemy.LivesToSubstract);
        Destroy(gameObject);
    }
    #endregion
}
