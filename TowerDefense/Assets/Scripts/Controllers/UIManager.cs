using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject scoreIndicator;
    private void Start()
    {
        GameEventManager.Instance.OnScoreChange.AddListener(UpdateScore);
        scoreIndicator.GetComponentInChildren<Text>().text = "Score: " + ScoreManager.Instance.CurrentScore + "$";

    }

    private void Update()
    {

    }

    private void UpdateScore()
    {
        scoreIndicator.GetComponentInChildren<Text>().text = "Score: " + ScoreManager.Instance.CurrentScore + "$";
    }
}
