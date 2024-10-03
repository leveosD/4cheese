using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected float speed;
    public float direction;
    protected Vector2 jumpForce;
    //public bool zeroVelocity;
    /*[SerializeField] protected bool moveRight;
    [SerializeField] protected bool moveLeft;*/
    protected Rigidbody2D rb;

    protected Vector2[] offset;
    protected Vector2[] size;
    protected Vector2 groundTriggerOffset;
    protected CapsuleCollider2D col;
    protected BoxCollider2D groundChecker;
    protected BoxCollider2D trigger;
    protected int finalMask;

    protected Vector2 attackForce;

    public Vector2 Velocity
    {
        get { return rb.velocity; }
    }

    public bool IsTrigger
    {
        get { return col.isTrigger; }
        set { col.isTrigger = value;}
    }

    public bool IsGrounded
    {
        get
        {
            //return groundChecker.IsTouching(collider);
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = 1 << LayerMask.NameToLayer("Ground");
            groundChecker.GetContacts(colliders);
            return colliders.Count > 0;
        }
    }

    /*[SerializeField] int colliderState;
    public int ColliderState
    {
        get { return colliderState; }
        set
        {
            if (value != colliderState)
            {
                colliderState = value;
                if (value == 1)
                {
                    col.size = size[1];
                    col.offset = new Vector2(offset[1].x * direction, offset[1].y);
                    groundChecker.offset = new Vector2(groundChecker.offset.x, groundTriggerOffset[1]);
                }
                else if (value == 0)
                {
                    col.size = size[0];
                    col.offset = new Vector2(offset[0].x * direction, offset[0].y);
                    groundChecker.offset = new Vector2(groundChecker.offset.x, groundTriggerOffset[0]);
                }
            }
        }
    }*/
    
    [SerializeField] int colliderState;
    public int ColliderState
    {
        get { return colliderState; }
        set
        {
            if (value != colliderState)
            {
                colliderState = value;
                if (value == 1)
                {
                    col.size = size[1];
                    col.offset = new Vector2(offset[1].x * direction, offset[1].y);
                    groundChecker.offset = new Vector2(groundChecker.offset.x, groundTriggerOffset[1]);
                }
                else if (value == 0)
                {
                    col.size = size[0];
                    col.offset = new Vector2(offset[0].x * direction, offset[0].y);
                    groundChecker.offset = new Vector2(groundChecker.offset.x, groundTriggerOffset[0]);
                }
            }
        }
    }

    [SerializeField] protected float smoothTime;

    IEnumerator SmoothChanges()
    {
        col.size = Vector2.LerpUnclamped(size[0], size[1], smoothTime);
        col.offset = Vector2.LerpUnclamped(offset[0], offset[1], smoothTime);
        yield return null;
    }

    [SerializeField] bool moving;
    public bool Moving
    {
        get { return moving; }
        set
        {
            moving = value;
        }
    }

    protected delegate IEnumerator Attacks();
    protected Attacks[] attacks;
    public int AttacksLength
    {
        get { return attacks.Length; }
    }
    [SerializeField] int attacking;
    public int Attacking
    {
        get { return attacking; }
        set
        {
            if (value != attacking && value != 0 && (value-1) < attacks.Length)
            {
                StartCoroutine(attacks[value - 1]());
            }
            attacking = value;
        }
    }

    [SerializeField] bool jumping;
    public bool Jumping
    {
        get { return jumping; }
        set
        {
            if (value == true)
                Jump();
            jumping = value;
        }
    }

    [SerializeField] DamageType damaged;
    public DamageType Damaged
    {
        get { return damaged; }
        set
        {
            damaged = value;
            if (Damaged != DamageType.NONE) trigger.enabled = false;
            //ColliderState = 0;
            /*rb.velocity = Vector2.zero;
            rb.AddForce(value, ForceMode2D.Impulse);*/
        }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundChecker = GetComponents<BoxCollider2D>()[0];
        //zeroVelocity = false;
        //IsGrounded = false;
        trigger = GetComponents<BoxCollider2D>()[1];
        //moveLeft = true;
        //moveRight = true;
        finalMask = GetComponent<CharacterController>().HostileMask;
    }

    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (Moving && Damaged == DamageType.NONE)
        {
            /*if (direction > 0 && !moveRight || direction < 0 && !moveLeft)
                direction = 0;*/

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }

    protected virtual void Jump()
    {
        if (IsGrounded && Damaged == DamageType.NONE)
        {
            rb.AddForce(new Vector2(jumpForce.x * direction, jumpForce.y), ForceMode2D.Impulse);
            ColliderState = 1;
        }
    }

    protected abstract IEnumerator Attack1();

    protected void ChangeCollider(int state)
    {
        ColliderState = state;
    }

    protected int IsSidePlatform(Collision2D collision)
    {
        int side = 0;
        if (collision.contacts.Length > 1)
            foreach(var contact in collision.contacts)
            {
                if (contact.point.x > transform.position.x)
                    side++;
                else if (contact.point.x < transform.position.x)
                    side--;
            }
        return side;
    }

    public void AddForce(Vector2 force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    /*protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            int side = IsSidePlatform(collision);
            if (side > 0)
            {
                moveRight = false;
            }
            else if(side < 0)
                moveLeft = false;
            else
            {
                moveRight = true;
                moveLeft = true;
            }
        }
    }*/

    /*protected void OnCollisionExit2D(Collision2D collision)
    {
        moveLeft = true;
        moveRight = true;
    }*/

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (trigger.IsTouchingLayers(finalMask) && !collider.isTrigger)
        {
            int d = collider.transform.position.x - transform.position.x > 0 ? 1 : -1;
            //collider.gameObject.GetComponent<CharacterController>().Damaged = attackForce;
            collider.gameObject.GetComponent<CharacterController>().GetDamage(new Vector2(d * attackForce.x, attackForce.y), 
                gameObject.GetComponent<CharacterController>().AttackType, 1);

        }
        /*else if (collider.tag == "Ground")
        {
            ColliderState = 0;
        }*/
    }

    /*protected void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Ground")
            IsGrounded = true;
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Ground" && !IsGrounded(collider))
        {
            IsGrounded = false;
            //moveLeft = true;
            //moveRight = true;
        }
    }*/

    public void SetAttackDirection()
    {
        trigger.offset *= new Vector2(-1, 1);
    }

    protected void AttackTriggerTurnOn()
    {
        trigger.enabled = true;
    }

    protected void AttackTriggerTurnOff()
    {
        trigger.enabled = false;
    }
}