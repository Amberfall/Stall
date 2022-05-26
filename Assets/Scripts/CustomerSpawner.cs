using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;

    [SerializeField] List<Transform> randomSpawnPositions;

    [SerializeField] float minSpawnTime = 15f;
    [SerializeField] float maxSpawnTime = 400f;
    float timeToSpawn;

    void Start()
    {
        ResetSpawnTime();
    }
    void Update()
    {
        if(timeToSpawn <= 0)
        {
            Instantiate(ChooseRandomCustomer(), ChooseRandomSpawnPosition().position, Quaternion.identity);
            ResetSpawnTime();
        }
        else
        {
            timeToSpawn -= Time.deltaTime;
        }

    }
    public void ResetSpawnTime()
    {
        timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
    GameObject ChooseRandomCustomer()
    {
        int randomIndex = Random.Range(0, customers.Count);
        return customers[randomIndex];
        
    }

    Transform ChooseRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, randomSpawnPositions.Count);
        return randomSpawnPositions[randomIndex];
    }

}
