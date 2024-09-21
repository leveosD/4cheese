using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstForce : MonoBehaviour
{
    CapsuleCollider2D col;

    Vector2 finalOffset;
    Vector2 finalSize;
    float offsetStepX;
    float offsetStepY;
    float sizeStepX;
    float sizeStepY;

    int mask;

    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        finalOffset = new Vector2(-0.04f, -0.38f);
        finalSize = new Vector2(0.55f, 0.28f);
        offsetStepX = (finalOffset.x - col.offset.x) / 7;
        offsetStepY = (finalOffset.y - col.offset.y) / 7;
        sizeStepX = (finalSize.x - col.size.x) / 7;
        sizeStepY = (finalSize.y - col.size.y) / 7;
        mask = 1 << LayerMask.NameToLayer("Player");
        StartCoroutine(Growing());
    }

    // offset = -0.04, -0.38
    //size = 0.55, 0.28

    IEnumerator Growing()
    {

        for(int i = 0; i < 7; i++)
        {
            //col.radius += 0.035f;
            col.offset += new Vector2(offsetStepX, offsetStepY);
            col.size += new Vector2(sizeStepX, sizeStepY);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag != "Ground" && col.IsTouchingLayers(mask))
        {
            Debug.Log(collider.gameObject);
            int direction = ((collider.transform.position.x - transform.position.x) > 0 ? 1 : -1);
            collider.gameObject.GetComponent<CharacterController>().GetDamage(new Vector2(2.5f * direction, 1.5f), DamageType.FIRE, 1);
        }
    }
}
