using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] protected GameObject player;

    protected ParticleSystem ps;
    protected ParticleSystem.MainModule psMain;
    protected Collider2D sleepTrigger;
    EnemySpriteController spriteContr;

    int temp = 0;
    public int Temp
    {
        get { return temp; }
        protected set
        {
            temp = value;
            if (temp == MaxTemp)
                movementContr.IsTrigger = true;
        }
    }

    int maxTemp = 2;
    public int MaxTemp
    {
        get { return maxTemp; }
        protected set { maxTemp = value; }
    }

    [SerializeField] float attackDelay;
    protected float AttackDelay
    {
        get { return attackDelay; }
        set
        {
            attackDelay = value;
            if (value != 0)
                StartCoroutine(Delay(value));
        }
    }

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
            Debug.Log(gameObject.name);
            damaged = value;
            spriteContr.Damaged = value;
            movementContr.Damaged = value;
        }
    }

    protected void OnDestroy()
    {
        Messenger.RemoveListener(GlobalEvents.PLAYERS_DEATH, Reset);
    }

    protected void Reset()
    {
        Moving = false;
        Jumping = false;
        Damaged = false;
        Attacking = 0;
        transform.position = startPosition;
        sleepTrigger.enabled = true;
        Health = MaxHealth;
        Temp = 0;
        spriteContr.stage = 0;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        spriteContr = GetComponent<EnemySpriteController>();
        player = GameObject.Find("Player");
        sleepTrigger = GetComponents<BoxCollider2D>()[2];
        HostileMask = 1 << LayerMask.NameToLayer("Player");
        startPosition = transform.position;
        Messenger.AddListener(GlobalEvents.PLAYERS_DEATH, Reset);
        
        ps = GetComponent<ParticleSystem>();
        psMain = ps.main;
        Temp = 0;
    }

    protected IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        AttackDelay = 0;
    }

    protected override void Death()
    {
        //Destroy(this);
    }

    public override void GetDamage(Vector2 force, DamageType damageType, int damage)
    {
        Moving = false;
        Attacking = 0;
        Jumping = false;
        movementContr.AddForce(force);
        Debug.Log(damageType.ToString());
        if (damageType == DamageType.FIRE && ps != null)
        {
            if (Temp == 0)
                ps.Play();
            else
            {
                psMain.simulationSpeed = 0.3f + 0.3f * Temp;
            }
            Temp += damage;
        }
        else if(damageType == DamageType.PUNCH)
        {
            Health -= damage;
            spriteContr.Damaged = true;
        }
        movementContr.Damaged = true;
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
        if (sleepTrigger.IsTouchingLayers(HostileMask))
        {
            sleepTrigger.enabled = false;
            Moving = true;
        }
    }

    protected void NotDamaged()
    {
        Damaged = false;
    }
}