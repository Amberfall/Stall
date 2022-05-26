using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateManager : MonoBehaviour
{

    public CustomerBaseState currentState;
    [HideInInspector] public Customer customer;
    public CustomerUnseatedState UnseatedState = new CustomerUnseatedState();
    public CustomerSeatedState SeatedState = new CustomerSeatedState();
    public CustomerFedState FedState = new CustomerFedState();
    public CustomerLostState LostState = new CustomerLostState();
    [HideInInspector] public Stall stall;

    void Awake()
    {
        stall = FindObjectOfType<Stall>();
        customer = GetComponent<Customer>();
        customer.stateManager = this;
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
