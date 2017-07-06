using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    #region Serialize Fields
    [SerializeField]
    private Turret selectedTurret;
    [SerializeField]
    private Turret previousSelectedTurret;
    [SerializeField]
    private ShopManager shopManager;
    #endregion

    #region Public Properties
    public Turret SelectedTurret { get { return selectedTurret; } set { selectedTurret = value; } }
    public Turret PreviousSelectedTurret { get { return previousSelectedTurret; } set { previousSelectedTurret = value; } }

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
    }
    #endregion

    #region Public Methods
    public void BuyCommonTurretDamageUpgrade()
    {
        if (PlayerCanBuyUpgrade(selectedTurret.CommonTurretDamageUpgrades, selectedTurret.CurrentDamageLevel))
        {
            if(PlayerHaveMoney(selectedTurret.CommonTurretDamageUpgrades, selectedTurret.CurrentDamageLevel))
            {
                ScoreManager.Instance.SubtractScore(selectedTurret.CommonTurretDamageUpgrades[++selectedTurret.CurrentDamageLevel].UpgradePrice);

                shopManager.RefreshShopCanvas();
                selectedTurret.SetCurrentUpgradesValues();
            }
            
        }
    }

    public void BuyCommonTurretFireCooldownUpgrade()
    {
        if (PlayerCanBuyUpgrade(selectedTurret.CommonTurretFireCooldownUpgrades, selectedTurret.CurrentFireCooldownLevel))
        {
            if(PlayerHaveMoney(selectedTurret.CommonTurretFireCooldownUpgrades, selectedTurret.CurrentFireCooldownLevel))
            {
                ScoreManager.Instance.SubtractScore(selectedTurret.CommonTurretFireCooldownUpgrades[++selectedTurret.CurrentFireCooldownLevel].UpgradePrice);

                shopManager.RefreshShopCanvas();
                selectedTurret.SetCurrentUpgradesValues();
            }
            else
            {
                GameEventManager.Instance.NotEnoughMoney();
            }
        }
    }

    public bool PlayerCanBuyUpgrade(List<Upgrade> upgradesList, int currentUpgradeLevel)
    {
        return currentUpgradeLevel < upgradesList.Count - 1;
    }

    public bool PlayerHaveMoney(List<Upgrade> upgradesList, int currentUpgradeLevel)
    {
        return upgradesList[currentUpgradeLevel + 1].UpgradePrice < ScoreManager.Instance.CurrentScore;
    }
    #endregion
}
