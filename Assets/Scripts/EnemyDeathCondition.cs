using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCondition : MonoBehaviour
{
    //[SerializeField] List<GameObject> enemyList;
    [SerializeField] GameObject jumppadPrefab;
    GameObject platform;
    [SerializeField] Vector3 spawnPosition;
    BoxCollider2D col;

    int count;
    int Count
    {
        get { return count; }
        set 
        { 
            count = value;
            Debug.Log("Count: " + Count);
            if (count == 0)
            {
                platform = GameObject.Instantiate(jumppadPrefab);
                platform.transform.position = spawnPosition;
                Destroy(this);
            }
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GlobalEvents.PLAYERS_DEATH, Reset);
    }

    void Start()
    {
        Count = transform.childCount;
        col = GetComponent<BoxCollider2D>();
        Messenger.AddListener(GlobalEvents.PLAYERS_DEATH, Reset);
    }

    void Reset()
    {
        Count = transform.childCount;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" && collider.GetType() == typeof(CapsuleCollider2D))
        {
            Count--;
        }
    }
}