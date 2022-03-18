using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breakable : MonoBehaviour
{

    [SerializeField]
    GameObject breakableObject;

    [SerializeField]
    float breakForce;
    
    Transform breakableParent;
    
    Text scoreText;

    GameManager gameManager;

    public bool objectDestroyed;

    GameObject objChild;
    private void Start()
    {

        gameManager = GameManager.instance;
        scoreText = GameObject.Find("ScoreNumbers").GetComponent<Text>();
        objChild = gameObject.transform.GetChild(0).gameObject;
        breakableParent = GameObject.FindGameObjectWithTag("Breakables").transform;
    }

    void breakObject()
    {
        GameObject obj = Instantiate(breakableObject, transform.position, transform.rotation);
        //obj.transform.localScale = transform.localScale;
        foreach(Rigidbody rb in obj.GetComponentsInChildren<Rigidbody>())
        {
            //Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(transform.forward*breakForce);
        }

        gameManager.score++;
        scoreText.text = gameManager.score.ToString();
        Destroy(obj, 2);
        objectDestroyed = true;
        objChild.SetActive(false);

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Bullet") && !objectDestroyed)
        {
            breakObject();
        }
    }


}
