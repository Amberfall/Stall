using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stool : MonoBehaviour
{
    public Customer customer;
    public Image timerImage;

    public GameEvent OnLoseGame;

    bool lostGame;

    float timer;
    bool hasGottenTime;
    float fullTime;
    private void Update()
    {
        if(customer != null)
        {
            if(customer.stateManager.currentState == customer.stateManager.SeatedState)
            {
                if (!hasGottenTime)
                {
                    timer = customer.timeWaiting;
                    fullTime = timer;
                    timerImage.enabled = true;
                    hasGottenTime = true;
                }
                if (!lostGame)
                {
                    timer -= Time.deltaTime;

                }
            }
        }
        else
        {
            timerImage.enabled = false;
            hasGottenTime = false;
        }

        timerImage.fillAmount = timer / fullTime;

        if(timer < 0)
        {
            OnLoseGame.Raise();
            foreach (Stool stool in FindObjectsOfType<Stool>())
            {
                stool.lostGame = true;
            }
        }
    }

    public bool IsOccupied()
    {
        if(customer != null)
        {
            return true;
        }
        else return false;
    }

    public Customer GetCustomer()
    {
        return customer;
    }
    public void SeatCustomer(Customer newCustomer)
    {
        customer = newCustomer;
    }
    public void RemoveCustomer()
    {
        customer = null;
    }
}
