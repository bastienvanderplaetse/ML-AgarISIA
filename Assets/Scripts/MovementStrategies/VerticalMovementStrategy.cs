using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementStrategy : IMovementStrategy
{
    private Vector3 movement;

    private VerticalMovementStrategy()
    {
        movement = Vector3.down;
    }

    public static VerticalMovementStrategy GetInstance()
    {
        return new VerticalMovementStrategy();
    }

    public void Move(Rigidbody2D rb2d, Vector3 position, Vector3 target, float speed)
    {
        rb2d.velocity = movement * speed;
    }

    public void OnCollisionWithBoundary()
    {
        movement = (movement == Vector3.down) ? Vector3.down : Vector3.down;
    }
}
