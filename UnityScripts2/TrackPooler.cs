using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool {

        public enum TrackObjTags
        {
            TrackObject1,
            TrackObject2,
            TrackObject3,
            TrackObject4
        }
        
        public TrackObjTags tag;
        public GameObject prefab;
        public int size;

    }

    #region Singleton

    public static TrackPooler Instance;
    public void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField]
    Transform trackParent;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
   
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {

                GameObject obj = Instantiate(pool.prefab,trackParent);


                if (obj.tag.Equals("TrackObject1") || obj.tag.Equals("TrackObject3") || obj.tag.Equals("TrackObject4"))
                {
                    for (int j = 0; j < obj.transform.GetChild(0).childCount; j++)
                    {
                        GameObject obstacle = obj.transform.GetChild(0).GetChild(j).gameObject;
                        obstacle.transform.rotation = Random.rotation;

                        if (obj.tag.Equals("TrackObject1"))
                        {
                            float size = Random.Range(1, 3);
                            obstacle.transform.localScale = new Vector3(size, size, size);
                            obstacle.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(Random.Range(0, 5), 100);
                        }
                    }
                }

                /*
                if (obj.tag.Equals("TrackObject4")) {

                    for (int j = 0; j < obj.transform.childCount; j++) {
                        obj.transform.GetChild(j).transform.rotation = Random.rotation;
                        float size = Random.Range(15,25);
                        obj.transform.GetChild(j).transform.localScale =new Vector3(size,size,size);
                    }
                }
                */
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag.ToString(),objectPool);

        }

    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation) {

        if (!poolDictionary.ContainsKey(tag)) {

            return null;
        }
        
        GameObject trackObject = poolDictionary[tag].Dequeue();
        trackObject.SetActive(true);
        trackObject.transform.position=position;
        trackObject.transform.rotation=rotation;
        
        return trackObject;
    }

    public void returnToPool(string tag, GameObject trackObject) {

        trackObject.SetActive(false);
        poolDictionary[tag].Enqueue(trackObject);
        
    }
    
}
