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

    public float speed = 5;
    public float runSpeed = 10;
    public float normalSpeed = 8;
    public float gravity = -9.8f;

    public float stamina = 5f;
    public float maxStamina = 5f;
    public float minStamina = 0f;
    public float staminaDrain = 2;
    public float staminaRegen = 2;
    bool sprinting = false;
    bool regenerated = true;

    AudioSource aSource = null;

    bool IsMoveing;
    
    float walkStepLength = 0.75f;
    float runStepLength = 0.25f;

    public AudioClip[] aClips = null;

    

    public float timerMax = 1f;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        timer = runStepLength;
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
                
                PlayRunning();
                stamina -= staminaDrain * Time.deltaTime;
                UpdateStamina(1);
                fpsCamera.GetComponentInChildren<Camera>().fieldOfView = 65;
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

        

        if (move != Vector3.zero && sprinting == false)
        {
            IsMoveing = true;
        }
        else
        {
            IsMoveing = false;
        }



        if (IsMoveing == true)
        {
            
            PlayWalking();
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

    void PlayWalking()
    {
        int aIndex = Random.Range(0, aClips.Length);

        
        
        timer -= Time.deltaTime;
        if(timer < 0)
        {

            timer = walkStepLength;
            aSource.clip = aClips[aIndex];

            PlayFootSteps(aClips[aIndex]);
        }
        

        
    }


    void PlayRunning()
    {
        int aIndex = Random.Range(0, aClips.Length);

        if (sprinting == true)
        {
            timer = runStepLength;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {

            timer = runStepLength;
            aSource.clip = aClips[aIndex];

            PlayFootSteps(aClips[aIndex]);
        }
    }
    
    void PlayFootSteps(AudioClip clip)
    {
        aSource.PlayOneShot(clip);
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
