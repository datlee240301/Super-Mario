namespace io.lockedroom.Games.SuperMario { 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float Speed;
    public float RightPos;
    public float LeftPos;
    private bool MovingForward = true;

    // Update is called once per frame
    void Update()
    {
        if (MovingForward)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(RightPos, transform.position.y), Speed * Time.deltaTime);
            if (transform.position.x >= RightPos)
            {
                MovingForward = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(LeftPos, transform.position.y), Speed * Time.deltaTime);
            if (transform.position.x <= LeftPos)
            {
                MovingForward = true;
            }
        }
    }
}
    }