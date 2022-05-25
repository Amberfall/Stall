using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerUnseatedState : CustomerBaseState
{
    Transform targetSeat = null;
    float timeWaiting;
    public override void EnterState(CustomerStateManager customerStateManager)
    {
        Debug.Log("new customer approaches...");
        customerStateManager.customer.onUnseated.Invoke();
        timeWaiting = 0;      
    }
    public override void UpdateState(CustomerStateManager customerStateManager)
    {
        //if you haven't found a seat yet...
        if(targetSeat == null)
        {
            //look for a seat
            targetSeat = customerStateManager.stall.SeatMe(customerStateManager.customer);

            //if you still didn't find one..
            if(targetSeat == null)
            {
                //update the timer.
                timeWaiting += Time.deltaTime;
                //and wander around.
            }
            //if timer runs out
            if(timeWaiting >= customerStateManager.customer.GetWaitTime())
            {
                //change to lost state and leave.
                customerStateManager.SwitchState(customerStateManager.LostState);
            }
        }
        else
        {
            if(customerStateManager.transform.position == targetSeat.position)
            {
                customerStateManager.SwitchState(customerStateManager.SeatedState);
            }
            //walk towards that seat, sit down and change to seated state. Don't update timer
            float step = customerStateManager.customer.walkSpeed * Time.deltaTime;
            customerStateManager.transform.position = Vector2.MoveTowards(customerStateManager.transform.position, targetSeat.position, step);
        }
        
    }
}
