using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    GameObject path;
    #endregion

    #region PrivateVariables
    private Transform nextPathNode;
    private int currentNodeIndex = 0;
    private Vector3 direction;
    private float currentDistance;
    #endregion

    #region UnityCallbacks
    void Start()
    {

    }

    void Update()
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

    #region PrivateMethods
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
        Destroy(gameObject);
    }
    #endregion
}
