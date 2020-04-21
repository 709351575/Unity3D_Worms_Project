using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("开始游戏！！！！");
        StartCoroutine(SetOnButtonFalse());
    }

    IEnumerator SetOnButtonFalse()
    {
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Player1").GetComponent<PlayerWeaponControl>().SetonButton(false);
        GameObject.Find("Player2").GetComponent<PlayerWeaponControl>().SetonButton(false);
        this.gameObject.SetActive(false);
    }
}
