using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirSpriteController : EnemySpriteController
{
    [SerializeField] bool awakening;
    public bool Awakening
    {
        get { return awakening; }
        set
        {
            awakening = value;
            if (value == true)
            {
                anim.Play("Base Layer.SirIntroduce");
            }
        }
    }

    protected void Start()
    {
        base.Start();
        Awakening = false;
    }

    protected override bool IsIdle()
    {
        if (Damaged == DamageType.NONE && Attacking == 0 && !Moving && !Awakening)
            return true;
        return false;
    }
}
