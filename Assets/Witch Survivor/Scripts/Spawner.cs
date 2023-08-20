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
            Spawn(0);
            timer = 0f;
        }
    }

    private void Spawn(int level)
    {
        SpawnData enemyData = GameManager.Instance.GetSpawnData(level);
        GameObject newEnemy = PoolManager.Instance.GetObject(enemyData.objectName);
        if(newEnemy != null)
        {
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
            if(enemyComponent != null)
            {
                enemyComponent.Init(enemyData);
            }
            newEnemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;
        }
        
    }
}

[System.Serializable]
public class SpawnData
{
    public string objectName;
    public float spawnTime;    
    public int health;
    public float speed;
}
