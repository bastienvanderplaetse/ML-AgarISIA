using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectedMass : Eatable
{
    public static int MOVE_TIME = 1;
    public static int MIN_MASS_TO_EAT = 21;
    public static float OFFSET = 0.5f;

    public GameObject ejectedMass;
    private bool moving;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    Vector3 Direction { get; set; }
    private Vector3 target;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (moving)
        {
            rb2d.velocity = SpeedCoefficient() * (target - transform.position).normalized;
        }
        
    }

    private float SpeedCoefficient()
    {
        return 100 / (Mathf.Log(Mass) - Mathf.Log(10) + 1);
    }

    public override bool CanBeEatenBy(Eatable eater)
    {
        return OverlapForEating(eater) && eater.Mass >= MIN_MASS_TO_EAT;
    }

    public override void Eaten()
    {
        Destroy(ejectedMass);
    }

    public void Create(Vector3 _target, Color _color, Transform _cell)
    {
        transform.position = _cell.position + Vector3.Normalize(_target - _cell.position) * (_cell.localScale.x / 2 + transform.localScale.x / 2 + OFFSET);
        spriteRenderer.color = _color;
        target = _target;
        moving = true;
        StartCoroutine(StopMove());
    }

    private IEnumerator StopMove()
    {
        yield return new WaitForSeconds(MOVE_TIME);
        moving = false;
        rb2d.velocity = Vector3.zero;
        target = transform.position;
    }
}
