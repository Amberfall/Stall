using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    Vector3 refVelo;


    public void Animate()
    {
        transform.localScale = Vector3.one * 2f;
    }


    private void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one, ref refVelo, 20 * Time.deltaTime);
    }
}
