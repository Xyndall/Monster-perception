using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour
{
    Light Lightcolor;

    public bool monstermove = false;
    bool lightswitch = false;

    float timewhitelight = 5;
    float timeredlight = 10;





    // Start is called before the first frame update
    void Start()
    {
       Lightcolor = gameObject.GetComponent<Light>();
     }

    // Update is called once per frame
    void Update()
    {
        if (timewhitelight <= 0)
        {
            lightswitch = true;
        }

        if (timeredlight <= 0)
        {
            lightswitch = false;
        }

        if (lightswitch == false)
        {
            timewhitelight -= Time.deltaTime;
            timeredlight = 10;
            Lightcolor.color = new Color(1, 1, 1, 1);
            //Debug.Log("White light timer " + timewhitelight);
            monstermove = false;
        }

        if(lightswitch == true)
        {
            timeredlight -= Time.deltaTime;
            timewhitelight = 5;
            //Debug.Log(timeredlight + "Red Light");
            Lightcolor.color = new Color(1, 0, 0, 1);
            //Debug.Log("Red light timer " + timeredlight);
            monstermove = true;
        }

    }
}
