using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    //protected SpriteController spriteContr;
    protected Movement movementContr;
    protected Vector3 startPosition;
    protected float stunTime = 0.5f;

    protected DamageType attackType;
    public DamageType AttackType
    {
        get { return attackType; }
        protected set { attackType = value; }
    }

    int hostileMask;
    public int HostileMask
    {
        get { return hostileMask; }
        protected set { hostileMask = value; }
    }

    int health;
    public int Health
    {
        get { return health; }
        protected set
        {
            health = value;
            if (health <= 0)
                movementContr.IsTrigger = true;
        }
    }

    int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        protected set { maxHealth = value; }
    }

    //[SerializeField] bool moving;
    public abstract bool Moving { get; protected set; }
    /*{
        get { return moving; }
        protected set
        {
            moving = value;
            spriteContr.Moving = value;
            movementContr.Moving = value;
        }
    }*/

    //[SerializeField] int attacking;
    public abstract int Attacking { get; protected set; }
    /*{
        get { return attacking; }
        protected set
        {
            attacking = value;
            spriteContr.Attacking = value;
            movementContr.Attacking = value;
        }
    }*/

    //[SerializeField] bool jumping;
    public abstract bool Jumping { get; protected set; }
    /*{
        get { return jumping; }
        protected set
        {
            jumping = value;
            spriteContr.Jumping = value;
            movementContr.Jumping = value;
        }
    }*/

    //[SerializeField] Vector2 damaged;
    public abstract bool Damaged { get; set; }
    /*{
        get { return damaged; }
        set
        {
            if (value != Vector2.zero)
            {
                damaged = value;
                movementContr.Damaged = value;
                if (value != Vector2.zero)
                {
                    spriteContr.Damaged = true;
                }
                else
                    spriteContr.Damaged = false;
            }
        }
    }*/


    /*IEnumerator Stunned()
    {
        yield return new WaitForSeconds(stunTime);
        Damaged = false;
        Debug.Log("Stunned");
    }*/

    protected abstract void Death();

    public abstract void GetDamage(Vector2 force, DamageType damageType, int damage);

    protected void Start()
    {
        //spriteContr = GetComponent<SpriteController>();
        movementContr = GetComponent<Movement>();
        startPosition = transform.position;
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    protected abstract void OnTriggerEnter2D(Collider2D collider);
}