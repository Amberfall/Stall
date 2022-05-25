using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatSheetTest : MonoBehaviour
{

    [SerializeField] float speed;
    PlayerController player;
    bool isShowing;
    [SerializeField] Vector3 notShowingPos;
    [SerializeField] Vector3 isShowingPos;
    Vector3 refVelo;

    RectTransform rectTrans;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rectTrans = GetComponent<RectTransform>();
    }

    private void Update()
    {
        switch (player.playerState)
        {
            case PlayerController.PlayerState.Moving:
                isShowing = false;
                break;
            case PlayerController.PlayerState.ChooseAction:
                isShowing = true;
                break;
            case PlayerController.PlayerState.ChooseIngredient:
                isShowing = true;
                break;
            case PlayerController.PlayerState.Cooking:
                isShowing = false;
                break;
            case PlayerController.PlayerState.ThrowingFood:
                isShowing = false;
                break;
            default:
                break;
        }


        if (isShowing)
        {
            rectTrans.position = Vector3.SmoothDamp(rectTrans.position, isShowingPos , ref refVelo, speed * Time.deltaTime);
        }
        else
        {
            rectTrans.position = Vector3.SmoothDamp(rectTrans.position, notShowingPos, ref refVelo, speed * Time.deltaTime);
        }

    }

}
