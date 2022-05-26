using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCooking : MonoBehaviour
{

    public Food currentFood;

    Stall stall;

    PlayerController player;

    [SerializeField] Transform arrowPositionParent;

    List<Transform> arrowPositions = new List<Transform>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();

        stall = FindObjectOfType<Stall>();

        foreach (Transform child in arrowPositionParent)
        {
            arrowPositions.Add(child);
        }

        DeactivateArrows();
    }

    private void OnEnable()
    {
        player.pressedCookingArrow += HitArrow;
    }

    private void OnDisable()
    {
        player.pressedCookingArrow -= HitArrow;
    }

    public void ActivateRandomArrow()
    {
        DeactivateArrows();

        for (int i = 0; i < arrowPositions.Count; i++)
        {
            arrowPositions[i].transform.GetChild(Random.Range(0, 4)).gameObject.SetActive(true);
        }

    }

    void DeactivateArrows()
    {
        foreach (Transform child in arrowPositions)
        {
            for (int i = 0; i < arrowPositions.Count; i++)
            {
                arrowPositions[i].GetChild(0).gameObject.SetActive(false);
                arrowPositions[i].GetChild(0).GetChild(0).gameObject.SetActive(false);
                arrowPositions[i].GetChild(1).gameObject.SetActive(false);
                arrowPositions[i].GetChild(1).GetChild(0).gameObject.SetActive(false);
                arrowPositions[i].GetChild(2).gameObject.SetActive(false);
                arrowPositions[i].GetChild(2).GetChild(0).gameObject.SetActive(false);
                arrowPositions[i].GetChild(3).gameObject.SetActive(false);
                arrowPositions[i].GetChild(3).GetChild(0).gameObject.SetActive(false);
            }
        }

    }

    int arrowCount;

    void HitArrow(ComboArrow.Direction direction)
    {
        bool hasFailed = true;

        foreach (Transform child in arrowPositions[arrowCount].transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                if(child.GetComponent<ComboArrow>().direction == direction)
                {
                    child.transform.GetChild(0).gameObject.SetActive(true);
                    arrowCount++;
                    if(arrowCount == arrowPositions.Count)
                    {
                        stall.counterSpots[player.stallInt].SetFood(currentFood);
                        player.playerState = PlayerController.PlayerState.Moving;
                        DeactivateArrows();
                        arrowCount = 0;
                        currentFood = null;
                    }
                    hasFailed = false;
                }
            }
        }

        if (hasFailed)
        {
            player.onFailedArrowCooking.Raise();
            DeactivateArrows();
            ActivateRandomArrow();
            arrowCount = 0;
        }

    }

}
