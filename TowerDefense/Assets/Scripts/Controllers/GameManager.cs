using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    #region
    [SerializeField]
    private int maxLives = 100;
    [SerializeField]
    private float timeToRestartLevel = 4;
    #endregion

    #region Private Fields
    private int currentLives;
    private int maxEnemies;
    private int currentEnemiesDestroyed;
    private bool endGameAvaible = false;
    #endregion

    #region Public Properties
    public int CurrentLives { get { return currentLives; } }
    public bool EndGameAvaible { get { return endGameAvaible; } set { endGameAvaible = value; } }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        currentLives = maxLives;
        GameEventManager.Instance.OnEnemyDestroyed.AddListener(AddDestroyedEnemy);
    }
    #endregion

    #region Public Methods
    public void SubstractLives(int livesToSubstract)
    {
        currentLives -= livesToSubstract;
        if (currentLives <= 0)
        {
            currentLives = 0;
            StartCoroutine(LooseGame());
        }
    }

    public void AddSpawnedEnemy()
    {
        maxEnemies++;
    }
    #endregion

    #region Private Methods
    private IEnumerator LooseGame()
    {
        UIManager.Instance.ShowLooseGamePanel();
        yield return new WaitForSeconds(timeToRestartLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WinGame()
    {
        UIManager.Instance.ShowWinGamePanel();
        yield return new WaitForSeconds(timeToRestartLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AddDestroyedEnemy()
    {
        currentEnemiesDestroyed++;
        if (endGameAvaible)
        {
            if (currentEnemiesDestroyed == maxEnemies)
            {
                StartCoroutine(WinGame());
            }
        }
    }
    #endregion
}
