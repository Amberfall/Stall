using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;

    [SerializeField] List<Transform> randomSpawnPositions;

    float timeToSpawn;

    float currentSpawnDifficulty;

    bool startSpawnTimer;

    public int customersFed;
    public int customersLost;

    public int amountOfCustomers;

    void Start()
    {
        //ResetSpawnTime();
        SpawnCustomer();
    }
    void Update()
    {
        if (startSpawnTimer)
        {
            if (timeToSpawn <= 0)
            {
                SpawnCustomer();
                ResetSpawnTime();
            }
            else
            {
                timeToSpawn -= Time.deltaTime;
            }
        }


    }

    void SpawnCustomer()
    {
        GameObject instantiatedObject;
        int randomIndex = Random.Range(0, randomSpawnPositions.Count);
        instantiatedObject = Instantiate(ChooseRandomCustomer(), randomSpawnPositions[randomIndex].position, Quaternion.identity);
        instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
        amountOfCustomers++;
    }


    void ChangeSpawnTime(float changeTo)
    {
        currentSpawnDifficulty = changeTo;
    }
    public void ResetSpawnTime()
    {
        timeToSpawn = currentSpawnDifficulty;
    }
    GameObject ChooseRandomCustomer()
    {
        int randomIndex = Random.Range(0, customers.Count);
        return customers[randomIndex];
        
    }

    public void CustomerGotFed()
    {
        customersFed++;

        StartSpawningAt(3, 60);

        if (!startSpawnTimer)
        {
            SpawnCustomer();
        }



        IncreaseSpawning(5, 50);
        IncreaseSpawning(8, 40);
        IncreaseSpawning(10, 30);
        IncreaseSpawning(12, 20);
        IncreaseSpawning(15, 10);
        IncreaseSpawning(17, 8);
        IncreaseSpawning(20, 5);

        amountOfCustomers--;

        if (customersFed > 3 && amountOfCustomers == 0)
        {
            SpawnCustomer();
        }
    }

    void IncreaseSpawning(int atNumOfFedCustomers, float spawnTime)
    {
        if (customersFed == atNumOfFedCustomers)
        {
            ChangeSpawnTime(spawnTime);
        }
    }

    void StartSpawningAt(int atNumOfFedCustomers, float spawnTime)
    {
        if (customersFed == atNumOfFedCustomers)
        {
            ChangeSpawnTime(spawnTime);
            startSpawnTimer = true;
        }
    }

    public void CustomerWasLost()
    {
        customersLost++;
        amountOfCustomers--;

        if (amountOfCustomers == 0)
        {
            SpawnCustomer();
        }

    }


}
