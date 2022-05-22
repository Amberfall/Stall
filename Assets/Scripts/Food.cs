using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Food", menuName = "Food")]
public class Food : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public List<Ingredient> ingredients;

    public bool ContainsIngredient(Ingredient ingredient)
    {
        for(int i = 0; i < ingredients.Count; i ++)
        {
            if(ingredients[i] == ingredient)
            {
                return true;
            }
        }
        return false;
    }
}
