using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : Singleton<GameEventManager>
{
    #region Serialized Fields
    [SerializeField, HideInInspector]
    private UnityEvent onEnemyDestroyed;
    [SerializeField, HideInInspector]
    private UnityEvent onEnemyHit;
    [SerializeField, HideInInspector]
    private UnityEvent onScoreChange;
    #endregion

    #region Public Properties
    public UnityEvent OnEnemyDestroyed { get { return onEnemyDestroyed; } set { onEnemyDestroyed = value; } }
    public UnityEvent OnEnemyHit { get { return onEnemyHit; } set { onEnemyHit = value; } }
    public UnityEvent OnScoreChange { get { return onScoreChange; } set { onScoreChange = value; } }
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

    public void ScoreChanged()
    {
        if (onScoreChange != null)
        {
            onScoreChange.Invoke();
        }
    }
    #endregion
}

