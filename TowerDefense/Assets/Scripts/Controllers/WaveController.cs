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
    //[SerializeField]
    //private List<Wave> waves;
    [SerializeField]
    private Wave[] waves;
    #endregion

    #region Private Variables
    private bool SpawnNextEnemy = true;
    private bool SpawnNextWave = true;
    private GameObject enemyToSpawn;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        //waves = new List<Wave>();
        //waves.Add(new Wave());
    }

    private void Update()
    {
        Debug.Log(waves.Length);
        StartCoroutine( SpawnWave(3, true));
    }
    #endregion

    #region Private Methods
    private IEnumerator SpawnWave(float time, bool spawnCondition)
    {
        yield return new WaitForSeconds(startSpawningTime);
        foreach (Wave w in waves)
        {
            Debug.Log(w.CurrentWave.Length);
            foreach (WaveIngredient wi in w.CurrentWave)
            {
                Debug.Log(wi.EnemyPrefab.name);
            //    for(int i = 0; i < wi.Amount; i++)
            //    {
            //        Instantiate(wi.EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            //        yield return new WaitForSeconds(spawnCooldown);
            //    }
                
            }
            //yield return new WaitForSeconds(nextWaveCooldown);
        }

        //spawnCondition = false;
        //yield return new WaitForSeconds(time);
        //spawnCondition = true;
    }

    #endregion
}
