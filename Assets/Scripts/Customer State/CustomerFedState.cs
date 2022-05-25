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
                leaving = true;
            }
        }
        else
        {
            customerStateManager.foodHolder.transform.localPosition =
            Vector2.MoveTowards(customerStateManager.foodHolder.transform.localPosition, customerStateManager.foodHolderStartPos, 5 * Time.deltaTime);

            if((Vector2)customerStateManager.foodHolder.transform.localPosition == customerStateManager.foodHolderStartPos)
            {
                hasGottenFood = true;
            }
        }

        if (leaving)
        {
            customerStateManager.foodHolder.sprite = null;
            customerStateManager.customer.transform.Translate(Vector2.right * Time.deltaTime * 5);
        }

    }


}
