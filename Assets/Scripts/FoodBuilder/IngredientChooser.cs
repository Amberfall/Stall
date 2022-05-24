using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChooser : MonoBehaviour
{

    public Ingredient ingredient;

    List<ComboArrow> arrows = new List<ComboArrow>();

    [HideInInspector] public int currentComboNum;


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent(out ComboArrow arrow))
            {
                arrows.Add(arrow);
            }
        }
    }

    public bool CheckCombo(ComboArrow.Direction direction, int comboNum)
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
