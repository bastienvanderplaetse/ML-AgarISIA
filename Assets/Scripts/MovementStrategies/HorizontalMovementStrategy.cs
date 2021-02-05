using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementStrategy : IMovementStrategy
{
    private Vector3 movement;

    private HorizontalMovementStrategy()
    {
        movement = Vector3.left;
    }

    public static HorizontalMovementStrategy GetInstance()
    {
        return new HorizontalMovementStrategy();
    }

    public void Move(Rigidbody2D rb2d, Vector3 position, Vector3 target, float speed)
    {
        rb2d.velocity = movement * speed;
    }

    public void OnCollisionWithBoundary()
    {
        movement = (movement == Vector3.left) ? Vector3.right : Vector3.left;
    }
}
