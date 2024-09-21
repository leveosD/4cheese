using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    int number;
    [SerializeField] List<Vector3> positions;
    List<GameObject> objects;

    void Awake()
    {
        Messenger.AddListener(GlobalEvents.PLAYERS_DEATH, Reset);
        Messenger.AddListener(GlobalEvents.SPAWN, Spawn);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GlobalEvents.PLAYERS_DEATH, Reset);
        Messenger.RemoveListener(GlobalEvents.SPAWN, Spawn);
    }

    void Start()
    {
        objects = new List<GameObject>();
        number = positions.Count;
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < number; i++)
        {
            objects.Add(Instantiate(prefab, positions[i], Quaternion.identity));
        }
    }

    void Reset()
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
        Spawn();
    }
}
