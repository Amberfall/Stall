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
    [SerializeField] GameEvent onStoppedMoving;
    [SerializeField] GameEvent onStartMoving;
    public GameEvent onFailedArrowCooking;

    [SerializeField] GameEvent onWave;

    [SerializeField] SpriteRenderer spriteSpriteRenderer;

    [SerializeField] float movementSpeed;
    public float timeToThrowAwayFood;
    [HideInInspector] public float throwTimer;

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


        if(waveTimeIfMoving < 0)
        {
            waveTimeIfMoving += Time.deltaTime;
        }

        if (waveTimeIfCanThrowAway < 0)
        {
            waveTimeIfCanThrowAway += Time.deltaTime;
        }

    }


    void CanThrowFoodEventCall()
    {
        if (waveTimeIfCanThrowAway >= 0)
        {
            if (!canThrowFoodEventBool)
            {
                onCanThrowAway.Raise();
                canThrowFoodEventBool = true;
            }
        }



        if (canThrowFoodEventBool)
        {
            isChoosingAction = false;
            isChoosingIngredient = false;
            isCooking = false;
            isMoving = false;
            isThrowingFood = false;
            waveTimeIfMoving = 1;


        }


    }

    void IsMovingEventCall()
    {
        if(waveTimeIfMoving >= 0)
        {
            if (!isMoving)
            {
                onNormalMovement.Raise();
                isMoving = true;
            }
        }



        if (isMoving)
        {
            isChoosingAction = false;
            isChoosingIngredient = false;
            isCooking = false;
            isThrowingFood = false;
            canThrowFoodEventBool = false;
            waveTimeIfCanThrowAway = 1;
        }

    }
    void IsChoosingIngredientEventCall()
    {
        if (!isChoosingIngredient)
        {
            onChoosingIngredients.Raise();
            isChoosingIngredient = true;
            waveTimeIfMoving = 1;
            waveTimeIfCanThrowAway = 1;
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
            waveTimeIfMoving = 1;
            waveTimeIfCanThrowAway = 1;

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
            waveTimeIfMoving = 1;
            waveTimeIfCanThrowAway = 1;

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
            waveTimeIfMoving = 1;
            waveTimeIfCanThrowAway = 1;

        }
        isChoosingAction = false;
        isChoosingIngredient = false;
        isCooking = false;
        isMoving = false;
        canThrowFoodEventBool = false;
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
                spriteSpriteRenderer.flipX = false;
                onStartMoving.Raise();
                stallInt++;
            }
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (stallInt > 0)
            {
                spriteSpriteRenderer.flipX = true;
                onStartMoving.Raise();
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


    bool hasStopped;
    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, stallPositions[stallInt].position, movementSpeed * Time.deltaTime);

        if(transform.position == stallPositions[stallInt].position)
        {
            if (!hasStopped)
            {
                if(playerState == PlayerState.Moving || playerState == PlayerState.ChooseAction)
                {
                    onStoppedMoving.Raise();
                    hasStopped = true;
                }
            }
        }
        else
        {
            hasStopped = false;
        }

        //transform.position = Vector3.SmoothDamp(transform.position, stallPositions[stallInt].position, ref refVelo, movementSpeed * Time.deltaTime);
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

    float waveTimeIfMoving;
    float waveTimeIfCanThrowAway;

    public void OnSoulTransformed()
    {
        if(playerState == PlayerState.Moving)
        {
            if (canThrowFood == true)
            {
                Debug.Log("CanTHrowWave");
                waveTimeIfCanThrowAway = -1.5f;
                canThrowFoodEventBool = false;
                onWave.Raise();
                return;
            }

            if (canThrowFood == false)
            {
                waveTimeIfMoving = -1.5f;
                isMoving = false;
                onWave.Raise();
                return;
            }
        }


    }

}
