using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayArrowTest : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> arrowSprites = new List<SpriteRenderer>();

    [SerializeField] Sprite leftArrow;
    [SerializeField] Sprite rightArrow;
    [SerializeField] Sprite upArrow;
    [SerializeField] Sprite downArrow;

    PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }



}
