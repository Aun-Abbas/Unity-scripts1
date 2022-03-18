using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    Light light1;

    [SerializeField]
    Light light2;

    [SerializeField]
    Light light3;

    [SerializeField]
    GameObject imagesource;
    
    //Light light;
    Image image;

    [SerializeField]
    Material jetmaterial;
    //GameObject jetObject;

    [SerializeField]
    Slider slider1;

    [SerializeField]
    Slider slider2;

    [SerializeField]
    Slider slider3;

    [SerializeField]
    Slider slider4;

    [SerializeField]
    Slider slider5;

    [SerializeField]
    Slider slider6;



    // Start is called before the first frame update
    void Start()
    {
        //light = lightSource.GetComponent<Light>();
        image = imagesource.GetComponent<Image>();

        //jetmaterial=jetObject.GetComponent<> 

    }

    // Update is called once per frame
    void Update()
    {
        image.color = light1.color;

        jetmaterial.SetFloat("_Metallic", slider1.value);
        jetmaterial.SetFloat("_Smoothness", slider2.value);
        //jetmaterial.SetColor("_Color",);

        light1.color = new Color(slider3.value, slider4.value, slider5.value, 1);
        light2.color = new Color(slider3.value, slider4.value, slider5.value, 1);
        light3.color = new Color(slider3.value, slider4.value, slider5.value, 1);

        light1.intensity = slider6.value;
        light2.intensity = slider6.value;
        light3.intensity = slider6.value;

        image.color = light1.color;

        slider1.transform.GetChild(3).GetComponent<Text>().text = slider1.value.ToString();
        slider2.transform.GetChild(3).GetComponent<Text>().text = slider2.value.ToString();
        slider3.transform.GetChild(3).GetComponent<Text>().text = slider3.value.ToString();
        slider4.transform.GetChild(3).GetComponent<Text>().text = slider4.value.ToString();
        slider5.transform.GetChild(3).GetComponent<Text>().text = slider5.value.ToString();
        slider6.transform.GetChild(3).GetComponent<Text>().text = slider6.value.ToString();




    }
}
