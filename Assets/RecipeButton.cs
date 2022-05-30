using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RecipeButton : MonoBehaviour
{

    public int price;

    public RectTransform rectTrans;
    public TextMeshProUGUI theText;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }
}
