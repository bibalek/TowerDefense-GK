using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField]
    private List<WaveIngredient> waveIngredients;
    //[SerializeField]
    //private Transform spawner;

    public List<WaveIngredient> CurrentIngredients { get { return waveIngredients; } }
    public Wave CurrentWave { get { return this; } }


    private float enemyCooldown = 1;
    private void Start()
    {
        //StartCoroutine(SpawnEnemies(enemyCooldown));
    }
    private void Update()
    {

    }

    //public IEnumerator SpawnEnemies(float spawnCooldown)
    //{

    //    foreach (WaveIngredient waveIngredient in waveIngredients)
    //    {
    //        Debug.Log(waveIngredient.CurrentWaveIngredient.Amount);
    //        Debug.Log(waveIngredient.CurrentWaveIngredient.EnemyPrefab);
    //        for (int i = 0; i < waveIngredient.CurrentWaveIngredient.Amount; i++)
    //        {
    //            yield return new WaitForSeconds(spawnCooldown);
    //            Instantiate(waveIngredient.CurrentWaveIngredient.EnemyPrefab, spawner.position, spawner.rotation);
    //        }
    //    }
    //}
}
