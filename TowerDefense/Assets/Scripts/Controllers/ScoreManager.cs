using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    [SerializeField]
    private int startScore;
    [SerializeField]
    private int currentScore;

    private void Awake()
    {
        currentScore = startScore;
        GameEventManager.Instance.OnEnemyDestroyed.AddListener(() => AddScore(5));
    }

    private void Update()
    {

    }

    public int CurrentScore { get { return currentScore; } }

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
}
