using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public Image staminaBar = null;
    public CanvasGroup sliderGroup = null;
    public GameObject fpsCamera = null;

    public CharacterController controller;

    public float speed = 8;
    float runSpeed = 20;
    float normalSpeed = 8;
    public float gravity = -9.8f;

    public float stamina = 5f;
    public float maxStamina = 5f;
    public float minStamina = 0f;
    public float staminaDrain = 2;
    public float staminaRegen = 2;
    bool sprinting = false;
    bool regenerated = true;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        fpsCamera.GetComponentInChildren<Camera>().fieldOfView = 60;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        

        Vector3 move = transform.right * x + transform.forward * z;

        gameObject.transform.position += move * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (regenerated == true)
            {
                sprinting = true;
                stamina -= staminaDrain * Time.deltaTime;
                UpdateStamina(1);
                fpsCamera.GetComponentInChildren<Camera>().fieldOfView = 75;
                speed = runSpeed;
                if (stamina <= 0)
                {

                    regenerated = false;
                    sprinting = false;
                    sliderGroup.alpha = 0;
                }
            }
        }
        else
        {
            sprinting = false;
        }

        
        
        if (sprinting == false)
        {
            speed = normalSpeed;
            if (stamina <= maxStamina)
            {
                
                UpdateStamina(1);
                stamina += staminaRegen * Time.deltaTime;

                if (stamina >= maxStamina)
                {
                    regenerated = true;
                    sliderGroup.alpha = 0;
                    
                }
            }

        }


    }

    //public void Sprinting()
    //{
    //    if (regenerated == true)
    //    {
    //        sprinting = true;
    //        stamina -= staminaDrain * Time.deltaTime;
    //        UpdateStamina(1);
    //        fpsCamera.GetComponentInChildren<Camera>().fieldOfView = 75;
    //        speed = runSpeed;
    //        if (stamina <= 0)
    //        {
                
    //            regenerated = false;
    //            sprinting = false;
    //            sliderGroup.alpha = 0;
    //        }
    //    }
    //}

    void UpdateStamina(int value)
    {
        staminaBar.fillAmount = stamina / maxStamina;

        if (value == 0)
        {
            sliderGroup.alpha = 0;
        }
        else
        {
            sliderGroup.alpha = 1;
        }

    }


    

}
