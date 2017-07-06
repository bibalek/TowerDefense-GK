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
    [SerializeField, HideInInspector]
    private UnityEvent onFailedBuild;
    [SerializeField, HideInInspector]
    private UnityEvent onNotEnoughMoney;
    #endregion

    #region Public Properties
    public UnityEvent OnEnemyDestroyed { get { return onEnemyDestroyed; } set { onEnemyDestroyed = value; } }
    public UnityEvent OnEnemyHit { get { return onEnemyHit; } set { onEnemyHit = value; } }
    public UnityEvent OnScoreChange { get { return onScoreChange; } set { onScoreChange = value; } }
    public UnityEvent OnFailedBuild { get { return onFailedBuild; } set { onFailedBuild = value; } }
    public UnityEvent OnNotEnoughMoney { get { return onNotEnoughMoney; } set { onNotEnoughMoney = value; } }
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

    public void FailedBuild()
    {
        if (onFailedBuild != null)
        {
            onFailedBuild.Invoke();
        }
    }

    public void NotEnoughMoney()
    {
        if (onNotEnoughMoney != null)
        {
            onNotEnoughMoney.Invoke();
        }
    }
    #endregion
}

