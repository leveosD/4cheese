using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentleController : EnemyController
{
    GentleMovement gentleMovement;
    GentleSpriteController gentleSprite;
    //protected ParticleSystem ps;

    public bool forHonor;

    [SerializeField] bool awakening;
    bool Awakening
    {
        get { return awakening; }
        set
        {
            //gentleMovement.Awakening = value;
            gentleSprite.Awakening = value;
        }
    }

    protected void Start()
    {
        base.Start();
        gentleSprite = GetComponent<GentleSpriteController>();
        gentleMovement = GetComponent<GentleMovement>();
        //ps = GetComponent<ParticleSystem>();
        MaxHealth = 2;
        Health = MaxHealth;
        MaxTemp = 2;
    }

    void Update()
    {
        if (!sleepTrigger.enabled && !Damaged)
        {
            float distance = player.transform.position.x - transform.position.x;
            if (Moving)
                movementContr.direction = distance >= 0 ? 1 : -1;

            if (Attacking == 0 && !Jumping && Mathf.Abs(distance) <= 50 && AttackDelay == 0)
            {
                Moving = false;
                if (Mathf.Abs(distance) <= 10)// && !forHonor)
                {
                    /*RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, new Vector2(-movementContr.direction, -0.4f), 5f, 1 << LayerMask.NameToLayer("Ground"));
                    if (hit.collider != null)
                    {
                        Attacking = 2;
                    }
                    else
                        forHonor = true;*/
                    Attacking = 2;
                    Debug.Log("Attack set");
                    AttackDelay = 1.5f;
                }
                else
                {
                    //Debug.Log(Attacking);
                    Attacking = 1;
                    AttackDelay = 1.1f;
                }
            }
            else if (Attacking == 0 && !Jumping && !Moving && Mathf.Abs(distance) > 50)
            {
                Moving = true;
                forHonor = false;
            }
        }
    }

    /*public void GetDamage(Vector2 direction)
    {
        base.GetDamage(direction);
        Debug.Log("get damage");
        ps.Play();
        StartCoroutine(Molten());
    }*/

    /*IEnumerator Molten()
    {
        yield return new WaitForSeconds(2f);
        ps.Stop();
    }*/

    /*protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ground" && Attacking == 2)
        {
            Awakening = true;
        }
        base.OnTriggerEnter2D(collider);
    }*/
}
