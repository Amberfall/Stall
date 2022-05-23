using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    [SerializeField] List<Transform> stallPositions = new List<Transform>();

    public delegate void PressedArrow(Vector2 direction);
    public PressedArrow pressedArrow;

    Vector3 refVelo;
    int stallInt;

    enum PlayerState { Moving, ArrowCooking};
    PlayerState playerState;



    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.Moving:
                MovementInput();
                break;
            case PlayerState.ArrowCooking:
                ComboInput();
                break;
            default:
                break;
        }


        Movement();


    }

    void ComboInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            pressedArrow.Invoke(Vector2.right);
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            pressedArrow.Invoke(Vector2.left);
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            pressedArrow.Invoke(Vector2.up);
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            pressedArrow.Invoke(Vector2.down);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            playerState = PlayerState.Moving;
        }

    }

    void MovementInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (stallInt < 2)
            {
                stallInt++;
            }
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (stallInt > 0)
            {
                stallInt--;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            playerState = PlayerState.ArrowCooking;
        }
    }

    void Movement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, stallPositions[stallInt].position, ref refVelo, movementSpeed * Time.deltaTime);
    }

}
