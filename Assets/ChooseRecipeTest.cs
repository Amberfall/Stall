using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRecipeTest : MonoBehaviour
{



    List<ArrowTest> arrows = new List<ArrowTest>();

    PlayerController player;

    int currentComboNum;


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent<ArrowTest>(out ArrowTest arrow))
            {
                arrows.Add(arrow);
            }
        }

        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        player.pressedArrow += CheckCombo;
    }

    private void OnDisable()
    {
        player.pressedArrow -= CheckCombo;
    }


    void CheckCombo(Vector2 direction)
    {
        if(arrows[currentComboNum].dir == direction)
        {
            arrows[currentComboNum].ArrowChosen();
            currentComboNum++;
        }
        else
        {
            foreach (ArrowTest arrow in arrows)
            {
                arrow.ResetArrow();
            }
            currentComboNum = 0;
        }

        if(currentComboNum == arrows.Count)
        {
            Debug.Log("Chosen Recipe");
            foreach (ArrowTest arrow in arrows)
            {
                //arrow.ResetArrow();
            }
            currentComboNum = 0;
        }
    }

}
