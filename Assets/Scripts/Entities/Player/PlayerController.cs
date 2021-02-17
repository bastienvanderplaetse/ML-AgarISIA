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

    private void Start()
    {
        player.ParentName = this.name;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Split();
        }
    }

    private void FixedUpdate()
    {
        movementStrategy.Move(rb2d, transform.position, Vector3.zero, SpeedCoefficient());
    }

    private void Split()
    {
        // if mass > 2 * initMass 
        // pas plus que 16 entités
        //GameObject[] playerChildren = new GameObject[transform.childCount - 2];
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        int numChildren = transform.childCount;
        Player playerComponent;
        for (int i = 2; i < numChildren; i++)
        {
            playerComponent = transform.GetChild(i).GetComponent<Player>();
            playerComponent.Mass /= 2;
            Transform clone = Instantiate(transform.GetChild(i));
            //Transform clone = Instantiate(transform.GetChild(i), transform);
            clone.position = playerComponent.transform.position;

            Debug.Log(rb2d.velocity.normalized);
            Debug.Log(new Vector2(1, 1).normalized);
            Debug.Log(this.name);
            clone.GetComponent<Player>().ParentName = this.name;
            // Two lines below => for spawning
            clone.GetComponent<Player>().Mass = playerComponent.Mass;
            clone.GetComponent<Player>().SpeedUp(rb2d.velocity, speed * 1.5f, transform);
        }
        transform.position = currentPosition;
        //Player players = transform.GetChild()
        //Debug.Log(players);
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
