using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : SpriteController
{
    protected Animator cracksAnim;
    //SpriteRenderer cracksSprite;
    CharacterController controller;
    [SerializeField] string name;

    public int stage;

    [SerializeField] bool moving;
    public override bool Moving
    {
        get { return moving; }
        set
        {
            moving = value;
            if (value == true)
            {
                // cracksAnim.Play("Base Layer.Stage" + stage + "Move");
                //anim.Play("Base Layer.move");
                anim.Play("Base Layer." + name + "MovingStage" + stage);
                //cracksAnim.Play("Base Layer.Move");
            }
        }
    }

    [SerializeField] int attacking;
    public override int Attacking
    {
        get { return attacking; }
        set
        {
            Debug.Log("Start");
            Debug.Log(movement.Moving);
            Debug.Log(movement.AttacksLength);
            Debug.Log("End");
            if (value != attacking && value != 0 && value < movement.AttacksLength + 1)
            {
                //anim.Play("Base Layer.attack" + value.ToString());
                //cracksAnim.Play("Base Layer.Stage" + stage + "Attack" + value.ToString());
                attacking = value;
                anim.Play("Base Layer." + name + "Attack" + value + "Stage" + stage);
                //cracksAnim.Play("Base Layer.Attack" + value);
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
                //anim.Play("Base Layer.jump");
                //cracksAnim.Play("Base Layer.Stage" + stage + "Jump");
                //StartCoroutine(base.JumpState());
                anim.Play("Base Layer." + name + "JumpStage" + stage);
                //cracksAnim.Play("Base Layer.Jump");
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
                //anim.Play("Base Layer.damaged");
                stage++;
                /*if (stage == 1)
                    cracksSprite.sortingOrder = 0;*/
                if (controller.Health > 0)
                {
                    anim.Play("Base Layer." + name + "Stage" + stage);
                }
            }
        }
    }

    protected void Start()
    {
        base.Start();
        //cracksAnim = transform.GetChild(0).GetComponent<Animator>();
        //cracksSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController>();
        stage = 0;
    }

    void Update()
    {
        if (movement.direction > 0 && !direction)
        {
            direction = true;
            sprite.flipX = rightSide;
            //cracksSprite.flipX = rightSide;
            movement.SetAttackDirection();
        }
        else if (movement.direction < 0 && direction)
        {
            direction = false;
            sprite.flipX = !rightSide;
            //cracksSprite.flipX = !rightSide;
            movement.SetAttackDirection();
        }

        if (IsIdle())
        {
            anim.Play("Base Layer." + name + "IdleStage" + stage);
            //cracksAnim.Play("Base Layer.Idle");;
        }
    }
}
