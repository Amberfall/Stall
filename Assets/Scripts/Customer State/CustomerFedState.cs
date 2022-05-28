using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFedState : CustomerBaseState
{
    bool hasGottenFood;
    float timeToEat;
    bool leaving;
    bool hasBeenFed;

    float timeToTransform;
    bool hasTransformed;

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
                    customerStateManager.customer.foodHolder.sprite = null;
                    customerStateManager.customer.onFed.Invoke();
                    hasBeenFed = true;
                }
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


            timeToTransform += Time.deltaTime;
            if(timeToTransform > 0.5f)
            {
                if (!hasTransformed)
                {
                    customerStateManager.customer.onHasTransformed.Invoke();
                    customerStateManager.stall.RemoveMe(customerStateManager.customer);
                    hasTransformed = true;
                }

            }


            if (hasTransformed)
            {
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


}
