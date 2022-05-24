using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSpot : MonoBehaviour
{
    [SerializeField] Food food;

    SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        food = null;
    }
    public void SetFood(Food newFood)
    {
        food = newFood;
        spriteRend.sprite = newFood.sprite;
    }
    public void RemoveFood()
    {
        food = null;
        spriteRend.sprite = null;
    }
    public Food CheckFood()
    {
        return food;
    }
    public bool HasFood()
    {
        if(food == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
