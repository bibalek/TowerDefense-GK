using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private GameObject scoreIndicator;
    [SerializeField]
    private GameObject upgradePanel;

    private bool upgradePanelEnabled = false;
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

    public void ChangeUpgradePanelVisibility()
    {
        if(upgradePanelEnabled)
        {
            upgradePanel.SetActive(false);
            upgradePanelEnabled = false;
        }
        else
        {
            upgradePanel.SetActive(true);
            upgradePanelEnabled = true;
        }
        
    }
}
