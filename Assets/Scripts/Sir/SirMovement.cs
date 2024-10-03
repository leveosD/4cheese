using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirMovement : Movement
{
    void Start()
    {
        base.Start();
        speed = 5f;
        jumpForce = new Vector2(0, 0);
        offset = new Vector2[] { new Vector2(0, -0.06f), new Vector2(0.1f, -0.06f) };
        size = new Vector2[] { new Vector2(0.42f, 0.84f), new Vector2(0.28f, 0.84f) };
        groundTriggerOffset = new Vector2(-0.48f, -0.48f);
        ColliderState = 0;
        attackForce = new Vector2(1.5f, 1f);
        finalMask = 1 << LayerMask.NameToLayer("Player");
        attacks = new Attacks[1];
        attacks[0] = new Attacks(Attack1);
    }

    void FixedUpdate()
    {
        if (IsGrounded && Moving)
            Move();
    }

    protected override IEnumerator Attack1()
    {
        if (IsGrounded && Damaged == DamageType.NONE)
        {
            StartCoroutine(Push());
        }
        yield return null;
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(0.13f);
        rb.AddForce(new Vector2(direction * 7f, 0), ForceMode2D.Impulse);
    }
}
