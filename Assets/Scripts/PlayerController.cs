using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameEvent onNormalMovement;
    [SerializeField] GameEvent onChoosingCookAction;
    [SerializeField] GameEvent onChoosingIngredients;
    [SerializeField] GameEvent onCooking;
    [SerializeField] GameEvent onCanThrowAway;
    [SerializeField] GameEvent onThrowingAway;




    [SerializeField] float movementSpeed;
    [SerializeField] float timeToThrowAwayFood;
    float throwTimer;

    [SerializeField] List<Transform> stallPositions = new List<Transform>();

    public delegate void PressedActionArrow(ComboArrow.Direction direction);
    public PressedActionArrow pressedActionArrow;

    public delegate void PressedIngredientArrow(ComboArrow.Direction direction);
    public PressedIngredientArrow pressedIngredientArrow;

    public delegate void PressedCookingArrow(ComboArrow.Direction direction);
    public PressedCookingArrow pressedCookingArrow;


    public delegate void ReleasedSpace();
    public ReleasedSpace releasedSpace;

    Stall stall;

    Vector3 refVelo;
    [HideInInspector] public int stallInt;

    public enum PlayerState { Moving, ChooseAction, ChooseIngredient, Cooking, ThrowingFood};
    public PlayerState playerState;

    private void Awake()
    {
        stall = FindObjectOfType<Stall>();
        stallInt = 1;
    }

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
            case PlayerState.Cooking:
                CookInput();
                break;
            case PlayerState.ThrowingFood:
                ThrowAwayFood();
                break;
            default:
                break;
        }


        Movement();

        PlayerStateEvents();
    }

    bool isMoving;
    bool isChoosingAction;
    bool isChoosingIngredient;
    bool isCooking;
    bool isThrowingFood;
    bool canThrowFood;
    bool canThrowFoodEventBool;
    void PlayerStateEvents()
    {
        switch (playerState)
        {
            case PlayerState.Moving:
                if (canThrowFood)
                {
                    CanThrowFoodEventCall();
                }
                else
                {
                    IsMovingEventCall();
                }
                break;
            case PlayerState.ChooseAction:
                IsChooseActionEventCall();
                break;
            case PlayerState.ChooseIngredient:
                IsChoosingIngredientEventCall();
                break;
            case PlayerState.Cooking:
                IsCookingEventCall();
                break;
            case PlayerState.ThrowingFood:
                IsThrowingFoodEventCall();
                break;
            default:
                break;
        }
    }


    void IsMovingEventCall()
    {
        if(!isMoving)
        {
            onNormalMovement.Raise();
            isMoving = true;
        }
        isChoosingAction = false;
        isChoosingIngredient = false;
        isCooking = false;
        isThrowingFood = false;
        canThrowFoodEventBool = false;
    }
    void IsChoosingIngredientEventCall()
    {
        if (!isChoosingIngredient)
        {
            onChoosingIngredients.Raise();
            isChoosingIngredient = true;
        }
        isChoosingAction = false;
        isMoving = false;
        isCooking = false;
        isThrowingFood = false;
        canThrowFoodEventBool = false;
    }
    void IsChooseActionEventCall()
    {
        if (!isChoosingAction)
        {
            onChoosingCookAction.Raise();
            isChoosingAction = true;
        }
        isMoving = false;
        isChoosingIngredient = false;
        isCooking = false;
        isThrowingFood = false;
        canThrowFoodEventBool = false;
    }
    void IsCookingEventCall()
    {
        if (!isCooking)
        {
            onCooking.Raise();
            isCooking = true;
        }
        isChoosingAction = false;
        isChoosingIngredient = false;
        isMoving = false;
        isThrowingFood = false;
        canThrowFoodEventBool = false;
    }
    void IsThrowingFoodEventCall()
    {
        if (!isThrowingFood)
        {
            onThrowingAway.Raise();
            isThrowingFood = true;
        }
        isChoosingAction = false;
        isChoosingIngredient = false;
        isCooking = false;
        isMoving = false;
        canThrowFoodEventBool = false;
    }
    void CanThrowFoodEventCall()
    {
        if (!canThrowFoodEventBool)
        {
            onCanThrowAway.Raise();
            canThrowFoodEventBool = true;
        }

        isChoosingAction = false;
        isChoosingIngredient = false;
        isCooking = false;
        isMoving = false;
        isThrowingFood = false;
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

    void CookInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            pressedCookingArrow.Invoke(ComboArrow.Direction.Right);
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            pressedCookingArrow.Invoke(ComboArrow.Direction.Left);
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            pressedCookingArrow.Invoke(ComboArrow.Direction.Up);
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            pressedCookingArrow.Invoke(ComboArrow.Direction.Down);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
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
            if (stall.counterSpots[stallInt].HasFood())
            {
                throwTimer = timeToThrowAwayFood;
                playerState = PlayerState.ThrowingFood;
            }
            else
            {
                playerState = PlayerState.ChooseAction;
            }
        }

        if (stall.counterSpots[stallInt].HasFood())
        {
            canThrowFood = true;
        }
        else
        {
            canThrowFood = false;
        }

    }

    void Movement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, stallPositions[stallInt].position, ref refVelo, movementSpeed * Time.deltaTime);
    }


    void ThrowAwayFood()
    {
        if(throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
        }
        else
        {
            stall.counterSpots[stallInt].RemoveFood();
            playerState = PlayerState.Moving;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            playerState = PlayerState.Moving;
        }

    }
}
