using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    public bool IsMoving = false;
    private float stamina;
    public int maxstamina = 200;
    public Image StaminaImage;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Start()
    {
        StaminaImage.fillAmount = 0;
    }
    private void Update()
    {

    }


    private void FixedUpdate()
    {
        if (IsMoving)
        {
            if (stamina <= 0)
            {
                IsMoving = false;
                m_Character.Move(0, true, false);
            }  
            else
            {
                // Read the inputs.
                float h = Input.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, true, false);
                if (h != 0)
                {
                    stamina -= 1;
                }
            }
            StaminaImage.fillAmount = stamina / maxstamina;
        }
    }

    public void SetMove()
    {
        IsMoving = true;
        stamina = maxstamina;
        StaminaImage.fillAmount = 1;
    }

    public void SetStill()
    {
        IsMoving = false;
        stamina = 0;
        m_Character.Move(0, true, false);
        StaminaImage.fillAmount = 0;
    }
}
