using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    [SerializeField] List<Transform> stallPositions = new List<Transform>();

    public delegate void PressedActionArrow(ComboArrow.Direction direction);
    public PressedActionArrow pressedActionArrow;

    public delegate void PressedIngredientArrow(ComboArrow.Direction direction);
    public PressedIngredientArrow pressedIngredientArrow;

    public delegate void ReleasedSpace();
    public ReleasedSpace releasedSpace;


    Vector3 refVelo;
    int stallInt;

    public enum PlayerState { Moving, ChooseAction, ChooseIngredient};
    public PlayerState playerState;



    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.Moving:
                MovementInput();
                break;
            case PlayerState.ChooseAction:
                ChooseActionInput();
                break;
            case PlayerState.ChooseIngredient:
                ChooseIngredientInput();
                break;
            default:
                break;
        }


        Movement();


    }

    void ChooseActionInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            pressedActionArrow.Invoke(ComboArrow.Direction.Right);
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            pressedActionArrow.Invoke(ComboArrow.Direction.Left);
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            pressedActionArrow.Invoke(ComboArrow.Direction.Up);
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            pressedActionArrow.Invoke(ComboArrow.Direction.Down);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            releasedSpace.Invoke();
        }
    }

    void ChooseIngredientInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            pressedIngredientArrow.Invoke(ComboArrow.Direction.Right);
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            pressedIngredientArrow.Invoke(ComboArrow.Direction.Left);
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            pressedIngredientArrow.Invoke(ComboArrow.Direction.Up);
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            pressedIngredientArrow.Invoke(ComboArrow.Direction.Down);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            releasedSpace.Invoke();
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
            playerState = PlayerState.ChooseAction;
        }
    }

    void Movement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, stallPositions[stallInt].position, ref refVelo, movementSpeed * Time.deltaTime);
    }

}
