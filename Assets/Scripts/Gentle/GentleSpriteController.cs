using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentleSpriteController : EnemySpriteController
{
    bool awakening;
    public bool Awakening
    {
        get { return awakening; }
        set
        {
            awakening = value;
            if (value == true)
                anim.Play("Base Layer.standup");
        }
    }

    protected override bool IsIdle()
    {
        if (Damaged == DamageType.NONE && Attacking == 0 && !Jumping && !Moving && !Awakening)
            return true;
        return false;
    }
}
