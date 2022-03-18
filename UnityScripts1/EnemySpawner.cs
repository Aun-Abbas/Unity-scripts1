using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTimeGap=2f;
    [SerializeField] int noOfSpawns=5;

    public event Action clearAllEnemies;
    List<Transform> spawnPoints;
    ObjectPooler objectPooler;
    GameManager gameManager;
    bool enemySpawned;
    int spawnCounter;
    float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.spawnEnemy += startSpawningProcess;
        objectPooler = ObjectPooler.Instance;
        spawnPoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++) {
            spawnPoints.Add(transform.GetChild(i));
        }        
    }
    
    void spawnEnemy() {
        
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)];
        GameObject enemy = objectPooler.spawnFromPool("EnemyObject1", spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<NavMeshAgent>().enabled = true;
    }

    public void startSpawningProcess()
    {
        if (spawnCounter < noOfSpawns)
        {

            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnTimeGap;
                spawnEnemy();
                spawnCounter++;
            }
        }

        else if (gameManager.kills == noOfSpawns) {
             StartCoroutine(waitTillNextSpawnProcess());
        }
    }

    IEnumerator waitTillNextSpawnProcess() {
        yield return new WaitForSeconds(1f);

        clearAllEnemies?.Invoke();
        spawnCounter = 0;
        gameManager.kills = 0;

    }
}
