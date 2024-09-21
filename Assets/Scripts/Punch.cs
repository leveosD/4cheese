using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    BoxCollider2D trigger;
    [SerializeField] string targetTag;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }
    public void SetEnabled()
    {
        trigger.enabled = !trigger.enabled;
    }

    public void SetDirection()
    {
        trigger.offset *= new Vector2(-1, 1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == targetTag)
        {
            int d = collider.transform.position.x - this.transform.position.x > 0 ? 1 : -1;
            //collider.gameObject.GetComponent<NewMovement>().GetDamaged(new Vector2(2.4f * d, 1f));
        }
    }
}
