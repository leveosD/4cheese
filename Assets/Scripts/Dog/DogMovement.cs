using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : Movement
{
    [SerializeField] bool awakening;
    public bool Awakening
    {
        get { return awakening; }
        set
        {
            awakening = value;
            if (value == true) WakeUp();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        speed = 5f;
        jumpForce = new Vector2(2f, 1.2f);
        offset = new Vector2[] { new Vector2(0.016f, -0.09f), new Vector2(0.016f, -0.03f) };
        size = new Vector2[] { new Vector2(0.72f, 0.72f), new Vector2(0.72f, 0.6f) };
        groundTriggerOffset = new Vector2(-0.47f, -0.32f);
        ColliderState = 1;
        finalMask = 1 << LayerMask.NameToLayer("Player");
        attacks = new Attacks[2];
        attacks[0] = new Attacks(Jump);
        attacks[1] = new Attacks(Attack1);
    }

    void FixedUpdate()
    {
        if (IsGrounded && Moving)
            Move();
    }

    void WakeUp()
    {
        if (IsGrounded && !Damaged)
        {
            rb.AddForce(new Vector2(0, 1.5f), ForceMode2D.Impulse);
            //trigger.enabled = true;
        }
    }

    protected override void Attack1() 
    {
        if(IsGrounded && !Damaged)
        {
            StartCoroutine(Push());
        }
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(0.23f);
        rb.AddForce(new Vector2(direction * 2.5f, 0), ForceMode2D.Impulse);
    }
}