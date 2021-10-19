using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    float maxStamina = 100f;
    float staminaDrain = 50f;
    float staminaRegen = 50f;
    public bool Regenerated = true;
    public bool sprinting = false;

    public Image staminaBar = null;
    public CanvasGroup sliderGroup = null;

    public CharacterController Character;

    private void Start()
    {
        Character.GetComponent<CharacterController>();


    }

    void Update()
    {
        if (sprinting == false)
        {
            if(stamina <= maxStamina - 0.1f)
            {
                UpdateStamina(1);
                stamina += staminaRegen * Time.deltaTime;

                if(stamina >= maxStamina)
                {
                    Character.speed = 20;
                    sliderGroup.alpha = 0;
                    Regenerated = true;
                }
            }
            
        }  



    }

    public void Sprinting()
    {
        if (Regenerated)
        {
            sprinting = true;
            stamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if(stamina <= 0)
            {
                Character.speed = 8;
                Regenerated = false;

                sliderGroup.alpha = 0;
            }
        }
    }

    void UpdateStamina(int value)
    {
        staminaBar.fillAmount = stamina / maxStamina;

        if(value == 0)
        {
            sliderGroup.alpha = 0;
        }
        else
        {
            sliderGroup.alpha = 1;
        }

    }


}
