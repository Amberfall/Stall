using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest : MonoBehaviour
{

    public enum Direction { Left, Right, Up, Down};
    public Direction direction;

    public void ArrowChosen()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void ResetArrow()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
