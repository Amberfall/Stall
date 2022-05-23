using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBuilder : MonoBehaviour
{

    PlayerController player;

    public List<Ingredient> currentIngredients = new List<Ingredient>();

    public int recipeNum;
    public int ingredientCombo;

    [SerializeField] Sprite leftArrow;
    [SerializeField] Sprite rightArrow;
    [SerializeField] Sprite upArrow;
    [SerializeField] Sprite downArrow;

    [SerializeField] List<ActionChooser> actionChoosers = new List<ActionChooser>();
    [SerializeField] List<IngredientChooser> ingredientChoosers = new List<IngredientChooser>();


    [SerializeField] List<Image> arrowImages = new List<Image>();


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();

        foreach (Transform child in transform)
        {
            arrowImages.Add(child.GetComponent<Image>());
        }

        foreach (ActionChooser actionChooser in FindObjectsOfType<ActionChooser>())
        {
            actionChoosers.Add(actionChooser);
        }

        foreach (IngredientChooser ingredientChooser in FindObjectsOfType<IngredientChooser>())
        {
            ingredientChoosers.Add(ingredientChooser);
        }
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
        if(recipeNum >= 4)
        {
            Debug.Log("Check if recipe and start coooookiing!");

            //Change this!
            ResetRecipeBuilder();
            player.playerState = PlayerController.PlayerState.Moving;
        }
        else
        {
            ResetRecipeBuilder();
            player.playerState = PlayerController.PlayerState.Moving;
        }
    }

    void PressedActionArrow(ArrowTest.Direction direction)
    {
        foreach (ActionChooser actionChooser in actionChoosers)
        {
            if (actionChooser.CheckCombo(direction))
            {
                switch (direction)
                {
                    case ArrowTest.Direction.Left:
                        arrowImages[recipeNum].sprite = leftArrow;
                        recipeNum++;
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ArrowTest.Direction.Right:
                        arrowImages[recipeNum].sprite = rightArrow;
                        recipeNum++;
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ArrowTest.Direction.Up:
                        arrowImages[recipeNum].sprite = upArrow;
                        recipeNum++;
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    case ArrowTest.Direction.Down:
                        arrowImages[recipeNum].sprite = downArrow;
                        recipeNum++;
                        player.playerState = PlayerController.PlayerState.ChooseIngredient;
                        break;
                    default:
                        break;
                }
            }
        }
    }


    void PressedIngredientArrow(ArrowTest.Direction direction)
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
                        case ArrowTest.Direction.Left:
                            CheckIngredient(leftArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ArrowTest.Direction.Right:
                            CheckIngredient(rightArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ArrowTest.Direction.Up:
                            CheckIngredient(upArrow, ingredientChooser);
                            doReset = false;
                            break;
                        case ArrowTest.Direction.Down:
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
            }
        }


    }


    void CheckIngredient(Sprite spriteDir, IngredientChooser ingredChooser)
    {
        arrowImages[recipeNum - 1].sprite = spriteDir;

        if (ingredientCombo == 3)
        {
            currentIngredients.Add(ingredChooser.ingredient);
            Debug.Log("Added" + ingredChooser.ingredient.name);
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
            arrowImage.sprite = null;
        }

        foreach (IngredientChooser ingredientChooser in ingredientChoosers)
        {
            ingredientChooser.currentComboNum = 0;
        }
        recipeNum = 0;
        ingredientCombo = 0;
        currentIngredients.Clear();
        player.playerState = PlayerController.PlayerState.ChooseAction;
    }

}
