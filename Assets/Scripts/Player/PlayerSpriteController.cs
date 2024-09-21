using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : SpriteController
{
    [SerializeField] bool moving;
    public override bool Moving
    {
        get { return moving; }
        set
        {
            moving = value;
            //anim.SetBool("moving", value);
            if (value == true) anim.Play("Base Layer.move");
            //else anim.Play("Base Layer.idle");
        }
    }

    [SerializeField] int attacking;
    public override int Attacking
    {
        get { return attacking; }
        set
        {
            if (value != attacking && value != 0 && value < movement.AttacksLength + 1)
            {
                string animation = "Base Layer.attack" + value.ToString();
                if (Jumping)
                    animation += "Jump";
                anim.Play(animation);
                attacking = value;
            }
            else
                attacking = 0;
        }
    }

    [SerializeField] bool jumping;
    public override bool Jumping
    {
        get { return jumping; }
        set
        {
            //anim.SetBool("jumping", value);
            if (value == true && !Jumping)
            {
                anim.Play("Base Layer.jump");
                StartCoroutine(JumpState());
            }
            else
                anim.speed = 1f;
            jumping = value;
        }
    }

    [SerializeField] bool damaged;
    public override bool Damaged
    {
        get { return damaged; }
        set
        {
            damaged = value;
            if (value == true)
            {
                anim.Play("Base Layer.damaged");
            }
        }
    }

    void Update()
    {

        if (movement.direction > 0 && !direction)
        {
            direction = true;
            sprite.flipX = rightSide;
            movement.SetAttackDirection();
        }
        else if (movement.direction < 0 && direction)
        {
            direction = false;
            sprite.flipX = !rightSide;
            movement.SetAttackDirection();
        }

        if (IsIdle())
            anim.Play("Base Layer.idle");
    }
}