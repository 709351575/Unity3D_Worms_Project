using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Weapons
{
    Rocket,
    Hameha,
    Plane
}


public class PlayerWeaponControl : MonoBehaviour
{
    public bool Selected;

    public Weapons CurrentWeapon;

    public GameObject Rocket;

    public GameObject plane;

    public GameObject hameha;

    public float PowerAngle;

    public GameObject AimImage;

    public Image PowerImage;

    public GameObject PowerObj;

    public float WeaponForce;

    public float WeaponMaxForce;

    public bool onbutton;

    bool mouseDowning;

    private float PrevPowerAngle;

    private bool CanAttack;

    // Start is called before the first frame update
    // private default
    void Start()
    {
        CanAttack = false;
        Selected = false;
        mouseDowning = false;
        onbutton = false;
        WeaponForce = 0;
        if (Selected && CurrentWeapon == Weapons.Rocket)
            AimImage.SetActive(true);
        else
            AimImage.SetActive(false);
    }
 
    // Update is called once per frame
    void Update()
    {
        // GetButton: true when holds
        // GetButtonDown(Up): only true when down(up)
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && Selected && !onbutton)
            {
                CheckWeaponDown();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (CanAttack && Selected && !onbutton)
            {
                CanAttack = false;
                CheckWeaponUp();
            }
        }
           
        if (Selected)
        {
            
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PowerAngle = Mathf.Atan2(MousePos.y - transform.position.y, MousePos.x - transform.position.x); // hu du zhi
            PowerAngle = PowerAngle * Mathf.Rad2Deg; // Radius to Degree
            if (transform.localScale.x > 0)
            {
                PowerAngle = Mathf.Min(PowerAngle, 60);
                PowerAngle = Mathf.Max(PowerAngle, 0);
                AimImage.transform.rotation = Quaternion.Euler(0, 0, PowerAngle);
            }
            else
            {
                if (PowerAngle < 0)
                    PowerAngle = 180;
                PowerAngle = Mathf.Max(PowerAngle, 120);
                AimImage.transform.rotation = Quaternion.Euler(0, 0, 180 + PowerAngle);
            }
        }

