using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TrackZoneParameters
    {
        public float distBtwTracks;
        public int noOfTracks;
        public int trackType;
    }
    
    TrackPooler trackPooler;
    GameObject lastSpawnTrack;
    float distFromLastSpawnTrack;
    bool trackSpawned;    
    
    string[] trackTags = new string[]
    {
        "TrackObject1",
        "TrackObject2",
        "TrackObject3",
        "TrackObject4"
    };

    [SerializeField]
    List<TrackZoneParameters> trackParams;
    
    int[] trackSpawnCounter;

    bool tracksRandomized;


    [SerializeField]
    GameObject orbPrefab;

    List<GameObject> orbs;



    // Start is called before the first frame update
    void Start()
    {
        trackPooler = TrackPooler.Instance;
        trackSpawnCounter = new int[trackParams.Count];
        orbs = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!tracksRandomized)
        {
            setRandomTracks();
        }

        spawnTrackZone();
        
    }
    
    void spawnTrack(float distanceBtw,int noOfTimeItSpawn,int spawnType)
    {
        float distanceBtwTracks = 0f;
        if(trackSpawnCounter[spawnType] == 0)
        {
            distanceBtwTracks = 50f;
        }
        else
        {
            distanceBtwTracks = distanceBtw;
        }
        
        if (trackSpawnCounter[spawnType] < noOfTimeItSpawn)
        {
            if (lastSpawnTrack == null)
            {
                lastSpawnTrack = trackPooler.spawnFromPool(trackTags[spawnType], transform.position, transform.rotation);
                trackSpawned = true;                
                trackSpawnCounter[spawnType]++;
                enableChildObjects(lastSpawnTrack);
                //placeOrbs(lastSpawnTrack);
            }
            else
            {
                distFromLastSpawnTrack = Vector3.Distance(transform.position, lastSpawnTrack.transform.position);
            }

            if (distFromLastSpawnTrack >= distanceBtwTracks && distFromLastSpawnTrack <= distanceBtwTracks + 5 && !trackSpawned)
            {
                lastSpawnTrack = trackPooler.spawnFromPool(trackTags[spawnType], transform.position, transform.rotation);
                trackSpawnCounter[spawnType]++;
                trackSpawned = true;
                enableChildObjects(lastSpawnTrack);
                //placeOrbs(lastSpawnTrack);
            }

            else if (distFromLastSpawnTrack < distanceBtwTracks || distFromLastSpawnTrack > distanceBtwTracks + 5)
            {
                trackSpawned = false;
            }
        }
        
    }

    void spawnTrackZone() {
        
        spawnTrack(trackParams[0].distBtwTracks, trackParams[0].noOfTracks, trackParams[0].trackType);
        
        for(int i = 1; i < trackParams.Count; i++)
        {
            if (trackSpawnCounter[trackParams[i-1].trackType] == trackParams[i-1].noOfTracks)
            {
                spawnTrack(trackParams[i].distBtwTracks, trackParams[i].noOfTracks, trackParams[i].trackType);
            }
        }
        
        if(trackSpawnCounter[trackParams[0].trackType] == trackParams[0].noOfTracks && 
           trackSpawnCounter[trackParams[1].trackType] == trackParams[1].noOfTracks && 
           trackSpawnCounter[trackParams[2].trackType] == trackParams[2].noOfTracks && 
           trackSpawnCounter[trackParams[3].trackType] == trackParams[3].noOfTracks)
        {

            for (int i = 0; i < trackParams.Count; i++)
            {
                trackSpawnCounter[trackParams[i].trackType] = 0;
            }
            
            tracksRandomized = false;
        }        
    }

    void setRandomTracks()
    {
        int x = Random.Range(1, 6);
        if (x == 1)
        {
            trackParams[1].trackType = 3;
            trackParams[2].trackType = 1;
            trackParams[3].trackType = 2;
        }

        else if (x == 2)
        {
            trackParams[1].trackType = 1;
            trackParams[2].trackType = 2;
            trackParams[3].trackType = 3;
        }

        else if (x == 3)
        {
            trackParams[1].trackType = 2;
            trackParams[2].trackType = 1;
            trackParams[3].trackType = 3;
        }

        else if (x == 4)
        {
            trackParams[1].trackType = 2;
            trackParams[2].trackType = 3;
            trackParams[3].trackType = 1;
        }

        else if (x == 5)
        {
            trackParams[1].trackType = 3;
            trackParams[2].trackType = 2;
            trackParams[3].trackType = 1;
        }
        tracksRandomized = true;
    }


    void enableChildObjects(GameObject track) {
        
        for(int i=0;i< track.transform.GetChild(0).childCount; i++)
        {
            track.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(true);
            track.transform.GetChild(0).GetChild(i).GetComponent<Breakable>().objectDestroyed = false;
        }
    }


    void placeOrbs(GameObject track) {


        int trackObjCount = track.transform.GetChild(0).childCount;

        int random1 = Random.Range(0, 3);
        int random2 = Random.Range(3, 6);
        int random3 = Random.Range(6, 9);
        int random4 = Random.Range(9, 14);

        Transform transform1 = track.transform.GetChild(0).GetChild(random1);
        Transform transform2 = track.transform.GetChild(0).GetChild(random2);
        Transform transform3 = track.transform.GetChild(0).GetChild(random3);
        Transform transform4 = track.transform.GetChild(0).GetChild(random4);

        GameObject orb1 = Instantiate(orbPrefab, transform1.position, transform1.rotation);
        GameObject orb2 = Instantiate(orbPrefab, transform2.position, transform2.rotation);
        GameObject orb3 = Instantiate(orbPrefab, transform3.position, transform3.rotation);
        GameObject orb4 = Instantiate(orbPrefab, transform4.position, transform4.rotation);

        //orbs.Add(orb1);
        //orbs.Add(orb2);
        //orbs.Add(orb3);
        //orbs.Add(orb4);

    }





    
}
