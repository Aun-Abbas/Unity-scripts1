using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JetController : MonoBehaviour
{
    [SerializeField]
    private float normalSpeedAlongZAxix=1f;

    [SerializeField]
    private float boostAlongZAxix=3f;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float xLimit=5f;

    [SerializeField]
    private float yLimit=5f;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    [Range(-1, 1)]
    private float xInputLimitForJetFire;

    [SerializeField]
    [Range(-1, 1)]
    private float yInputLimitForJetFire;

    [SerializeField]
    private List<JetFireControls> fireControls;
    
    GameObject jet;
    Quaternion jetInitialRot;
    Vector3 jetInitialPose;
    bool jetEngineBoosted;

    float speed;

    // List<GameObject> jetTrails;

    //ParticleSystem.ForceOverLifetimeModule forceModule;

    Vector3 CamPose;
    float CamY_Offset;
    float CamX_Offset;

    Camera cam;
    float fieldOfViewValue;
    public bool gunActivated;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CamPose = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        //jetTrails = new List<GameObject>();
        jet = GameObject.FindGameObjectWithTag("Jet");
        jetInitialRot = jet.transform.GetChild(0).rotation;
        jetInitialPose = jet.transform.position;
        //jetTrails.AddRange( GameObject.FindGameObjectsWithTag("JetTrail"));

        //Debug.Log(jetInitialRot);

        CamY_Offset = CamPose.y - jet.transform.position.y;
        CamX_Offset = CamPose.x - jet.transform.position.x;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            speed = normalSpeedAlongZAxix;
            if (fieldOfViewValue <= 1)
            {
                cam.fieldOfView = Mathf.Lerp(45f, 60f, fieldOfViewValue);
                fieldOfViewValue += 0.05f;
            }

            gunActivated = false;
            
        }
        else
        {
            speed = boostAlongZAxix;
            if (fieldOfViewValue >= 0)
            {
                cam.fieldOfView = Mathf.Lerp(45f, 60f, fieldOfViewValue);
                fieldOfViewValue -= 0.05f;
            }

            gunActivated = true;
        }
        
        float x_movement = joystick.Horizontal;
        float y_movement = joystick.Vertical;

        transform.Translate(transform.forward * Time.deltaTime * speed);

        if(jet.transform.position.x <=  xLimit &&
           jet.transform.position.x >= -xLimit &&
           jet.transform.position.y <=  yLimit &&
           jet.transform.position.y >= -yLimit)
        {
            jet.transform.Translate(x_movement * movementSpeed * Time.deltaTime,
                                y_movement * movementSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(jet.transform.position.x > xLimit)
            {
                jet.transform.position = new Vector3(jet.transform.position.x-0.01f,
                                  jet.transform.position.y, jet.transform.position.z);
            }
            else if(jet.transform.position.x < -xLimit)
            {
                jet.transform.position = new Vector3(jet.transform.position.x + 0.01f,
                                  jet.transform.position.y, jet.transform.position.z);
            }

            if (jet.transform.position.y > yLimit)
            {
                jet.transform.position = new Vector3(jet.transform.position.x,
                                  jet.transform.position.y - 0.01f, jet.transform.position.z);
            }
            else if (jet.transform.position.y < -yLimit)
            {
                jet.transform.position = new Vector3(jet.transform.position.x,
                                  jet.transform.position.y + 0.01f, jet.transform.position.z);
            }

        }

        Vector3 jetPose = jet.transform.localPosition;
        jet.transform.localPosition = new Vector3(jetPose.x, jetPose.y, jetInitialPose.z);
        float tiltAngle = -x_movement * rotationSpeed * Time.deltaTime;
        Quaternion jetRot = jet.transform.GetChild(0).localRotation;
        jet.transform.GetChild(0).localRotation = new Quaternion(jetInitialRot.x, jetInitialRot.y, jetRot.z + tiltAngle, 1.0f);        
        controlEngineFire(x_movement,y_movement);        

    }
    
    void controlEngineFire(float x, float y) {

        if (x == 0 || y == 0)
        {
            for (int i = 0; i < fireControls.Count; i++) {

                if (jetEngineBoosted)
                {
                    if (i == 4 || i == 5)
                    {
                        fireControls[i].setPropellerSpeed(true);
                    }
                    else
                    {
                        fireControls[i].setPropellerSpeed(false);
                    }
                }
                else {
                    fireControls[i].setPropellerSpeed(false);
                }
                
            }    
        }
        else
        {
            fireControls[0].setPropellerSpeed(true);
            fireControls[1].setPropellerSpeed(true);
            fireControls[4].setPropellerSpeed(false);
            fireControls[5].setPropellerSpeed(false);
            jetEngineBoosted = true;
        }

        if (x < -xInputLimitForJetFire)
        {
            fireControls[2].setPropellerSpeed(true);
            fireControls[3].setPropellerSpeed(false);
        }
        else if (x > xInputLimitForJetFire)
        {           
            fireControls[3].setPropellerSpeed(true);
            fireControls[2].setPropellerSpeed(false);
        }

        if (y < -yInputLimitForJetFire) {  

            fireControls[6].setPropellerSpeed(true);
            fireControls[7].setPropellerSpeed(true);
            fireControls[8].setPropellerSpeed(false);
            fireControls[9].setPropellerSpeed(false);
        }
        else if (y > yInputLimitForJetFire) {

            fireControls[6].setPropellerSpeed(false);
            fireControls[7].setPropellerSpeed(false);
            fireControls[8].setPropellerSpeed(true);
            fireControls[9].setPropellerSpeed(true);
        }
        
    }
}    

[System.Serializable]
public struct JetFireControls
{
    public GameObject propellerObj;
    public float minSpeed;
    public float maxSpeed;
    public bool hasVariableSimulationSpeed;
    ParticleSystem propeller;
    ParticleSystem.MainModule mainModule;
    bool boostfire;
    bool objInitialized;


    void initializer()
    {
        propeller = propellerObj.GetComponent<ParticleSystem>();
        mainModule = propeller.main;
        objInitialized = true;
    }

    public void setPropellerSpeed(bool boost)
    {
        if (!objInitialized) {
            initializer();
        }        
        boostfire = boost;
        if (!boostfire)
        {
            mainModule.startLifetime = Mathf.Lerp(0, minSpeed, 1f);
        }
        else {
            mainModule.startLifetime = Mathf.Lerp(0, maxSpeed, 1f);
        }

        if (hasVariableSimulationSpeed)
        {
            mainModule.simulationSpeed = mainModule.startLifetime.constant * 5;
        }
        else {
            mainModule.simulationSpeed = 15;
        }        
    } 
   
}

