using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : EnemyController
{
    GolemMovement golemMovement;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        golemMovement = GetComponent<GolemMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sleepTrigger.enabled && !Damaged)
        {
            float distance = player.transform.position.x - transform.position.x;
            if (Moving)
                movementContr.direction = distance >= 0 ? 1 : -1;

            //Debug.Log(Mathf.Abs(distance));
            if (Attacking == 0 && Mathf.Abs(distance) <= 5f && AttackDelay == 0)
            {
                Moving = false;
                Attacking = 2;
                AttackDelay = 1f;
            }
            else if (Attacking == 0 && Mathf.Abs(distance) < 10 && Mathf.Abs(distance) > 5f && AttackDelay == 0)
            {
                Moving = false;
                Attacking = 1;
                AttackDelay = 3f;
            }
            else if (Attacking == 0 && !Moving && !sleepTrigger.enabled && !Damaged && AttackDelay == 0 && Mathf.Abs(distance) >= 10f)
            {
                Moving = true;
            }
        }
    }
}
