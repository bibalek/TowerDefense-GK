using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : Singleton<GameEventsManager>
{
    #region Serialized Fields
    [SerializeField, HideInInspector]
    private UnityEvent onEnemyDestroyed;
    [SerializeField, HideInInspector]
    private UnityEvent onEnemyHit;
    #endregion

    #region Public Properties
    public UnityEvent OnEnemyDestroyed { get { return onEnemyDestroyed; } set { onEnemyDestroyed = value; } }
    public UnityEvent OnEnemyHit { get { return onEnemyHit; } set { onEnemyHit = value; } }
    #endregion

    #region Public Methods
    public void EnemyDestroyed()
    {
        if (onEnemyDestroyed != null)
        {
            onEnemyDestroyed.Invoke();
        }
    }

    public void EnemyHit()
    {
        if (onEnemyHit != null)
        {
            onEnemyHit.Invoke();
        }
    }
    #endregion
}

