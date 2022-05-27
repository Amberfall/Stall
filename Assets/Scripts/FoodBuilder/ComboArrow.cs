using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboArrow : MonoBehaviour
{

    public enum Direction { Left, Right, Up, Down};
    public Direction direction;

    Vector3 refVelo;


    public void Animate()
    {
        transform.localScale = Vector3.one * 1.5f;
    }


    private void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one,ref refVelo , 10 * Time.deltaTime);
    }
}
