using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLostState : CustomerBaseState
{
     public override void EnterState(CustomerStateManager customerStateManager)
    {
        customerStateManager.customer.onLost.Invoke();
        customerStateManager.stall.RemoveMe(customerStateManager.customer);

    }
    public override void UpdateState(CustomerStateManager customerStateManager)
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
