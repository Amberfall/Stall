using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChooser : MonoBehaviour
{

    List<ComboArrow> arrows = new List<ComboArrow>();

    public CookAction cookAction;

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



    public bool CheckCombo(ComboArrow.Direction direction)
    {
        List<ComboArrow> tempList = new List<ComboArrow>();

        foreach (ComboArrow arrow in arrows)
        {
            if(arrow.direction == direction)
            {
                tempList.Add(arrow);
            }
        }

        if(tempList.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
