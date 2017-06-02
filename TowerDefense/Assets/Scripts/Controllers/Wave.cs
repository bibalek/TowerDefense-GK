using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    #region Serialize Fields
    //[SerializeField]
    //private List<WaveIngredient> wave;
    [SerializeField]
    private WaveIngredient[] wave;
    #endregion

    public WaveIngredient[] CurrentWave { get; private set; }

    //public Wave(GameObject prefab, int startSize, int maxSize, string name)
    //{
    //    oryginalPrefab = prefab;
    //    startPoolSize = startSize;
    //    maxPoolSize = maxSize;
    //    poolName = name;
    //}

}
