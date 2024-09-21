using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    int direction;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    Vector2 bulletForce;
    //BoxCollider2D col;
    float speed;

    void Start()
    {
        speed = 20f;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bulletForce = new Vector2(1.7f, 1f);
        //col = GetComponent<BoxCollider2D>();
        //col.callbackLayers = 
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, 0f);
    }

    public void SetDirection(float d)
    {
        direction = d > 0 ? 1 : -1;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            int direction = (collider.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            collider.gameObject.GetComponent<CharacterController>().GetDamage(new Vector2(bulletForce.x * direction, bulletForce.y), DamageType.PUNCH, 1);
            Destroy(this.gameObject);
        }
        else if(collider.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
