using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    #region Serialize Fields
    [SerializeField]
    private string name = "Default Upgrade";
    [SerializeField]
    private int upgradeValue;
    [SerializeField]
    private int upgradePrice;
    #endregion

    #region Public Properties
    public string Name { get { return name; } }
    public int UpgradeValue { get { return upgradeValue; } }
    public int UpgradePrice { get { return upgradePrice; } }
    #endregion
}