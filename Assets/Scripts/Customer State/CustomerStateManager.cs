using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateManager : MonoBehaviour
{
    CustomerBaseState currentState;
    [HideInInspector] public Customer customer;
    public CustomerUnseatedState UnseatedState = new CustomerUnseatedState();
    public CustomerSeatedState SeatedState = new CustomerSeatedState();
    public CustomerFedState FedState = new CustomerFedState();
    public CustomerLostState LostState = new CustomerLostState();
    [HideInInspector] public Stall stall;

    public SpriteRenderer foodHolder;
    [HideInInspector] public Vector2 foodHolderStartPos;

    void Awake()
    {
        stall = FindObjectOfType<Stall>();
        customer = GetComponent<Customer>();
        foodHolderStartPos = foodHolder.transform.localPosition;
    }

    void Start()
    {
        currentState = UnseatedState;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(CustomerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
