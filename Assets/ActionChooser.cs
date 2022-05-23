using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChooser : MonoBehaviour
{

    List<ArrowTest> arrows = new List<ArrowTest>();

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



    public bool CheckCombo(ArrowTest.Direction direction)
    {
        List<ArrowTest> tempList = new List<ArrowTest>();

        foreach (ArrowTest arrow in arrows)
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
