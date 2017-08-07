using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
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
    [SerializeField]
    private GameObject path;
    #endregion

    #region Public Properties
    public GameObject Path { get { return path; } }
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        StartCoroutine(SpawnWave(nextWaveCooldown));
    }
    #endregion

    #region Private Methods
    private IEnumerator SpawnWave(float time)
    {
        yield return new WaitForSeconds(startSpawningTime);
        foreach (Wave wave in waves)
        {
            yield return SpawnEnemies(spawnCooldown, wave.CurrentWave.CurrentIngredients);
            yield return new WaitForSeconds(nextWaveCooldown);
        }
        GameManager.Instance.EndGameAvaible = true;
    }

    private IEnumerator SpawnEnemies(float spawnCooldown, List<WaveIngredient> waveIngredients)
    {
        foreach (WaveIngredient waveIngredient in waveIngredients)
        {
            for (int i = 0; i < waveIngredient.CurrentWaveIngredient.Amount; i++)
            {
                Instantiate(waveIngredient.CurrentWaveIngredient.EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
                GameManager.Instance.AddSpawnedEnemy();
                yield return new WaitForSeconds(spawnCooldown);
            }
        }
    }
    #endregion
}
