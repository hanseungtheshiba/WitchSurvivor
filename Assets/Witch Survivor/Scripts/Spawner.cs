using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    private float timer = 0f;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1f)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject enemy = PoolManager.Instance.GetObject("Officer");
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int index;
    public int health;
    public float speed;
}
