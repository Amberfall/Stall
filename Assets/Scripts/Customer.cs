using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Customer : MonoBehaviour
{

    [SerializeField] float waitTime; //how long the customer will wait before leaving
    public float timeWaiting; //how long the customer has been waiting
    [SerializeField] Ingredient missingIngredient;
    public float walkSpeed = 5f;

    public SpriteRenderer foodHolder;
    [HideInInspector] public Vector2 foodHolderStartPos;

    [HideInInspector] public BoxCollider2D wanderAroundArea;

    public SpriteRenderer spriteSpriteRenderer;

    public UnityEvent onUnseated;
    public UnityEvent onSeated;
    public UnityEvent onEating;
    public UnityEvent onFed;
    public UnityEvent onLost;
    public UnityEvent onStoppedWalking;
    public UnityEvent onStartWalking;
    public UnityEvent onGetFood;

    [HideInInspector] public Vector2 spawnPos;

    public CustomerStateManager stateManager;

    private void Awake()
    {
        wanderAroundArea = FindObjectOfType<WanderAroundArea>().GetComponent<BoxCollider2D>();
        foodHolderStartPos = foodHolder.transform.localPosition;
    }

    private void Update()
    {
        spriteSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    /*
    public void AddTimeWaiting(float time) //increase timer for how long customer has been waiting
    {
        timeWaiting += time;
    }
    public void ResetTimeWaiting()
    {
        timeWaiting = 0;
    }
    */
    public float GetWaitTime() //get how long the customer is willing to wait
    {
        return waitTime;
    }
    public Ingredient GetIngredient()
    {
        return missingIngredient;
    }


}
