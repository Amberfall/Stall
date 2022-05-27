using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerUnseatedState : CustomerBaseState
{
    Transform targetSeat = null;

    bool hasStoppedWalking;
    Vector2 moveToPos;
    float moveTimer;
    bool hasEnteredSpawnArea;
    bool move;

    public override void EnterState(CustomerStateManager customerStateManager)
    {
        Debug.Log("new customer approaches...");
        customerStateManager.customer.onUnseated.Invoke();
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

                if (!hasEnteredSpawnArea)
                {
                    if (!customerStateManager.customer.wanderAroundArea.OverlapPoint(customerStateManager.transform.position))
                    {
                        if(customerStateManager.transform.position.x > customerStateManager.stall.transform.position.x)
                        {
                            customerStateManager.customer.spriteSpriteRenderer.flipX = true;
                        }
                        else
                        {
                            customerStateManager.customer.spriteSpriteRenderer.flipX = false;

                        }

                        float step = customerStateManager.customer.walkSpeed * Time.deltaTime;
                        customerStateManager.transform.position = Vector2.MoveTowards(customerStateManager.transform.position, customerStateManager.stall.transform.position, step);
                    }
                    else
                    {
                        hasEnteredSpawnArea = true;
                    }
                }
                else
                {
                    if(moveTimer <= 0)
                    {

                        hasStoppedWalking = false;

                        move = false;
                        moveToPos = new Vector2(customerStateManager.transform.position.x + Random.Range(-4, 5), customerStateManager.transform.position.y + Random.Range(-4, 5));

                        if (customerStateManager.customer.wanderAroundArea.OverlapPoint(moveToPos))
                        {

                            if(moveToPos.x > customerStateManager.transform.position.x)
                            {
                                customerStateManager.customer.spriteSpriteRenderer.flipX = false;
                            }
                            else
                            {
                                customerStateManager.customer.spriteSpriteRenderer.flipX = true;

                            }
                            customerStateManager.customer.onStartWalking.Invoke();
                            move = true;
                            moveTimer = 3;
                        }
                        else
                        {
                            moveToPos = new Vector2(customerStateManager.transform.position.x + Random.Range(-4, 5), customerStateManager.transform.position.y + Random.Range(-4, 5));
                        }

                    }
                }

                if (move)
                {
                    float step = customerStateManager.customer.walkSpeed * Time.deltaTime;
                    customerStateManager.transform.position = Vector2.MoveTowards(customerStateManager.transform.position, moveToPos, step);

                    if(moveToPos == (Vector2)customerStateManager.transform.position)
                    {
                        if (!hasStoppedWalking)
                        {
                            customerStateManager.customer.onStoppedWalking.Invoke();
                            hasStoppedWalking = true;
                        }

                        moveTimer -= Time.deltaTime;

                    }

                }



                //and wander around.
            }
            //if timer runs out
        }
        else
        {
            if(customerStateManager.transform.position == targetSeat.position)
            {
                customerStateManager.SwitchState(customerStateManager.SeatedState);
            }
            //walk towards that seat, sit down and change to seated state. Don't update timer

            if (customerStateManager.transform.position.x > customerStateManager.stall.transform.position.x)
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = true;
            }
            else
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = false;

            }

            if (hasStoppedWalking)
            {
                customerStateManager.customer.onStartWalking.Invoke();
                hasStoppedWalking = false;
            }

            if (targetSeat.position.x > customerStateManager.transform.position.x)
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = false;
            }
            else
            {
                customerStateManager.customer.spriteSpriteRenderer.flipX = true;

            }

            float step = customerStateManager.customer.walkSpeed * Time.deltaTime;
            customerStateManager.transform.position = Vector2.MoveTowards(customerStateManager.transform.position, targetSeat.position, step);
        }



    }

    
}
