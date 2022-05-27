using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public float timeToRead;
    public float timeBetweenDialogues;

    float readTimer;
    float timeBetweenTimer;
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    public List<Stool> stools = new List<Stool>();
    public List<GameObject> textBoxes = new List<GameObject>();


    int displayNumber;

    bool hasShownedHint;
    int randomHint;

    private void Start()
    {
        foreach (TextMeshProUGUI text in texts)
        {
            text.text = null;
        }

        foreach (GameObject gObj in textBoxes)
        {
            gObj.SetActive(false);
        }
    }


    private void Update()
    {
        DisplayDialogue(displayNumber);
    }


    void DisplayDialogue(int displayNum)
    {
        if (stools[displayNum].GetCustomer() != null)
        {
            texts[displayNum].text = null;
        }


        if (timeBetweenTimer < 0)
        {
            if (stools[displayNum].GetCustomer() != null)
            {
                Customer customer;
                customer = stools[displayNum].GetCustomer();
                Ingredient ingredient;
                ingredient = customer.GetIngredient();

                if (customer.stateManager.currentState == customer.stateManager.SeatedState)
                {
                    readTimer -= Time.deltaTime;
                    textBoxes[displayNum].SetActive(true);

                    if (!hasShownedHint)
                    {
                        randomHint = Random.Range(0, ingredient.hints.Count);
                        hasShownedHint = true;
                    }

                    texts[displayNum].text = ingredient.hints[randomHint];

                    if (readTimer < 0)
                    {
                        texts[displayNum].text = null;
                        textBoxes[displayNum].SetActive(false);
                        hasShownedHint = false;
                        if (displayNumber == 2)
                        {
                            displayNumber = 0;
                        }
                        else
                        {
                            displayNumber++;
                        }
                        readTimer = timeToRead;
                        timeBetweenTimer = timeBetweenDialogues;
                    }

                }
                else
                {
                    texts[displayNum].text = null;
                    textBoxes[displayNum].SetActive(false);

                    if (displayNumber == 2)
                    {
                        displayNumber = 0;
                    }
                    else
                    {
                        displayNumber++;
                    }
                    readTimer = timeToRead;
                    timeBetweenTimer = timeBetweenDialogues;
                }

            }
            else
            {
                if (displayNumber == 2)
                {
                    displayNumber = 0;
                }
                else
                {
                    displayNumber++;
                }
            }
        }
        else
        {
            timeBetweenTimer -= Time.deltaTime;
        }
       

    }


}
