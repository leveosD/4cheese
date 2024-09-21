using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirController : EnemyController
{
    //[SerializeField] GameObject player;

    SirMovement sirMovement;
    SirSpriteController sirSprite;
    int attackCounter;
    //CircleCollider2D sleepTrigger;
    //int playerMask;

    /*[SerializeField] bool awakening;
    bool Awakening
    {
        get { return awakening; }
        set
        {
            sirSprite.Awakening = value;
        }
    }*/

    protected void Start()
    {
        base.Start();
        sirMovement = GetComponent<SirMovement>();
        sirSprite = GetComponent<SirSpriteController>();
        attackCounter= 0;
        Health = 3;
        MaxTemp = 2;
    }

    void Update()
    {
        if (!sleepTrigger.enabled && !Damaged && !sirSprite.Awakening)
        {
            float distance = player.transform.position.x - transform.position.x;
            if (Moving)
                movementContr.direction = distance >= 0 ? 1 : -1;

            if (Attacking == 0 && Moving && Mathf.Abs(distance) <= 9 && AttackDelay == 0)
            {
                Moving = false;
                Attacking = 1;
                attackCounter++;
                if (attackCounter == 3)
                {
                    AttackDelay = 2f;
                    attackCounter = 0;
                }
            }
            else if (Attacking == 0 && !Moving && !sleepTrigger.enabled && !Damaged)
            {
                Moving = true;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "DEATH")
        {
            movementContr.IsTrigger = false;
            Death();
        }
        if (sleepTrigger.IsTouchingLayers(HostileMask))
        {
            sleepTrigger.enabled = false;
            StartCoroutine(WakeUp());
            //GetComponent<Animator>().Play("Base Layer.Awake");
        }
    }

    IEnumerator WakeUp()
    {
        Debug.Log("wake up");
        sirSprite.Awakening = true;
        yield return new WaitForSeconds(1f);
        Moving = true;
        sirSprite.Awakening = false;
    }
}
