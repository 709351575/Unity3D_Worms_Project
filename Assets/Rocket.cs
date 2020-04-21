using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;

    public int radius;

    private bool CanCollide, ReAdjust;

    private Vector3 CollidePos;

    private Quaternion CollideRot;

    // Start is called before the first frame update
    void Start()
    {
        CanCollide = true;
        ReAdjust = false;
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

        if (ReAdjust)
        {
            this.transform.position = CollidePos;
            this.transform.rotation = CollideRot;
        }
    }

    IEnumerator DamageGround(Vector2 point, int radius)
    {
        yield return new WaitForSeconds(0.8f);
        this.GetComponent<Animator>().SetBool("Exploding", false);
        GameObject obj = GameObject.Find("Hills_1");
        obj.GetComponent<DestructibleSprite>().ApplyDamage(point, radius);
        obj = GameObject.Find("Hills_2");
        obj.GetComponent<DestructibleSprite>().ApplyDamage(point, radius);
    }

    public void OnCollisionEnter2D(Collision2D collision) // collision: something collided
    {
        if (!CanCollide)
            return;
        CanCollide = false;
        if (collision.gameObject.tag == "Des") // tag: GameObject Classification
        {
            Debug.Log("Collide Ground");
            CollidePos = this.transform.position;
            CollideRot = this.transform.rotation;
            ReAdjust = true;
            this.GetComponent<Animator>().SetBool("Exploding", true);
            StartCoroutine(DamageGround(collision.contacts[0].point, radius));
            StartCoroutine(DelayDestroy(collision.contacts[0].point));
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

    IEnumerator DelayDestroy(Vector2 PosCollide)
    {
        while (this.GetComponent<Animator>().GetBool("Exploding"))
            yield return new WaitForSeconds(0.02f);
        CheckDamage(PosCollide);
        DestroyMyself();
    }

    public void CheckDamage(Vector2 PosCollide)
    {
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");
        Vector3 Pos1 = player1.transform.position;
        Vector3 Pos2 = player2.transform.position;
        if ((Pos1.x - PosCollide.x) * (Pos1.x - PosCollide.x) + (Pos1.y - PosCollide.y) * (Pos1.y - PosCollide.y) < 2)
        {
            Debug.Log("Explode Reaches Player1 PosCollide:" + PosCollide + "PosPlayer1:"+Pos1);
            player1.GetComponent<PlayerHealthControl>().ChangeHealth(-damage / 2);
        }
           
        if ((Pos2.x - PosCollide.x) * (Pos2.x - PosCollide.x) + (Pos2.y - PosCollide.y) * (Pos2.y - PosCollide.y) < 2)
        {
            Debug.Log("Explode Reaches Player2 PosCollide:" + PosCollide + "PosPlayer2:" + Pos2);
            player2.GetComponent<PlayerHealthControl>().ChangeHealth(-damage / 2);
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
