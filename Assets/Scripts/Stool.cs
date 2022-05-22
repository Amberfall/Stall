using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stool : MonoBehaviour
{
    Customer customer;

    public bool IsOccupied()
    {
        if(customer != null)
        {
            return true;
        }
        else return false;
    }

    public Customer GetCustomer()
    {
        return customer;
    }
    public void SeatCustomer(Customer newCustomer)
    {
        customer = newCustomer;
    }
    public void RemoveCustomer()
    {
        customer = null;
    }
}
