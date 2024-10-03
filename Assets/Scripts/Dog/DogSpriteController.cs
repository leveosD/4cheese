using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpriteController : EnemySpriteController
{
    [SerializeField] bool awakening;
    public bool Awakening
    {
        get { return awakening; }
        set
        {
            awakening = value;
            if(Awakening) anim.Play("Base Layer.DogAwakeningStage" + stage);
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
