using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject breakableObject;
    [SerializeField] float breakForce;
    [SerializeField] string sourceOfDestruction;
    [SerializeField] int targetStrength = 10;
    [SerializeField] Slider healthBar;

    public event Action stopShootingAtTarget;
    bool objectDestroyed;
    MeshRenderer mesh;
    int targetHealth = 100;
    int hitCounter;

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (objectDestroyed) {
            stopShootingAtTarget?.Invoke();
        }        
    }

    void breakObject()
    {
        GameObject obj = Instantiate(breakableObject, transform.position, transform.rotation);
        foreach (Rigidbody rb in obj.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(transform.up * breakForce);
        }

        Destroy(obj, 3);
        objectDestroyed = true;
        mesh.enabled = false;        
        
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag.Equals(sourceOfDestruction) && !objectDestroyed)
        {
            hitCounter++;
            if (hitCounter >= targetStrength) {
                targetHealth--;
                healthBar.value = targetHealth;
                hitCounter = 0;                
            }
            
            if (targetHealth == 0) {
                breakObject();
            }
            
        }

    }
}
