using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{

    public GameEvent onWonGame;

    public GameObject transformButton;

    public GoldTextScript goldText;

    public int currentGold;

    public RecipeButton recipeButton;

    public List<Transform> recipes = new List<Transform>();


    public List<float> recipeButtonYPositions = new List<float>();
    
    public int currentRecipe;

    public int totalGoldHad;


    public void EarnGoldFromCustomer()
    {
        totalGoldHad += 10;
        currentGold += 10;
        goldText.SetGoldTo(currentGold);
    }

    public void TryBuyRecipe()
    {
        if(currentRecipe < 4)
        {
            if (recipeButton.price <= currentGold)
            {
                recipes[currentRecipe].gameObject.SetActive(true);
                recipeButton.rectTrans.anchoredPosition = new Vector2(3, recipeButtonYPositions[currentRecipe]);

                if (currentRecipe == 0)
                {
                    currentGold -= recipeButton.price;
                    goldText.SetGoldTo(currentGold);
                    recipeButton.price = 20;
                    recipeButton.theText.text = "Buy Recipe " + recipeButton.price.ToString() + " G";

                }

                if (currentRecipe == 1)
                {
                    currentGold -= recipeButton.price;
                    goldText.SetGoldTo(currentGold);
                    recipeButton.price = 20;
                    recipeButton.theText.text = "Buy Recipe " + recipeButton.price.ToString() + " G";

                }

                if (currentRecipe == 2)
                {
                    currentGold -= recipeButton.price;
                    goldText.SetGoldTo(currentGold);
                    recipeButton.price = 20;
                    recipeButton.theText.text = "Buy Recipe " + recipeButton.price.ToString() + " G";

                }

                if (currentRecipe == 3)
                {
                    currentGold -= recipeButton.price;
                    goldText.SetGoldTo(currentGold);
                    recipeButton.price = 20;
                    recipeButton.theText.text = "Buy Recipe " + recipeButton.price.ToString() + " G";
                    transformButton.SetActive(true);


                }

                currentRecipe++;
                return;

            }
            else
            {
                Debug.Log("Cant afford");
            }
        }




        if (currentRecipe == 4)
        {
            if(currentGold >= 100)
            {
                currentGold -= 100;
                goldText.SetGoldTo(currentGold);
                onWonGame.Raise();
                transformButton.SetActive(false);
                Debug.Log("Won Game");
            }

        }


    }

}
