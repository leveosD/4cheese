using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    protected PlayerMovement playerMovement;
    protected PlayerSpriteController spriteContr;
    int currentAttack;

    [SerializeField] bool moving;
    public override bool Moving
    {
        get { return moving; }
        protected set
        {
            moving = value;
            spriteContr.Moving = value;
            movementContr.Moving = value;
        }
    }

    [SerializeField] int attacking;
    public override int Attacking
    {
        get { return attacking; }
        protected set
        {
            attacking = value;
            spriteContr.Attacking = value;
            movementContr.Attacking = value;
        }
    }

    [SerializeField] bool jumping;
    public override bool Jumping
    {
        get { return jumping; }
        protected set
        {
            jumping = value;
            spriteContr.Jumping = value;
            movementContr.Jumping = value;
        }
    }

    [SerializeField] bool damaged;
    public override bool Damaged
    {
        get { return damaged; }
        set
        {
            damaged = value;
            movementContr.Damaged = value;
            spriteContr.Damaged = value;
        }
    }

    protected void Start()
    {
        base.Start();
        playerMovement = GetComponent<PlayerMovement>();
        spriteContr = GetComponent<PlayerSpriteController>();
        Health = 1;
        currentAttack = 1;
        AttackType = DamageType.FIRE;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attacking = currentAttack;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jumping = true;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentAttack++;
            AttackType = DamageType.PUNCH;
            if (currentAttack > 2)
            {
                currentAttack = 1;
                AttackType = DamageType.FIRE;
            }
        }

        movementContr.direction = Input.GetAxis("Horizontal");
        if(movementContr.direction != 0 && !Jumping && !Damaged && Attacking == 0 && !Moving)
        {
            spriteContr.Moving = true;
        }
        else if(movementContr.direction == 0 || Jumping || Damaged || Attacking == 0)
            spriteContr.Moving = false;
    }

    protected override void Death()
    {
        //Debug.Log("meh... you died");
        playerMovement.IsTrigger = false;
        Messenger.Broadcast(GlobalEvents.PLAYERS_DEATH);
        transform.position = startPosition;
        Damaged = false;
    }

    public override void GetDamage(Vector2 force, DamageType damageType, int damage)
    {
        Damaged = true;
        /*if (Health > 0)
            StartCoroutine(Stunned());*/
        Moving = false;
        Attacking = 0;
        Jumping = false;
        movementContr.AddForce(force);
        Health -= damage;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Damaged = false;
            //Jumping = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            Jumping = false;
        }
        else if (collider.tag == "DEATH")
        {
            movementContr.IsTrigger = false;
            Death();
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            Attacking = 0;
    }
}
