using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{

    public int firstLanternCustomers;
    public int secondLanternCustomers;
    public int thirdLanternCustomers;

    [SerializeField] GameEvent OnWonGame;
    [SerializeField] GameEvent OnFirstLanternLit;
    [SerializeField] GameEvent OnSecondLanternLit;
    [SerializeField] GameEvent OnThirdLanternLit;

    [SerializeField] List<GameObject> customers;

    [SerializeField] List<Transform> randomSpawnPositions;
    
    public float timeToSpawn;

    public float currentSpawnDifficulty;

    bool startSpawnTimer;

    public int customersFed;
    public int customersLost;

    public int amountOfCustomers;


    float serveTime;

    void Start()
    {
        serveTime = 10;
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

        if(amountOfCustomers == 0)
        {
            SpawnCustomer();
        }

    }

    void SpawnCustomer()
    {
        if(amountOfCustomers < 6)
        {
            GameObject instantiatedObject;
            int randomIndex = Random.Range(0, randomSpawnPositions.Count);
            instantiatedObject = Instantiate(ChooseRandomCustomer(), randomSpawnPositions[randomIndex].position, Quaternion.identity);
            instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
            instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
            amountOfCustomers++;
        }

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

        if(customersFed == firstLanternCustomers)
        {
            OnFirstLanternLit.Raise();
        }

        if(customersFed == secondLanternCustomers)
        {
            OnSecondLanternLit.Raise();
        }

        if(customersFed == thirdLanternCustomers)
        {
            OnThirdLanternLit.Raise();
            OnWonGame.Raise();
        }

        IncreaseSpawning(3, 30, 180);
        IncreaseSpawning(5, 15, 160);
        IncreaseSpawning(8, 10, 120);
        IncreaseSpawning(10, 8, 90);


        if (customersFed > 2 && amountOfCustomers == 0)
        {
            SpawnCustomer();
        }

        amountOfCustomers--;

    }

    void IncreaseSpawning(int atNumOfFedCustomers, float spawnTime, float servTime)
    {
        if (customersFed == atNumOfFedCustomers)
        {
            serveTime = servTime;
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
        if (amountOfCustomers == 0)
        {
            SpawnCustomer();
        }
        amountOfCustomers--;


    }


    public void CustomerWasSeated()
    {
    }

}
