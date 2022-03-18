using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject breakableObject;
    [SerializeField] float breakForce;
    [SerializeField] string sourceOfDestruction;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] int targetStrength = 5;
    [SerializeField] Slider healthBar;

    bool objectDestroyed;    
    ObjectPooler objectPooler;
    GameManager gameManager;
    EnemySpawner enemySpawner;

    int targetHealth = 100;
    int hitCounter;

    public event Action stopShootingBullets;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        gameManager = GameManager.instance;
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        enemySpawner.clearAllEnemies += destroyObjectCompletely;
    }

    void breakObject()
    {
        GameObject obj = Instantiate(breakableObject, transform.position, transform.rotation);
        foreach (Rigidbody rb in obj.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(transform.forward * breakForce);
        }

        Destroy(obj, 3);
        objectDestroyed = true;

        mesh.enabled = false;
        gameManager.kills++;
        stopShootingBullets?.Invoke();        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals(sourceOfDestruction) && !objectDestroyed)
        {
            hitCounter++;
            if (hitCounter >= targetStrength)
            {
                targetHealth--;
                healthBar.value = targetHealth;
                hitCounter = 0;
            }

            if (targetHealth == 0)
            {
                breakObject();
            }
        }
    }

    void destroyObjectCompletely() {

        mesh.enabled = true;
        objectDestroyed = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        objectPooler.returnToPool("EnemyObject1",gameObject);   
    }
    
}
