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

    private void Start()
    {

    }

    #region Public Properties
    public WaveIngredient CurrentWaveIngredient { get { return this; } }
    public GameObject EnemyPrefab { get { return enemyPrefab; } }
    public int Amount { get { return amount; } }
    #endregion
}
