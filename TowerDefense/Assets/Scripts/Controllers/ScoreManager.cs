using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    #region Serialized Fields
    [SerializeField]
    private int startScore;
    [SerializeField]
    private int currentScore;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        currentScore = startScore;
        GameEventManager.Instance.OnEnemyDestroyed.AddListener(() => AddScore(5));
    }
    #endregion

    #region Public Properties
    public int CurrentScore { get { return currentScore; } }
    #endregion

    #region Public Methods
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        GameEventManager.Instance.ScoreChanged();
    }

    public void SubtractScore(int scoreToSubtract)
    {
        currentScore -= scoreToSubtract;
        GameEventManager.Instance.ScoreChanged();
    }
    #endregion
}
