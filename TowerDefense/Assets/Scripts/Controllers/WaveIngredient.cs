using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveIngredient
{
    #region Serialize Fields
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int amount;
    #endregion

    #region Public Properties
    public WaveIngredient CurrentWaveIngredient { get; private set; }
    public GameObject EnemyPrefab { get; private set; }
    public int Amount { get { return amount; } }
    #endregion
}
