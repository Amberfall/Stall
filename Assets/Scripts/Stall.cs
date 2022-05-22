using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stall : MonoBehaviour
{
    [SerializeField] List<Stool> stools;
    [SerializeField] List<CounterSpot> counterSpots;
    public List<Stool> ListOpenStools()
    {
        List<Stool> openStools = new List<Stool>();
        for(int i = 0; i < stools.Count; i++)
        {
            if(stools[i].IsOccupied() != true)
            {
                openStools.Add(stools[i]);
            }
        }
        return openStools;
    }
    public Transform SeatMe(Customer customer)
    {
        Debug.Log("Customer requesting a seat...");
        List<Stool> openStools = ListOpenStools();
        if(openStools.Count == 0)
        {
            Debug.Log("could not find an open seat.");
            return null;
        }
        else
        {
            int randomIndex = Random.Range(0, openStools.Count);
            Debug.Log("random int is " + randomIndex);
            Debug.Log("Seating customer at seat # " + (randomIndex + 1) + ".");
            openStools[randomIndex].SeatCustomer(customer);
            return openStools[randomIndex].transform;
        }
    }
    public void RemoveMe(Customer customer)
    {
        Debug.Log("A customer is done eating...");
        for(int i = 0; i < stools.Count; i++)
        {
            if(stools[i].GetCustomer() == customer)
            {
                stools[i].RemoveCustomer();
                Debug.Log("customer from seat " + i + 1 + " is leaving.");
            }
        }
    }
    
    public int CheckFoodsForIngredient(Ingredient ingredient)
    {
        for(int i = 0; i < counterSpots.Count; i ++)
        {
            Food foodToCheck = counterSpots[i].CheckFood();
            if(foodToCheck  != null)
            {
                if(foodToCheck.ContainsIngredient(ingredient))
                {
                    Debug.Log("Found the right food!");
                    return i; //probably change this to calling a function to give the food to the customer
                }
                
            }
        }
        Debug.Log("could not find the right food...");
        return -1;
    }
}
