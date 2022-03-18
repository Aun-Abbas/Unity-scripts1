using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 0.6f;
    [SerializeField] float fireRate = 0.06f;
    [SerializeField] GameObject leftBulletSpawner;
    [SerializeField] GameObject rightBulletSpawner;
    
    float nextFire;
    ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            shoot();
        }        
    }
    
    void shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            GameObject bullet1 = objectPooler.spawnFromPool("PlayerBullet",leftBulletSpawner.transform.position,leftBulletSpawner.transform.rotation);
            GameObject bullet2 = objectPooler.spawnFromPool("PlayerBullet",rightBulletSpawner.transform.position, rightBulletSpawner.transform.rotation);

            bullet1.GetComponent<Rigidbody>().velocity = (leftBulletSpawner.transform.forward) * bulletSpeed;
            bullet2.GetComponent<Rigidbody>().velocity = (rightBulletSpawner.transform.forward) * bulletSpeed;

            StartCoroutine(destroyBullet(bullet1, bullet2));
        }
        
    }

    IEnumerator destroyBullet(GameObject bullet1, GameObject bullet2)
    {
        yield return new WaitForSeconds(1f);
        objectPooler.returnToPool("PlayerBullet", bullet1);
        objectPooler.returnToPool("PlayerBullet", bullet2);

    }
}
