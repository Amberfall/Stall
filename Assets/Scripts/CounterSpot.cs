using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSpot : MonoBehaviour
{
    Food food;

    void Awake()
    {
        food = null;
    }
    public void SetFood(Food newFood)
    {
        food = newFood;
    }
    public void RemoveFood()
    {
        food = null;
    }
    public Food CheckFood()
    {
        return food;
    }
}
