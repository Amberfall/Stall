using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    //a variable which holds the combo that translates to this ingredient

    public List<string> hints = new List<string>();

}
