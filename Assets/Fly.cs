using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
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
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");
        bool isplayer1 = player1.GetComponent<PlayerWeaponControl>().Selected;
        if (isplayer1)
        {
            Debug.Log("Player 1 Gonna Fly");
            player1.GetComponent<PlayerWeaponControl>().SwitchToWeapon(2);
        }
        else
        {
            Debug.Log("Player 2 Gonna Fly");
            player2.GetComponent<PlayerWeaponControl>().SwitchToWeapon(2);
        }
    }
}
