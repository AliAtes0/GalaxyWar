using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy_1_Prefab;
    [SerializeField]
    private GameObject _enemy_2_Prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;
    private float _time = 0;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    

    // Update is called once per frame
    void Update()
    {
        _time+= Time.deltaTime;
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (!_stopSpawning) //player is not equal to false
        {
            //Spawning the enemy
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
            if(_time<=60f)
                Instantiate(_enemy_1_Prefab, posToSpawn, Quaternion.identity, _enemyContainer.transform);
            else if(_time>60f)
                Instantiate(_enemy_2_Prefab, posToSpawn, Quaternion.identity, _enemyContainer.transform);
            float x = Random.Range(1.5f, 2.5f);
            yield return new WaitForSeconds(x);

        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(9f, 15f));
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
            int x = Random.Range(0, 4);
            
            Instantiate(powerups[x], posToSpawn, Quaternion.identity);
            
        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
