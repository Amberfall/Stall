using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest : MonoBehaviour
{

    public enum Direction { Left, Right, Up, Down};
    public Direction direction;

    [HideInInspector] public Vector2 dir;

    private void Awake()
    {
        SetDirection();
    }

    public void ArrowChosen()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void ResetArrow()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    void SetDirection()
    {
        switch (direction)
        {
            case Direction.Left:
                dir = Vector2.left;
                break;
            case Direction.Right:
                dir = Vector2.right;
                break;
            case Direction.Up:
                dir = Vector2.up;
                break;
            case Direction.Down:
                dir = Vector2.down;
                break;
            default:
                break;
        }
    }
}
