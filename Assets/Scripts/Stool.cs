using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stool : MonoBehaviour
{
    public Customer customer;
    public Image timerImage;
    public Image background;

    public Color startColor;
    public Color endColor;

    public GameEvent OnLoseGame;

    bool lostGame;

    bool wonGame;

    float timer;
    bool hasGottenTime;
    float fullTime;
    private void Update()
    {
        if (!wonGame)
        {
            if (customer != null)
            {
                if (customer.stateManager.currentState == customer.stateManager.SeatedState)
                {
                    if (!hasGottenTime)
                    {
                        timer = customer.timeWaiting;
                        fullTime = timer;
                        timerImage.enabled = true;
                        background.enabled = true;
                        hasGottenTime = true;
                    }
                    if (!lostGame)
                    {
                        timer -= Time.deltaTime;

                    }
                    else
                    {
                        timerImage.enabled = false;
                        background.enabled = false;

                    }
                }
                else
                {
                    timerImage.enabled = false;
                    background.enabled = false;
                    hasGottenTime = false;
                }

            }
            else
            {
                background.enabled = false;
                timerImage.enabled = false;
                hasGottenTime = false;
            }

            timerImage.fillAmount = timer / fullTime;
            timerImage.color = Vector4.Lerp(endColor, startColor, (timer / fullTime));
            if (timer < 0)
            {
                OnLoseGame.Raise();
                foreach (Stool stool in FindObjectsOfType<Stool>())
                {
                    stool.timerImage.enabled = false;
                    stool.background.enabled = false;
                    stool.lostGame = true;


                }
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


    public void WonTheGame()
    {
        wonGame = true;

        foreach (Stool stool in FindObjectsOfType<Stool>())
        {
            stool.timerImage.enabled = false;
            stool.background.enabled = false;

        }

    }
}
