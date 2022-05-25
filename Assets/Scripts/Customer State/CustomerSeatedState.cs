using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSeatedState : CustomerBaseState
{
    public override void EnterState(CustomerStateManager customerStateManager)
    {
        Debug.Log("I have sat down!");
    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {
        foreach (CounterSpot counterSpot in customerStateManager.stall.counterSpots)
        {
            if (counterSpot.HasFood())
            {
                if (counterSpot.CheckFood().ContainsIngredient(customerStateManager.customer.GetIngredient()))
                {
                    Debug.Log("Take food");
                    customerStateManager.foodHolder.transform.position = counterSpot.transform.position;
                    customerStateManager.foodHolder.sprite = counterSpot.CheckFood().sprite;
                    counterSpot.RemoveFood();
                    customerStateManager.SwitchState(customerStateManager.FedState);
                }
            }
        }


    }
}
