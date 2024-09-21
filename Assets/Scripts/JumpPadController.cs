using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour
{
    BoxCollider2D platform;
    Rigidbody2D rb;
    Animator animator;
    Animator wormsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wormsAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float contact = collision.GetContact(0).point.y;
        float highBorder = transform.position.y + platform.size.y / 2 + transform.localScale.y * platform.offset.y;
        if (collision.collider.tag == "Ground")
            rb.bodyType = RigidbodyType2D.Static;
        else if (collision.collider.tag == "Player" && collision.GetContact(0).point.y > (transform.position.y + platform.size.y / 2 + transform.localScale.y * platform.offset.y))
        {
            animator.Play("Base Layer.jumppadActive");
            wormsAnimator.Play("Base Layer.jumpOut");
            //Debug.Log(wormsAnimator);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && collider.isTrigger && collider.gameObject.GetComponent<CharacterController>().Attacking != 0)
        {
            wormsAnimator.Play("Base Layer.jumpOut");
        }
    }
}