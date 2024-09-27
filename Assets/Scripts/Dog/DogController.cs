using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.CoreModule

public class DogController : EnemyController
{
    //[SerializeField] GameObject player;

    DogSpriteController dogSprite;
    DogMovement dogMovement;
    /*
        CircleCollider2D sleepTrigger;
        int playerMask;*/

    [SerializeField] bool awakening;
    bool Awakening
    {
        get { return awakening; }
        set
        {
            dogMovement.Awakening = value;
            dogSprite.Awakening = value;
        }
    }

    protected void Start()
    {
        base.Start();
        dogSprite = GetComponent<DogSpriteController>();
        dogMovement = GetComponent<DogMovement>();
        Messenger.RemoveListener(GlobalEvents.PLAYERS_DEATH, base.Reset);
        Messenger.AddListener(GlobalEvents.PLAYERS_DEATH, Reset);
        MaxHealth = 2;
        Health = 2;
        MaxTemp = 3;
    }

    protected void Reset()
    {
        base.Reset();
        StopCoroutine(WakeUp());
        Awakening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sleepTrigger.enabled && !Damaged)
        {
            float distance = player.transform.position.x - transform.position.x;
            if (Moving)
                movementContr.direction = distance >= 0 ? 1 : -1;

            if (Attacking == 0 && Moving && AttackDelay == 0 && Mathf.Abs(distance) <= 15)
            {
                Moving = false;
                AttackDelay = 2;
                if (Mathf.Abs(distance) <= 10)
                {
                    Attacking = 1;
                }
                else
                {
                    Attacking = 2;
                }
            }
        }
    }

    //protected override void Death() { }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Debug.Log(Damaged);
            if (Damaged)
            {
                
                StartCoroutine(WakeUp());
            }
            Damaged = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collider)
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
        }
    }

    IEnumerator StopSliding()
    {
        yield return new WaitUntil(() => dogMovement.Velocity == Vector2.zero);
        if (Attacking != 0 && !Awakening)
        {
            Attacking = 0;
            StartCoroutine(WakeUp());
        }
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(0.7f);
        Awakening = true;
        yield return new WaitForSeconds(0.7f);
        Moving = true;
        Awakening = false;
    }
}
