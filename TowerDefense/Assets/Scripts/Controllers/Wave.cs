using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField]
    private List<WaveIngredient> waveIngredients;

    public List<WaveIngredient> CurrentIngredients { get { return waveIngredients; } }
    public Wave CurrentWave { get { return this; } }
}
