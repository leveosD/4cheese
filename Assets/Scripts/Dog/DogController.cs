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
            //if (value == true && awakening != true) StartCoroutine(WakeUp());
            dogMovement.Awakening = value;
            dogSprite.Awakening = value;
            awakening = value;
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
        //StopCoroutine(WakeUp());
        Awakening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sleepTrigger.enabled && Damaged == DamageType.NONE)
        {
            float distance = player.transform.position.x - transform.position.x;
            if (Moving)
                movementContr.direction = distance >= 0 ? 1 : -1;

            if (Attacking == 0 && Moving && AttackDelay == 0 && Mathf.Abs(distance) <= 15)
            {
                Moving = false;
                AttackDelay = 2;
                Attacking = Random.Range(1, 3);
                //Awakening = true;
            }
            else if (Attacking == 0 && !Awakening && !Moving)
            {
                Awakening = true;
            }
        }
    }

    //protected override void Death() { }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Damaged != DamageType.NONE)
            {
                Damaged = DamageType.NONE;
            }
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
            Moving = true;
            //Awakening = true;
        }
    }

    void NotAwakening()
    {
        Moving = true;
        Awakening = false;
        Debug.Log(Awakening);
    }

    /*IEnumerator StopSliding()
    {
        yield return new WaitUntil(() => dogMovement.Velocity == Vector2.zero);
        if (!Awakening && Damaged == DamageType.NONE)
        {
            Attacking = 0;
            StartCoroutine(WakeUp());
        }
    }*/

    /*IEnumerator WakeUp()
    {
        yield return new WaitUntil(() => dogMovement.Velocity == Vector2.zero && Damaged == DamageType.NONE);
        Attacking = 0;
        yield return new WaitForSeconds(0.7f);
        Moving = true;
    }*/
}
