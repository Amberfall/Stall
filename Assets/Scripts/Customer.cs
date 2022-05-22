using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] float waitTime = 10f; //how long the customer will wait before leaving
    float timeWaiting; //how long the customer has been waiting
    [SerializeField] Ingredient missingIngredient;
    public float walkSpeed = 5f;
    public void AddTimeWaiting(float time) //increase timer for how long customer has been waiting
    {
        timeWaiting += time;
    }
    public void ResetTimeWaiting()
    {
        timeWaiting = 0;
    }
    public float GetWaitTime() //get how long the customer is willing to wait
    {
        return waitTime;
    }

}
