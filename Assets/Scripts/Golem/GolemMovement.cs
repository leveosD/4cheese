using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : Movement
{
    void Start()
    {
        base.Start();
        speed = 5f;
        jumpForce = new Vector2(0, 0);
        offset = new Vector2[] { col.offset, new Vector2(0.07f, col.offset.y) };
        size = new Vector2[] { col.size, col.size };
        groundTriggerOffset = new Vector2(0, 0);
        ColliderState = 0;
        attackForce = new Vector2(2f, 1f);
        finalMask = 1 << LayerMask.NameToLayer("Player");
        attacks = new Attacks[2];
        attacks[0] = new Attacks(Attack1);
        attacks[1] = new Attacks(Attack2);
    }

    protected override IEnumerator Attack1()
    {
        if (IsGrounded && !Damaged)
        {
            StartCoroutine(StrongAttack());
        }
        yield return null;
    }

    protected IEnumerator Attack2()
    {
        if (IsGrounded && !Damaged)
        {
            StartCoroutine(FootAttack());
        }
        yield return null;
    }

    IEnumerator StrongAttack()
    {
        yield return null;
    }

    IEnumerator FootAttack()
    {
        yield return null;
    }
}
