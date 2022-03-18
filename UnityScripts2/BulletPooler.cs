using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {

        public enum BulletObjTags
        {
            BulletObject1,
            BulletObject2,
            BulletObject3,
            BulletObject4
        }

        public BulletObjTags tag;
        public GameObject prefab;
        public int size;

    }

    #region Singleton

    public static BulletPooler Instance;
    public void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField]
    Transform bulletParent;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, bulletParent);                
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag.ToString(), objectPool);

        }

    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            
            return null;
        }

        GameObject bulletObject = poolDictionary[tag].Dequeue();
        bulletObject.SetActive(true);
        bulletObject.transform.position = position;
        bulletObject.transform.rotation = rotation;

        return bulletObject;
    }

    public void returnToPool(string tag, GameObject trackObject)
    {

        trackObject.SetActive(false);
        poolDictionary[tag].Enqueue(trackObject);

    }

}
