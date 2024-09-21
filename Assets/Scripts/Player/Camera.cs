using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;
    float step = 30f;

    void Start()
    {
        offset = new Vector3(0, 3, transform.position.z);
    }

    void Update()
    {
        Vector3 target = new Vector3()
        {
            x = player.transform.position.x,
            y = player.transform.position.y + player.transform.localScale.y / 3,
            z = player.transform.position.z - 1f,
        };

        transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
        //transform.position = Vector3.LerpUnclamped(transform.position, target, step * Time.deltaTime);
        //Vector3 v = player.GetComponent<Rigidbody2D>().velocity;
        //transform.position = Vector3.SmoothDamp(transform.position, target, ref v, step);
    }
}
