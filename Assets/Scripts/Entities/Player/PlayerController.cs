using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Player player;

    public MovementStrategyType movementStrategyType;
    private IMovementStrategy movementStrategy;

    private Rigidbody2D rb2d;
    private float initMassLog;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector3.zero;
        HandleMovementStrategyType();

        initMassLog = Mathf.Log(10);// Mathf.Log(player.initialMass);
    }

    private void FixedUpdate()
    {
        movementStrategy.Move(rb2d, transform.position, Vector3.zero, SpeedCoefficient());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Background")
        {
            movementStrategy.OnCollisionWithBoundary();
        }
    }

    public void HandleMovementStrategyType()
    {
        switch (movementStrategyType)
        {
            case MovementStrategyType.Fixe:
                movementStrategy = FixeMovementStrategy.GetInstance();
                break;

            case MovementStrategyType.Horizontal:
                movementStrategy = HorizontalMovementStrategy.GetInstance();
                break;

            case MovementStrategyType.Vertical:
                movementStrategy = VerticalMovementStrategy.GetInstance();
                break;
        }
    }

    private float SpeedCoefficient()
    {
        return speed / (Mathf.Log(player.Mass) - initMassLog + 1);
    }
}
