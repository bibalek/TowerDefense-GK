using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Serialized Fields
    [SerializeField]
    private GameObject scoreIndicator;
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private GameObject youCantBuildHereNotification;
    [SerializeField]
    private GameObject notEnoughMoneyNotification;
    [SerializeField]
    private float notificationFadeoutTime;
    [SerializeField]
    private GameObject markedAreaPrefab;
    [SerializeField]
    private GameObject looseGamePanel;
    [SerializeField]
    private GameObject winGamePanel;
    [SerializeField]
    private GameObject livesIndicator;
    #endregion

    #region Private Fields
    private bool upgradePanelEnabled = false;
    private GameObject markedArea;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        markedArea = Instantiate(markedAreaPrefab, Vector3.zero, Quaternion.identity);
        markedArea.SetActive(false);
        InitializeEventListeners();
        UpdateScore();
        UpdateLives();
    }
    #endregion

    #region Public Methods
    public void ChangeUpgradePanelVisibility(Turret turret)
    {
        if (upgradePanelEnabled)
        {
            UpgradeManager.Instance.SelectedTurret = turret;
            if (UpgradeManager.Instance.PreviousSelectedTurret == UpgradeManager.Instance.SelectedTurret)
            {
                upgradePanel.SetActive(false);
                upgradePanelEnabled = false;
                markedArea.SetActive(false);
                BuildManager.Instance.CanBuild = true;
            }
            else
            {
                UpgradeManager.Instance.PreviousSelectedTurret = turret;
                markedArea.transform.position = turret.transform.position;
                ShopManager.Instance.RefreshShopCanvas();
                markedArea.SetActive(true);
                BuildManager.Instance.CanBuild = false;
            }
        }
        else
        {
            upgradePanel.SetActive(true);
            UpgradeManager.Instance.SelectedTurret = turret;
            UpgradeManager.Instance.PreviousSelectedTurret = turret;
            ShopManager.Instance.RefreshShopCanvas();
            upgradePanelEnabled = true;
            markedArea.transform.position = turret.transform.position;
            markedArea.SetActive(true);
            BuildManager.Instance.CanBuild = false;
        }
    }

    public void ShowLooseGamePanel()
    {
        looseGamePanel.SetActive(true);
    }

    public void ShowWinGamePanel()
    {
        winGamePanel.SetActive(true);
    }
    #endregion

    #region Private Methods
    private void UpdateScore()
    {
        scoreIndicator.GetComponentInChildren<Text>().text = "Score: " + ScoreManager.Instance.CurrentScore + "$";
    }

    private IEnumerator FadeNotification(GameObject notification, float time)
    {
        Text notificationText = notification.GetComponentInChildren<Text>();
        Image panel = notification.GetComponent<Image>();
        Color notificationColor = notificationText.color;
        Color panelColor = panel.color;
        float currentTime = time;
        while (currentTime > 0)
        {
            notificationText.color = new Color(notificationColor.r, notificationColor.g, notificationColor.b, currentTime / time);
            panel.color = new Color(panelColor.r, panelColor.g, panelColor.b, currentTime / time);
            currentTime -= Time.deltaTime;
            yield return null;
        }
        TurnOffNotification(notification);
    }

    private void TurnOffNotifications()
    {
        TurnOffNotification(youCantBuildHereNotification);
        TurnOffNotification(notEnoughMoneyNotification);
    }

    private void TurnOffNotification(GameObject notification)
    {
        notification.SetActive(false);
    }

    private void ShowYouCantBuildHereNotification()
    {
        SetNotification(youCantBuildHereNotification);
    }

    private void ShowNotEnoughMoneyNotification()
    {
        SetNotification(notEnoughMoneyNotification);
    }
    private void SetNotification(GameObject notification)
    {
        TurnOffNotifications();
        notification.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeNotification(notification, notificationFadeoutTime));
    }

    private void UpdateLives()
    {
        livesIndicator.GetComponentInChildren<Text>().text = "Lives: " + GameManager.Instance.CurrentLives;
    }

    private void InitializeEventListeners()
    {
        GameEventManager.Instance.OnScoreChange.AddListener(UpdateScore);
        GameEventManager.Instance.OnFailedBuild.AddListener(ShowYouCantBuildHereNotification);
        GameEventManager.Instance.OnNotEnoughMoney.AddListener(ShowNotEnoughMoneyNotification);
        GameEventManager.Instance.OnPlayerHit.AddListener(UpdateLives);
    }
    #endregion
}
