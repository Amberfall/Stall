using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;

    [SerializeField] List<Transform> randomSpawnPositions;
    
    public float timeToSpawn;

    public float currentSpawnDifficulty;

    bool startSpawnTimer;

    public int customersFed;
    public int customersLost;

    public int amountOfCustomers;

    int seatedCustomers;

    int serveTime;

    void Start()
    {
        serveTime = 100;
        ResetSpawnTime();
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
        instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
        if(serveTime > 15)
        {
            serveTime -= 2;
        }
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
        StartSpawningAt(2, 60);

        if (!startSpawnTimer)
        {
            SpawnCustomer();
        }



        IncreaseSpawning(4, 40);
        IncreaseSpawning(6, 30);
        IncreaseSpawning(8, 20);
        IncreaseSpawning(10, 15);
        IncreaseSpawning(12, 10);
        IncreaseSpawning(14, 8);
        IncreaseSpawning(16, 5);

        amountOfCustomers--;
        seatedCustomers--;
        if (customersFed > 2 && amountOfCustomers == 0)
        {
            SpawnCustomer();
        }
    }

    void IncreaseSpawning(int atNumOfFedCustomers, float spawnTime)
    {
        if (customersFed == atNumOfFedCustomers)
        {
            timeToSpawn -= spawnTime;
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
        seatedCustomers--;
        if (amountOfCustomers == 0)
        {
            SpawnCustomer();
        }

    }


    public void CustomerWasSeated()
    {
        seatedCustomers++;
    }

}
