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
        serveTimer = 120 - (customersFed * 3);
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
        IncreaseSpawning(26, 5);

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
