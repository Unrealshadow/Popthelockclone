using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject starPrefab;

    public DataHolder dataHolder;
    public PaddleMover paddleMover;

    private void Start()
    {
        Spawn();
        EventManager.instance.AddListener<DotHitEvent>(Spawn);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<DotHitEvent>(Spawn);
    }

    private void Spawn(object obj=null)
    {
        int angle = Random.Range(dataHolder.minSpawnAngle, dataHolder.maxSpawnAngle);
        GameObject spawnedObj =
            Instantiate(GetRandomObj(), paddleMover.transform.position, Quaternion.identity, transform);
        spawnedObj.transform.RotateAround(transform.position, Vector3.forward, (int)paddleMover.direction * angle);
    }

    private GameObject GetRandomObj()
    {
        if (Random.value > 0.2)
        {
            return dotPrefab;
        }
        else
        {
            return starPrefab;
        }
    }
}