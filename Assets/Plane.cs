using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private bool CanCollide;

    // Start is called before the first frame update
    void Start()
    {
        CanCollide = true;
    }

    // Update is called once per frame
    void Update()
    {
        float xx = transform.position.x;
        float yy = transform.position.y;
        if (xx < -20 || xx > 20)
        {
            DestroyMyself();
            return;
        }

        if (yy < -20)
        {
            DestroyMyself();
            return;
        }
    }

    IEnumerator SetCanCollide()
    {
        yield return new WaitForSeconds(0.2f);
        CanCollide = true;
    }

    public void OnCollisionEnter2D(Collision2D collision) // collision: something collided
    {
        if (!CanCollide)
            return;
        CanCollide = false;
        StartCoroutine(SetCanCollide());
        if (collision.gameObject.tag == "Des") // tag: GameObject Classification
        {
            Debug.Log("Plane Collide Ground");
            ChangePos(collision.contacts[0].point);
            DestroyMyself();
            return;
        }

        if (collision.gameObject.tag == "Player1")
        {
            Debug.Log("Plane Collide Player1");
            DestroyMyself();
            return;
        }

        if (collision.gameObject.tag == "Player2")
        {
            Debug.Log("Plane Collide Player2");
            DestroyMyself();
            return;
        }
    }

    public void ChangePos(Vector2 Pos)
    {
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");
        bool isplayer1 = player1.GetComponent<PlayerWeaponControl>().Selected;
        if (isplayer1)
            player1.transform.position = new Vector3 (Pos.x, Pos.y + 0.5f, player1.transform.position.z);
        else
            player2.transform.position = new Vector3 (Pos.x, Pos.y + 0.5f, player2.transform.position.z);
    }

    public void DestroyMyself()
    {
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");
        bool isplayer1 = player1.GetComponent<PlayerWeaponControl>().Selected;
        if (player2.GetComponent<PlayerHealthControl>().CurrentHealth > 0 && player1.GetComponent<PlayerHealthControl>().CurrentHealth > 0)
        {
            if (isplayer1)
            {
                Debug.Log("Switch To Player 2");
                player1.GetComponent<PlayerWeaponControl>().SetSelected(false);
                player2.GetComponent<PlayerWeaponControl>().SetSelected(true);
            }
            else
            {
                Debug.Log("Switch To Player 1");
                player2.GetComponent<PlayerWeaponControl>().SetSelected(false);
                player1.GetComponent<PlayerWeaponControl>().SetSelected(true);
            }
        }
        Destroy(gameObject);
    }
}
