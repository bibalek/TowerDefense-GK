using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : Singleton<GameEventsManager>
{
    [SerializeField, HideInInspector]
    private UnityEvent onEnemyDestroyed;

    public UnityEvent OnEnemyDestroyed { get { return onEnemyDestroyed; } set { onEnemyDestroyed = value; } }


    public void EnemyDestroyed()
    {
        if (onEnemyDestroyed != null)
        {
            onEnemyDestroyed.Invoke();
        }
    }

}

