using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFedState : CustomerBaseState
{
    bool hasGottenFood;
    float timeToEat;
    bool leaving;
    bool hasBeenFed;
    public override void EnterState(CustomerStateManager customerStateManager)
    {
        customerStateManager.customer.onGetFood.Invoke();

    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {

        if (hasGottenFood)
        {
            timeToEat += Time.deltaTime;

            if(timeToEat > 2.7f)
            {
                if (!hasBeenFed)
                {
                    customerStateManager.customer.onFed.Invoke();
                    hasBeenFed = true;
                }
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
            customerStateManager.transform.position = Vector2.MoveTowards(customerStateManager.transform.position, customerStateManager.customer.spawnPos,
                customerStateManager.customer.walkSpeed * Time.deltaTime);

            if (customerStateManager.transform.position.x > customerStateManager.customer.spawnPos.x)
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = true;
            }
            else
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = false;

            }
        }

    }


}
