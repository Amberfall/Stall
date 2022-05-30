using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTextScript : MonoBehaviour
{

    TextMeshProUGUI theText;

    private void Awake()
    {
        theText = GetComponent<TextMeshProUGUI>();
    }
    public void SetGoldTo(float amount)
    {
        theText.text = amount.ToString() + " G";
        theText.fontSize = 60;
    }

    float refVelo;

    private void Update()
    {
        theText.fontSize = Mathf.SmoothDamp(theText.fontSize, 30, ref refVelo, 10 * Time.deltaTime);
    }

}
