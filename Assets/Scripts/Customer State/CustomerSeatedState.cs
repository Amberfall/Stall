using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSeatedState : CustomerBaseState
{
    public override void EnterState(CustomerStateManager customerStateManager)
    {
        customerStateManager.customer.onSeated.Invoke();

        Debug.Log("I have sat down!");
    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {
        customerStateManager.customer.spriteSpriteRenderer.flipX = false;


        foreach (CounterSpot counterSpot in customerStateManager.stall.counterSpots)
        {
            if (counterSpot.HasFood())
            {
                if (counterSpot.CheckFood().ContainsIngredient(customerStateManager.customer.GetIngredient()))
                {
                    Debug.Log("Take food");
                    customerStateManager.customer.foodHolder.transform.position = counterSpot.transform.position;
                    customerStateManager.customer.foodHolder.sprite = counterSpot.CheckFood().sprite;
                    counterSpot.RemoveFood();
                    customerStateManager.SwitchState(customerStateManager.FedState);
                }
            }
        }



    }
}
