using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementStrategyType
{
    Fixe,
    Horizontal,
    Vertical
}

public interface IMovementStrategy
{
    void Move(Rigidbody2D rb2d, Vector3 position, Vector3 target, float speed);
    void OnCollisionWithBoundary();
}
