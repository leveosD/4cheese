using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    Punch punch;
    [SerializeField] protected bool moveRight;
    [SerializeField] protected bool moveLeft;

    void Start()
    {
        base.Start();
        //punch = this.GetComponentInChildren<Punch>();
        speed = 17f;
        offset = new Vector2[] { new Vector2(0, -0.07f), new Vector2(0, 0.07f) };
        size = new Vector2[] { new Vector2(0.45f, 0.97f), new Vector2(0.45f, 0.8f) };
        groundTriggerOffset = new Vector2(-0.56f, -0.37f);
        jumpForce = Vector2.up * 2.4f;
        Moving = true;
        ColliderState = 0;
        attacks = new Attacks[2];
        attacks[0] = new Attacks(Attack1);
        attacks[1] = new Attacks(Attack1);
        //attackForce = new Vector2(2f, 4f);
        attackForce = new Vector2(1.5f, 2f);
        finalMask = 1 << LayerMask.NameToLayer("Enemy");

        moveLeft = true;
        moveRight = true;
    }

    protected override void Move()
    {
        direction = Input.GetAxis("Horizontal");
        if (direction != 0 && !Damaged)
        {
            if (direction > 0 && !moveRight || direction < 0 && !moveLeft)
                direction = 0;

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            int side = IsSidePlatform(collision);
            if (side > 0)
            {
                moveRight = false;
            }
            else if (side < 0)
                moveLeft = false;
            else
            {
                moveRight = true;
                moveLeft = true;
            }
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        moveLeft = true;
        moveRight = true;
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.tag == "Ground")
        {
            ColliderState = 0;
        }
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Ground" && !groundChecker.IsTouching(collider))
        {
            //IsGrounded = false;
            moveLeft = true;
            moveRight = true;
        }
    }

    protected override IEnumerator Attack1()
    {
        if (!Damaged)
        {
            //punch.SetEnabled();
            if (!IsGrounded)
            {
                rb.AddForce(new Vector2(direction * 1.6f, 0.3f), ForceMode2D.Impulse);
            }
            else if (IsGrounded)// && MIN_TIME <= Time.time - lastClickTime && Time.time - lastClickTime <= MAX_TIME)
            {
                rb.AddForce(new Vector2(direction * 1.6f, 0), ForceMode2D.Impulse);
            }
            //lastClickTime = Time.time;
        }
        yield return null;
    }
}