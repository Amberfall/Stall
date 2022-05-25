using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryIngredientShow : MonoBehaviour
{

    SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = transform.root.GetComponent<Customer>().GetIngredient().sprite;
        Debug.Log(transform.root.GetComponent<Customer>().GetIngredient().sprite);

    }

}
