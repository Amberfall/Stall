using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookAction", menuName = "CookAction")]
public class CookAction : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    //a variable which holds the combo that translates to this ingredient
}
