using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodComboBuilder : MonoBehaviour
{

    PlayerController player;

    ArrowCooking arrowCooking;

    CookAction currentCookAction;
    Ingredient firstIngredient;
    Ingredient secondsIngredient;

    int recipeNum;
    int ingredientCombo;

    [SerializeField] List<Food> availableFoods = new List<Food>();


    [SerializeField] Sprite leftArrow;
    [SerializeField] Sprite rightArrow;
    [SerializeField] Sprite upArrow;
    [SerializeField] Sprite downArrow;

    List<ActionChooser> actionChoosers = new List<ActionChooser>();
    List<IngredientChooser> ingredientChoosers = new List<IngredientChooser>();

    public Transform arrowPositions;
    List<Image> arrowImages = new List<Image>();

    public Transform textPositions;
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        arrowCooking = FindObjectOfType<ArrowCooking>();

        foreach (Transform child in arrowPositions.transform)
        {
            arrowImages.Add(child.GetComponent<Image>());
        }

        foreach (Transform child in textPositions.transform)
        {
            texts.Add(child.GetComponent<TextMeshProUGUI>());
        }

        foreach (ActionChooser actionChooser in FindObjectsOfType<ActionChooser>())
        {
            actionChoosers.Add(actionChooser);
        }

        foreach (IngredientChooser ingredientChooser in FindObjectsOfType<IngredientChooser>())
        {
            ingredientChoosers.Add(ingredientChooser);
        }

        ResetRecipeBuilder();
    }

    private void OnEnable()
    {
        player.pressedActionArrow += PressedActionArrow;
        player.pressedIngredientArrow += PressedIngredientArrow;
        player.releasedSpace += ReleasedSpace;
    }

    private void OnDisable()
    {
        player.pressedActionArrow -= PressedActionArrow;
        player.pressedIngredientArrow -= PressedIngredientArrow;
        player.releasedSpace -= ReleasedSpace;

    }


    void ReleasedSpace()
    {
        bool isARecipe = false;

        if(recipeNum >= 4)
        {
            if(secondsIngredient == null)
            {
                foreach (Food food in availableFoods)
                {
                    if(food.ingredients.Count == 1)
                    {
                        if (food.cookAction == currentCookAction)
                        {
                            if (food.ingredients[0] == firstIngredient)
                            {
                                isARecipe = true;
                                player.playerState = PlayerController.PlayerState.Cooking;
                                Debug.Log("Cook " + food.name);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Food food in availableFoods)
                {
                    if (food.cookAction == currentCookAction)
                    {
                        if (food.ingredients[0] == firstIngredient)
                        {
                            if(food.ingredients[1] == secondsIngredient)
                            {
                                isARecipe = true;
                                player.playerState = PlayerController.PlayerState.Cooking;
                                arrowCooking.ActivateRandomArrow();
                                Debug.Log("Cook " + food.name);
                                ResetRecipeBuilder();
                            }
                        }
                    }
                }
            }

            if (!isARecipe)
            {
                Debug.Log("This is not a recipe! You crazy!");
                ResetRecipeBuilder();
                player.playerState = PlayerController.PlayerState.Moving;
            }

        }
        else
        {
            ResetRecipeBuilder();
            player.playerState = PlayerController.PlayerState.Moving;
        }
    }

    void PressedActionArrow(ComboArrow.Direction direction)
    {
        foreach (ActionChooser actionChooser in actionChoosers)
        {
            if (actionChooser.CheckCombo(direction))
            {
                switch (direction)
                {
                    case ComboArrow.Direction.Left:
                        arrowImages[recipeNum].sprite = leftArrow;
                        arrowImages[recipeNum].gameObject.SetActive(true);
                        recipeNum++;
                        currentCookAction = actionChooser.cookAction;
                        texts[0].text = actionChooser.cookAction.name;
                        Debug.Log("CookAction is " + actionChooser.cookAction.name);
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ComboArrow.Direction.Right:
                        arrowImages[recipeNum].sprite = rightArrow;
                        arrowImages[recipeNum].gameObject.SetActive(true);
                        recipeNum++;
                        currentCookAction = actionChooser.cookAction;
                        texts[0].text = actionChooser.cookAction.name;
                        Debug.Log("CookAction is " + actionChooser.cookAction.name);
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ComboArrow.Direction.Up:
                        arrowImages[recipeNum].sprite = upArrow;
                        arrowImages[recipeNum].gameObject.SetActive(true);
                        recipeNum++;
                        currentCookAction = actionChooser.cookAction;
                        texts[0].text = actionChooser.cookAction.name;
                        Debug.Log("CookAction is " + actionChooser.cookAction.name);
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ComboArrow.Direction.Down:
                        arrowImages[recipeNum].sprite = downArrow;
                        arrowImages[recipeNum].gameObject.SetActive(true);
                        recipeNum++;
                        currentCookAction = actionChooser.cookAction;
                        texts[0].text = actionChooser.cookAction.name;
                        Debug.Log("CookAction is " + actionChooser.cookAction.name);
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    default:
                        break;
                }
            }
        }
    }


    void PressedIngredientArrow(ComboArrow.Direction direction)
    {
        bool doReset = true;

        recipeNum++;
        ingredientCombo++;

        if(arrowImages.Count >= recipeNum)
        {
            foreach (IngredientChooser ingredientChooser in ingredientChoosers)
            {
                if (ingredientChooser.CheckCombo(direction, ingredientCombo))
                {
                    switch (direction)
                    {
                        case ComboArrow.Direction.Left:
                            CheckIngredient(leftArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ComboArrow.Direction.Right:
                            CheckIngredient(rightArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ComboArrow.Direction.Up:
                            CheckIngredient(upArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ComboArrow.Direction.Down:
                            CheckIngredient(downArrow, ingredientChooser);
                            doReset = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (doReset)
            {
                ResetRecipeBuilder();
                player.playerState = PlayerController.PlayerState.ChooseAction;
            }
        }


    }


    void CheckIngredient(Sprite spriteDir, IngredientChooser ingredChooser)
    {
        arrowImages[recipeNum - 1].sprite = spriteDir;
        arrowImages[recipeNum - 1].gameObject.SetActive(true);

        if (ingredientCombo == 3)
        {
            if (firstIngredient == null)
            {
                firstIngredient = ingredChooser.ingredient;
                texts[1].text = ingredChooser.ingredient.name;

            }
            else
            {
                secondsIngredient = ingredChooser.ingredient;
                texts[2].text = ingredChooser.ingredient.name;

            }

            Debug.Log("Added " + ingredChooser.ingredient.name);
            ingredientCombo = 0;
            foreach (IngredientChooser ingredientChooser in ingredientChoosers)
            {
                ingredientChooser.currentComboNum = 0;
            }
        }
    }

    void ResetRecipeBuilder()
    {
        foreach (Image arrowImage in arrowImages)
        {
            arrowImage.gameObject.SetActive(false);
        }

        foreach (IngredientChooser ingredientChooser in ingredientChoosers)
        {
            ingredientChooser.currentComboNum = 0;
        }
        recipeNum = 0;
        ingredientCombo = 0;
        currentCookAction = null;
        firstIngredient = null;
        secondsIngredient = null;
        foreach (TextMeshProUGUI text in texts)
        {
            text.text = null;
        }
    }

}
