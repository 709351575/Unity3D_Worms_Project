using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
     public int radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision) // collision: something collided
    {
        if (collision.gameObject.tag == "Des") // tag: GameObject Classification
        {
            StartCoroutine(Boom(collision));
        }
    }

    IEnumerator Boom(Collision2D collision)
    {
        yield return new WaitForSeconds(3f);
        if (collision.contacts.Length > 0)
        {
            collision.gameObject.GetComponent<DestructibleSprite>().ApplyDamage(collision.contacts[0].point, radius);
        }
        Destroy(gameObject);
    }
}
