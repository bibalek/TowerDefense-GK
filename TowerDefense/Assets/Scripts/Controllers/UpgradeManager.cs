using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{

    #region Debug
    public bool resetPlayerPrefs;
    #endregion
    #region Serialize Fields
    //[SerializeField]
    //private List<Upgrade> commonTurretDamageUpgrades;
    //[SerializeField]
    //private List<Upgrade> commonTurretFireCooldownUpgrades;
    [SerializeField]
    private ShopManager shopManager;

    [SerializeField]
    private Turret selectedTurret;
    #endregion

    #region Private Fields
    private int currentDamageLevel = 1;
    private int currentFireCooldownLevel = 1;

    #endregion

    #region Public Properties
    public int CurrentDamageLevel { get { return currentDamageLevel; } }
    public int CurrentFireCooldownLevel { get { return currentFireCooldownLevel; } }
    public Turret SelectedTurret { get { return selectedTurret; } set { selectedTurret = value; } }


    //public List<Upgrade> CommonTurretDamageUpgrades { get { return commonTurretDamageUpgrades; } }
    //public List<Upgrade> CommonTurretFireCooldownUpgrades { get { return commonTurretFireCooldownUpgrades; } }

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
            selectedTurret.CurrentDamageLevel++;
            ScoreManager.Instance.SubtractScore(selectedTurret.CommonTurretDamageUpgrades[currentDamageLevel - 1].UpgradePrice);
            
            shopManager.RefreshShopCanvas();
            selectedTurret.SetCurrentUpgradesValues();
        }
    }

    public void BuyCommonTurretFireCooldownUpgrade()
    {
        if (PlayerCanBuyUpgrade(selectedTurret.CommonTurretFireCooldownUpgrades, selectedTurret.CurrentFireCooldownLevel))
        {
            currentFireCooldownLevel++;
            ScoreManager.Instance.SubtractScore(selectedTurret.CommonTurretFireCooldownUpgrades[currentFireCooldownLevel - 1].UpgradePrice);
            
            shopManager.RefreshShopCanvas();
            selectedTurret.SetCurrentUpgradesValues();
        }
    }

    public bool PlayerCanBuyUpgrade(List<Upgrade> upgradesList, int currentUpgradeLevel)
    {
        if (currentUpgradeLevel < upgradesList.Count - 1)
        {
            return upgradesList[++currentUpgradeLevel].UpgradePrice < ScoreManager.Instance.CurrentScore;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
