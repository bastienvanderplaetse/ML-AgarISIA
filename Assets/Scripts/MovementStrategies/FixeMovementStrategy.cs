using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixeMovementStrategy : IMovementStrategy
{
    private static FixeMovementStrategy instance;

    private FixeMovementStrategy()
    {
        
    }

    public static FixeMovementStrategy GetInstance()
    {
        if (instance == null)
        {
            instance = new FixeMovementStrategy();
        }

        return instance;
    }

    public void Move(Rigidbody2D rb2d, Vector3 position, Vector3 target, float speed)
    {
        rb2d.velocity = Vector3.zero;
    }

    public void OnCollisionWithBoundary()
    {

    }
}
