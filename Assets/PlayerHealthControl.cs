using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthControl : MonoBehaviour
{
    public float HealthMax;

    public float CurrentHealth;

    public Image HealthImage;

    public Button Restart;

    private Vector3 InitPosition;

    private Vector3 InitLocal;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = HealthMax;
        InitPosition = transform.position;
        InitLocal = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        CurrentHealth = HealthMax;
        transform.position = InitPosition;
        transform.localScale = InitLocal;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        HealthImage.fillAmount = 1;
        this.GetComponent<Animator>().SetBool("isDie", false);
        this.GetComponent<PlatformerCharacter2D>().CheckFacingRight();
    }

    public void ChangeHealth(float delta)
    {
        if (CurrentHealth + delta > HealthMax)
            CurrentHealth = HealthMax;
        else
            CurrentHealth += delta;
        StartCoroutine(SetHurt());
        StartCoroutine(UpdateHealthBar());


        if (CurrentHealth <= 0)
        {
            Debug.Log(this.name + "GG");
            this.GetComponent<Animator>().SetBool("isDie", true);
            Camera cm = GameObject.Find("Main Camera").GetComponent<Camera>();
            Restart.gameObject.SetActive(true);
            cm.GetComponent<CameraFollow>().SetTrackingTarget(GameObject.Find("Button_Restart"));
            Button button;
            button = GameObject.Find("Button_Hameha").GetComponent<Button>();
            button.gameObject.SetActive(false);
            button = GameObject.Find("Button_Fly").GetComponent<Button>();
            button.gameObject.SetActive(false);
            GameObject.Find("Player1").GetComponent<PlayerWeaponControl>().Selected = false;
            GameObject.Find("Player2").GetComponent<PlayerWeaponControl>().Selected = false;
        }

    }
    IEnumerator SetHurt()
    {
        this.GetComponent<Animator>().SetBool("Gethurt", true);
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<Animator>().SetBool("Gethurt", false);
    }

    IEnumerator UpdateHealthBar()
    {
        float TargetFillAmount = Mathf.Max(CurrentHealth / HealthMax, 0);
        while (HealthImage.fillAmount > TargetFillAmount && CurrentHealth < HealthMax)
        {
            yield return new WaitForSeconds(0.02f);
            HealthImage.fillAmount = Mathf.Max(TargetFillAmount, HealthImage.fillAmount - 0.01f);
        }

    }
}
