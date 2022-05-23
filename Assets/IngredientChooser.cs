using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChooser : MonoBehaviour
{

    public Ingredient ingredient;

    List<ArrowTest> arrows = new List<ArrowTest>();

    public int currentComboNum;


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent(out ArrowTest arrow))
            {
                arrows.Add(arrow);
            }
        }
    }

    public bool CheckCombo(ArrowTest.Direction direction, int comboNum)
    {
        if(comboNum == currentComboNum + 1)
        {
            if (arrows[currentComboNum].direction == direction)
            {
                currentComboNum++;
                return true;
            }
            else
            {
                currentComboNum = 0;
                return false;
            }
        }
        else
        {
            currentComboNum = 0;
            return false;
        }

    }

}
