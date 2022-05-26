using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLostState : CustomerBaseState
{
     public override void EnterState(CustomerStateManager customerStateManager)
    {
        customerStateManager.customer.onLost.Invoke();
    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {
        customerStateManager.stall.RemoveMe(customerStateManager.customer);
        customerStateManager.customer.transform.Translate(Vector2.down * Time.deltaTime * 3);

    }
}
