using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentleMovement : Movement
{
    [SerializeField] GameObject bulletPrefab;
    GameObject bullet;
    Vector2 bulletOffset;

    [SerializeField] GameObject burstPrefab;
    GameObject burst;
    Vector2 burstOffset;

    void Start()
    {
        base.Start();
        speed = 5f;
        jumpForce = new Vector2(0, 0);
        offset = new Vector2[] { new Vector2(0.01f, -0.07f), new Vector2(0.12f, -0.01f) };
        size = new Vector2[] { new Vector2(0.52f, 0.94f), new Vector2(0.73f, 0.67f) };
        groundTriggerOffset = new Vector2(-0.55f, -0.4f);
        bulletOffset = new Vector2(0.6f, -0.2f);
        burstOffset = new Vector2(1.03f, 0.6f);
        ColliderState = 0;
        attacks = new Attacks[2];
        attacks[0] = new Attacks(Attack1);
        attacks[1] = new Attacks(Attack2);
    }

    void FixedUpdate()
    {
        if (IsGrounded && Moving)
            Move();
    }

    protected override IEnumerator Attack1()
    {
        if (IsGrounded && !Damaged)
        {
            yield return new WaitForSeconds(0.07f);
            bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position + new Vector3(direction * bulletOffset.x, bulletOffset.y, 0);
            bullet.GetComponent<BulletBehaviour>().SetDirection(direction);
        }
        yield return null;
    }

    protected IEnumerator Attack2()
    {
        if (IsGrounded && !Damaged)
        {
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(new Vector2(-direction * 1.2f, 0.8f), ForceMode2D.Impulse);
        }
        yield return null;
    }

    void BurstInstantiate()
    {
        burst = Instantiate(burstPrefab);
        burst.transform.position = this.transform.position + new Vector3(direction * burstOffset.x, burstOffset.y, 0);
    }
}
