using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDestroyer : MonoBehaviour
{
    TrackPooler trackPooler;
    string[] TrackObjTags = new string[]
    {
        "TrackObject1",
        "TrackObject2",
        "TrackObject3",
        "TrackObject4"
    };

    // Start is called before the first frame update
    void Start()
    {
        trackPooler = TrackPooler.Instance;
    }

    private void OnTriggerExit(Collider obj)
    {
        for (int i = 0; i < TrackObjTags.Length; i++) {
            if (obj.tag.Equals(TrackObjTags[i]))
            {
                
                /*
                for (int j = 0; j < obj.transform.childCount; j++)
                {
                    ShapeKeysManager shapeKeysManager = obj.transform.GetChild(j).GetComponent<ShapeKeysManager>();
                    shapeKeysManager.resetAllShapeKeys();
                }
                */
                
               trackPooler.returnToPool(obj.tag, obj.gameObject);                
            }
        }
        
        
    }

}
