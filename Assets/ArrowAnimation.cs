using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    Vector3 refVelo;


    public void Animate()
    {
        if(gameObject.name == "ArrowPosition")
        {
            transform.localScale = Vector3.one * 1.5f;

        }
        else
        {
            transform.localScale = Vector3.one * 2f;
        }
    }


    private void Update()
    {
        if (gameObject.name == "ArrowPosition")
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 10 * Time.deltaTime);

        }
        else
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one, ref refVelo, 20 * Time.deltaTime);
        }
    }
}
