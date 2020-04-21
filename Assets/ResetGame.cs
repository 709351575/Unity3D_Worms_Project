using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public Camera MainCamera;

    public GameObject ground1, ground2;

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
        Debug.Log("重启游戏！！！！");
        GameObject gameObject = GameObject.Find("Player1");
        gameObject.GetComponent<PlayerHealthControl>().Respawn();
        gameObject = GameObject.Find("Player2");
        gameObject.GetComponent<PlayerHealthControl>().Respawn();
        ground1.GetComponent<DestructibleSprite>().ReConstruct();
        ground2.GetComponent<DestructibleSprite>().ReConstruct();
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
