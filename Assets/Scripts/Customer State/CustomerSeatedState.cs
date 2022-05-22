using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSeatedState : CustomerBaseState
{
    public override void EnterState(CustomerStateManager customer)
    {
        Debug.Log("I have sat down!");
    }
    public override void UpdateState(CustomerStateManager customer)
    {
        
    }
}
