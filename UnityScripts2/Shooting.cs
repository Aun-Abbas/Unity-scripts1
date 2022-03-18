using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform spawnPoint1;

    [SerializeField]
    Transform spawnPoint2;

    [SerializeField]
    float bulletSpeed = 0.6f;
    
    [SerializeField]
    float fireRate = 0.06f;

    float nextFire;

    BulletPooler bulletPooler;

    JetController jetController;

    [SerializeField]
    AudioSource gunAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletPooler = BulletPooler.Instance;
        jetController = GetComponent<JetController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (jetController.gunActivated)
        {
            shoot();
            if (!gunAudio.isPlaying)
            {                
                gunAudio.Play();
            }
            
        }
        else
        {
            gunAudio.Stop();
        }
    }

    void shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet1 = bulletPooler.spawnFromPool("BulletObject1", spawnPoint1.position, spawnPoint1.rotation);
            bullet1.GetComponent<Rigidbody>().velocity =(spawnPoint1.transform.forward) * bulletSpeed;
            GameObject bullet2 = bulletPooler.spawnFromPool("BulletObject1", spawnPoint2.position, spawnPoint2.rotation);
            bullet2.GetComponent<Rigidbody>().velocity = (spawnPoint2.transform.forward) * bulletSpeed;            
            StartCoroutine(destroyBullet(bullet1, bullet2));
            
        }


        IEnumerator destroyBullet(GameObject bullet1, GameObject bullet2)
        {
            yield return new WaitForSeconds(1f);
            bulletPooler.returnToPool("BulletObject1", bullet1);
            bulletPooler.returnToPool("BulletObject1", bullet2);
            
        }
        
    }











}
