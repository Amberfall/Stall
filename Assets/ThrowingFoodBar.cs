using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowingFoodBar : MonoBehaviour
{
    Image theImage;

    PlayerController player;

    private void Awake()
    {
        theImage = GetComponent<Image>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        theImage.fillAmount = player.throwTimer / player.timeToThrowAwayFood;
    }
}
