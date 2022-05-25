using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFedState : CustomerBaseState
{
    bool hasGottenFood;
    float timeToEat;
    bool leaving;

    public override void EnterState(CustomerStateManager customerStateManager)
    {

    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {

        if (hasGottenFood)
        {
            timeToEat += Time.deltaTime;

            if(timeToEat > 1)
            {
                customerStateManager.customer.onFed.Invoke();
                customerStateManager.stall.RemoveMe(customerStateManager.customer);
                leaving = true;
            }
        }
        else
        {
            customerStateManager.customer.foodHolder.transform.localPosition =
            Vector2.MoveTowards(customerStateManager.customer.foodHolder.transform.localPosition, customerStateManager.customer.foodHolderStartPos, 5 * Time.deltaTime);

            if((Vector2)customerStateManager.customer.foodHolder.transform.localPosition == customerStateManager.customer.foodHolderStartPos)
            {
                hasGottenFood = true;
                customerStateManager.customer.onEating.Invoke();
            }
        }

        if (leaving)
        {
            customerStateManager.customer.foodHolder.sprite = null;
            customerStateManager.customer.transform.Translate(Vector2.right * Time.deltaTime * 3);
        }

    }


}
