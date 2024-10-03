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
            bool oldVal = awakening;
            awakening = value;
            if(value == true && oldVal != true)
            {
                //StartCoroutine(WakeUp(0.5f));
            }
        }
    }

    int waitcounter;

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
        attacks[0] = new Attacks(Attack1);
        attacks[1] = new Attacks(Attack2);
        waitcounter= 0;
    }

    void FixedUpdate()
    {
        if (IsGrounded && Moving)
            Move();
    }

    IEnumerator WakeUp(float delay)
    {
        if (waitcounter == 0)
        {
            waitcounter++;
            //yield return new WaitUntil(() => IsGrounded && Velocity == Vector2.zero);
            yield return new WaitForSeconds(delay);
            rb.AddForce(new Vector2(0, 1.5f), ForceMode2D.Impulse);
            waitcounter = 0;
            //trigger.enabled = true;
        }
    }

    protected override IEnumerator Attack1()
    {
        if (IsGrounded && Damaged == DamageType.NONE)
        {
            //yield return new WaitUntil(() => IsGrounded && Velocity == Vector2.zero);
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(new Vector2(jumpForce.x * direction, jumpForce.y), ForceMode2D.Impulse);
            //StartCoroutine(WakeUp(1));
        }
    }

    IEnumerator Attack2() 
    {
        if(IsGrounded && Damaged == DamageType.NONE)
        {
            //yield return new WaitUntil(() => IsGrounded && Velocity == Vector2.zero);
            yield return new WaitForSeconds(0.5f);
            rb.AddForce(new Vector2(direction * 3f, 0), ForceMode2D.Impulse);
            //StartCoroutine(WakeUp(0.2f));
        }
    }
}