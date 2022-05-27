using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image customerTimeImage;

    float serveCustomerInTime;
    float serveTimer;

    bool hasCustomer;

    int seatedCustomers;

    void Start()
    {
        ResetServeTime();
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

        if (seatedCustomers > 0)
        {
            if (serveTimer < 0)
            {
                Debug.Log("Dead!");
            }
            else
            {
                serveTimer -= Time.deltaTime;
            }
        }

        customerTimeImage.fillAmount = serveTimer / serveCustomerInTime;


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

    void ResetServeTime()
    {
        serveTimer = 60 - (customersFed * 3);
        serveCustomerInTime = serveTimer;
    }
    GameObject ChooseRandomCustomer()
    {
        int randomIndex = Random.Range(0, customers.Count);
        return customers[randomIndex];
        
    }

    public void CustomerGotFed()
    {
        customersFed++;
        ResetServeTime();
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
        seatedCustomers--;
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
