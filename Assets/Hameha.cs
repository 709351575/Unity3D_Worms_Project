 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hameha : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;

    public int radius;

    public bool goRight;

    private bool CanCollide;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        CanCollide = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            rb.MovePosition(rb.position + Vector2.right * speed * Time.deltaTime); // move right
        }
        else
        {
            rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime); // move left
            // scale -1
        }

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

    public void OnCollisionEnter2D(Collision2D collision) // collision: something collided
    {
        if (!CanCollide)
            return;
        CanCollide = false;

        if (collision.gameObject.tag == "Des") // tag: GameObject Classification
        {
            Debug.Log("Collide Ground");
            DestroyMyself();
            return;
        }

        if (collision.gameObject.tag == "Player1")
        {
            Debug.Log("Collide Player 1");
            collision.gameObject.GetComponent<PlayerHealthControl>().ChangeHealth(-damage);
            DestroyMyself();
            return;
        }

        if (collision.gameObject.tag == "Player2")
        {
            Debug.Log("Collide Player 2");
            collision.gameObject.GetComponent<PlayerHealthControl>().ChangeHealth(-damage);
            DestroyMyself();
            return;
        }
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
