using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager> {

    #region Serialize Fields
    [SerializeField]
    private Button damageUpgradeButton;
    [SerializeField]
    private Button fireCooldownUpgradeButton;
    private Text creditText;
    #endregion

    #region Private Variables
    private int currentScore;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        currentScore = ScoreManager.Instance.CurrentScore;
    }

    private void Update()
    {
        currentScore = ScoreManager.Instance.CurrentScore;
    }
    #endregion

    #region Public Methods
    public void RefreshShopCanvas()
    {
        RefreshShopButtonInfo(damageUpgradeButton, UpgradeManager.Instance.SelectedTurret.CommonTurretDamageUpgrades, UpgradeManager.Instance.SelectedTurret.CurrentDamageLevel);
        RefreshShopButtonInfo(fireCooldownUpgradeButton, UpgradeManager.Instance.SelectedTurret.CommonTurretFireCooldownUpgrades, UpgradeManager.Instance.SelectedTurret.CurrentFireCooldownLevel);
        
    }
    #endregion

    #region Private Methods
    private void RefreshShopButtonInfo(Button currentButton, List<Upgrade> currentUpgradeList, int currentUpgradeLevel)
    {
        if (currentUpgradeLevel < currentUpgradeList.Count - 1)
        {
            bool playerCanBuyUpgrade = GetPlayerBuyUpgrade(currentUpgradeList, currentUpgradeLevel);
            float upgradePrice = currentUpgradeList[currentUpgradeLevel + 1].UpgradePrice;
            SetUpgradeCostText(currentButton, playerCanBuyUpgrade, upgradePrice.ToString());
            SetUpgradeLevelText(currentButton, currentUpgradeList, currentUpgradeLevel + 1);
        }
        else
        {
            currentButton.interactable = false;
            SetUpgradeCostText(currentButton, false, "MAX LEVEL");
            SetUpgradeLevelText(currentButton, currentUpgradeList, currentUpgradeLevel + 1);
        }
    }

    private void SetUpgradeLevelText(Button currentButton, List<Upgrade> currentUpgradeList, int nextUpgradeLevel)
    {
        Text upgradeLevelText = currentButton.transform.GetChild(1).GetComponent<Text>();
        upgradeLevelText.text = nextUpgradeLevel + "/" + currentUpgradeList.Count;
    }

    private void SetUpgradeCostText(Button currentButton, bool playerCanBuyUpgrade, string upgradePrice)
    {
        Text upgradeCostText = currentButton.transform.GetChild(2).GetComponent<Text>();
        upgradeCostText.color = (playerCanBuyUpgrade) ? Color.green : Color.red;
        upgradeCostText.text = "COST: " + upgradePrice + "";
    }

    private bool GetPlayerBuyUpgrade(List<Upgrade> currentUpgradeList, int currentUpgradeLevel)
    {
        return UpgradeManager.Instance.PlayerHaveMoney(currentUpgradeList, currentUpgradeLevel);
    }
    #endregion
}
