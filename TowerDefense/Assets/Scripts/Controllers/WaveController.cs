using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    #region Serialize Fields
    [SerializeField]
    private float spawnCooldown;
    [SerializeField]
    private float nextWaveCooldown;
    [SerializeField]
    private float startSpawningTime;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private List<Wave> waves;
    //[SerializeField]
    //private Wave[] waves;
    #endregion

    #region Private Variables
    private bool SpawnNextEnemy = true;
    private bool SpawnNextWave = true;
    private GameObject enemyToSpawn;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        StartCoroutine(SpawnWave(nextWaveCooldown));
    }

    private void Update()
    {
        Debug.Log(SpawnNextWave);

    }
    #endregion

    #region Private Methods
    private IEnumerator SpawnWave(float time)
    {
        int i = 0;
        StopCoroutine("SpawnEnemies");
        if (SpawnNextWave)
        {
            foreach (Wave wave in waves)
            {
                Debug.Log(i++);
                yield return new WaitForSeconds(time);
                StartCoroutine(SpawnEnemies(spawnCooldown, wave.CurrentWave.CurrentIngredients));


            }
        }
    }

    public IEnumerator SpawnEnemies(float spawnCooldown, List<WaveIngredient> waveIngredients)
    {
        SpawnNextWave = false;
        foreach (WaveIngredient waveIngredient in waveIngredients)
        {
            //Debug.Log(waveIngredient.CurrentWaveIngredient.Amount);
            //Debug.Log(waveIngredient.CurrentWaveIngredient.EnemyPrefab);
            for (int i = 0; i < waveIngredient.CurrentWaveIngredient.Amount; i++)
            {
                yield return new WaitForSeconds(spawnCooldown);
                Instantiate(waveIngredient.CurrentWaveIngredient.EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        SpawnNextWave = true;
    }

    #endregion
}