        if (Selected && mouseDowning)
        {
            WeaponForce += 100 * Time.deltaTime; // deltaTime: single frame time
            if (WeaponForce >= WeaponMaxForce)
            {
                WeaponForce = WeaponMaxForce;
            }
            PowerImage.fillAmount = (WeaponForce / WeaponMaxForce);
        }

       
    }

    public void CheckWeaponDown()
    {
        switch (CurrentWeapon)
        {
            case Weapons.Plane:
                if (!mouseDowning)
                    PrevPowerAngle = PowerAngle;
                mouseDowning = true;
                WeaponForce = 0;
                PowerImage.fillAmount = 0;
                AimImage.SetActive(false); // no showing aim image
                if (transform.localScale.x > 0)
                {
                    PowerObj.transform.rotation = Quaternion.Euler(0, 0, PowerAngle);
                }
                else
                {
                    PowerObj.transform.rotation = Quaternion.Euler(0, 0, 180 + PowerAngle);
                }
                this.GetComponent<Platformer2DUserControl>().SetStill();
                
                break;
            case Weapons.Rocket:
                if (!mouseDowning)
                    PrevPowerAngle = PowerAngle;
                mouseDowning = true;
                WeaponForce = 0;
                PowerImage.fillAmount = 0;
                AimImage.SetActive(false); // no showing aim image
                if (transform.localScale.x > 0)
                {
                    PowerObj.transform.rotation = Quaternion.Euler(0, 0, PowerAngle);
                }
                else
                {
                    PowerObj.transform.rotation = Quaternion.Euler(0, 0, 180 + PowerAngle);
                }
                this.GetComponent<Platformer2DUserControl>().SetStill();
                break;
            case Weapons.Hameha:
                this.GetComponent<Platformer2DUserControl>().SetStill();
                break; 
            default:
                break;
        }
    }

    public void CheckWeaponUp()
    {
        switch (CurrentWeapon)
        {
            case Weapons.Plane:
                mouseDowning = false;
                PowerImage.fillAmount = 0;
                // instantiate plane
                float Angle = Mathf.Deg2Rad * PrevPowerAngle;
                float len = Mathf.Sqrt(Mathf.Cos(Angle) * Mathf.Cos(Angle) + Mathf.Sin(Angle) * Mathf.Sin(Angle));
                Vector2 Direction = new Vector2(Mathf.Cos(Angle) / len, Mathf.Sin(Angle) / len); // position 2d
                if (this.transform.localScale.x > 0)
                    plane.transform.localScale = new Vector3(- 1, 1, 1);
                else
                    plane.transform.localScale = new Vector3(1, 1, 1);
                GameObject _plane = Instantiate(plane, this.transform.position, Quaternion.identity, null);
                _plane.GetComponent<Rigidbody2D>().AddForce(Direction * (WeaponForce * 5));
                WeaponForce = 0;
                Camera cm = GameObject.Find("Main Camera").GetComponent<Camera>();
                cm.GetComponent<CameraFollow>().SetTrackingTarget(_plane);
                break;
            case Weapons.Rocket:
                mouseDowning = false;
                PowerImage.fillAmount = 0;
                // instantiate bomb 
                Angle = Mathf.Deg2Rad * PrevPowerAngle;
                len = Mathf.Sqrt(Mathf.Cos(Angle) * Mathf.Cos(Angle) + Mathf.Sin(Angle) * Mathf.Sin(Angle));
                Direction = new Vector2(Mathf.Cos(Angle) / len, Mathf.Sin(Angle) / len); // position 2d
                if (this.transform.localScale.x < 0)
                    Rocket.transform.localScale = new Vector3(-1, 1, 1);
                else
                    Rocket.transform.localScale = new Vector3(1, 1, 1);
                GameObject _Rocket = Instantiate(Rocket, this.transform.position, Quaternion.identity, null);
                _Rocket.GetComponent<Rigidbody2D>().AddForce(Direction * (WeaponForce * 5));
                WeaponForce = 0;
                cm = GameObject.Find("Main Camera").GetComponent<Camera>();
                cm.GetComponent<CameraFollow>().SetTrackingTarget(_Rocket);
                break;
            case Weapons.Hameha:
                GameObject _hameha = Instantiate(hameha, this.transform.position, Quaternion.identity, null);
                if (transform.localScale.x > 0)
                {
                    _hameha.GetComponent<Hameha>().goRight = true;
                    _hameha.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    _hameha.GetComponent<Hameha>().goRight = false;
                    _hameha.transform.localScale = new Vector3(-1, 1, 1);
                }
                cm = GameObject.Find("Main Camera").GetComponent<Camera>();
                cm.GetComponent<CameraFollow>().SetTrackingTarget(_hameha);
                
                break;
            default:
                break;
        }
        
    }

    public void SwitchToWeapon(int num)
    {
        if (Selected)
        {
            switch (num)
            {
                case 0:
                    AimImage.SetActive(false);
                    CurrentWeapon = Weapons.Hameha;
                    Debug.Log("Switch To Hameha");
                    break;
                case 1:
                    AimImage.SetActive(true);
                    CurrentWeapon = Weapons.Rocket;
                    break;
                case 2:
                    AimImage.SetActive(true);
                    CurrentWeapon = Weapons.Plane;
                    Debug.Log("Switch To Plane");
                    break;
                default:
                    break;
            }
        }
    }

    public void SetonButton(bool bb)
    {
        onbutton= bb;
    }

    public void SetSelected(bool bb)
    {
        CanAttack = bb;
        Selected = bb;
        if (bb)
        {
            Camera cm = GameObject.Find("Main Camera").GetComponent<Camera>();
            cm.GetComponent<CameraFollow>().SetTrackingTarget(GameObject.Find(this.name));
            SwitchToWeapon(1);
            this.GetComponent<Platformer2DUserControl>().SetMove();
            if (CurrentWeapon == Weapons.Rocket)
                AimImage.SetActive(true);
        }
    }

}
