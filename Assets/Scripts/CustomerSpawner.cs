using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{

    public int firstLanternCustomers;
    public int secondLanternCustomers;
    public int thirdLanternCustomers;

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

    GoldSystem goldSystem;


    float serveTime;

    private void Awake()
    {
        goldSystem = FindObjectOfType<GoldSystem>();
    }

    void Start()
    {
        serveTime = 200;
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


            if (goldSystem.totalGoldHad <= 20)
            {
                Debug.Log("Eggspawn");
                GameObject instantiatedObject;
                int randomSoulIndex = 0;
                int randomIndex = Random.Range(0, randomSpawnPositions.Count);
                instantiatedObject = Instantiate(ChooseRandomCustomer(randomSoulIndex), randomSpawnPositions[randomIndex].position, Quaternion.identity);
                instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
                instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
                amountOfCustomers++;
            }

            if (goldSystem.totalGoldHad > 20 && goldSystem.totalGoldHad <= 50)
            {
                GameObject instantiatedObject;
                int randomSoulIndex = Random.Range(1, 2);
                Debug.Log(randomSoulIndex);
                int randomIndex = Random.Range(0, randomSpawnPositions.Count);
                instantiatedObject = Instantiate(ChooseRandomCustomer(randomSoulIndex), randomSpawnPositions[randomIndex].position, Quaternion.identity);
                instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
                instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
                amountOfCustomers++;
            }

            if (goldSystem.totalGoldHad > 50 && goldSystem.totalGoldHad <= 70)
            {
                GameObject instantiatedObject;
                int randomSoulIndex = Random.Range(0, 3);
                int randomIndex = Random.Range(0, randomSpawnPositions.Count);
                instantiatedObject = Instantiate(ChooseRandomCustomer(randomSoulIndex), randomSpawnPositions[randomIndex].position, Quaternion.identity);
                instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
                instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
                amountOfCustomers++;
            }

            if (goldSystem.totalGoldHad > 70 && goldSystem.totalGoldHad <= 90)
            {
                GameObject instantiatedObject;
                int randomSoulIndex = Random.Range(0, 4);
                int randomIndex = Random.Range(0, randomSpawnPositions.Count);
                instantiatedObject = Instantiate(ChooseRandomCustomer(randomSoulIndex), randomSpawnPositions[randomIndex].position, Quaternion.identity);
                instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
                instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
                amountOfCustomers++;
            }



            if (goldSystem.totalGoldHad > 90)
            {
                GameObject instantiatedObject;
                int randomSoulIndex = Random.Range(0, customers.Count);
                int randomIndex = Random.Range(0, randomSpawnPositions.Count);
                instantiatedObject = Instantiate(ChooseRandomCustomer(randomSoulIndex), randomSpawnPositions[randomIndex].position, Quaternion.identity);
                instantiatedObject.GetComponent<Customer>().spawnPos = randomSpawnPositions[randomIndex].position;
                instantiatedObject.GetComponent<Customer>().timeWaiting = serveTime;
                amountOfCustomers++;
            }



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

    GameObject ChooseRandomCustomer(int maxRange)
    {
        return customers[maxRange];
        
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
