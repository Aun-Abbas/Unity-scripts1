using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float sensitivity=10f;
    [SerializeField] float viewScopeX;
    [SerializeField] float viewScopeY;

    float xRotation, yRotation;    

    private void Start()
    {
        
    }

    void Update()
    {
        float inputx = joystick.Horizontal * sensitivity * Time.deltaTime;
        float inputy = joystick.Vertical * sensitivity * Time.deltaTime;

        xRotation -= inputy;
        xRotation = Mathf.Clamp(xRotation,-viewScopeX,viewScopeX);

        yRotation -= inputx;
        yRotation = Mathf.Clamp(yRotation, -viewScopeY, viewScopeY);

        transform.localRotation = Quaternion.Euler(xRotation,0,-yRotation);
    }
}
