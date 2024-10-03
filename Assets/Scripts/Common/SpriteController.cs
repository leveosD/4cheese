using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteController : MonoBehaviour
{
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Movement movement;
    Vector2 raycastOffset;
    Punch punch;
    protected bool direction;

    protected bool rightSide;
    
    public abstract bool Moving { get; set; }
    public abstract int Attacking { get; set; }
    public abstract bool Jumping { get; set; }
    public abstract DamageType Damaged { get; set; }
    
    protected void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rightSide = sprite.flipX;
        raycastOffset = new Vector2(0, -2.7f);
        movement = GetComponent<Movement>();
        direction = true;
    }
    
    protected virtual bool IsIdle()
    {
        if (Damaged == DamageType.NONE && Attacking == 0 && !Jumping && !Moving)
            return true;
        return false;
    }

    protected IEnumerator JumpState()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => movement.Velocity.y <= 0);

        if(Attacking == 0 && Jumping)
            anim.Play("Base Layer.idle");
            //anim.Play("Base Layer.land"); ÏÅÐÅÐÈÑÎÂÀÒÜ È ÄÎÁÀÂÈÒÜ ÀÍÈÌÀÖÈÞ ÏÐÈÇÅÌËÅÍÈß
    }
}
