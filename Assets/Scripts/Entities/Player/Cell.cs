using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Eatable
{
    public static float MASS_MAX = 30000;
    private static int INTERVAL_DECREASE = 1;
    private static float DECREASE_RATE = 0.2f;
    private float initMassLog;

    public float speed;

    public Transform target;

    private Rigidbody2D rb2d;

    public string PlayerName { get; set; }

    protected override void Awake()
    {
        base.Awake();
        //Debug.Log("Cell Awake");
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector3.zero;
        target = transform.parent.GetComponent<CellManager>().target;
        GetComponent<SpriteRenderer>().color = transform.parent.GetComponent<CellManager>().color;
    }

    protected override void Start()
    {
        base.Start();
        //Debug.Log("Cell Start");
        initMassLog = Mathf.Log(initialMass);
        ParentName = transform.parent.parent.name;
        StartCoroutine(MassDecrease());
    }

    private void FixedUpdate()
    {
        //Debug.Log(ParentName);
    }


    void Update()
    {
        rb2d.velocity = SpeedCoefficient() * (target.position - transform.position).normalized;
    }

    private float SpeedCoefficient()
    {
        return speed / (Mathf.Log(Mass) - initMassLog + 1);
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.gameObject.tag);
    //    if (collision.gameObject.tag == "Eatable")
    //    {
    //        Debug.Log("ICI");
    //        Eatable eatable = collision.gameObject.GetComponent<Eatable>();
    //        if (eatable.CanBeEatenBy(this))
    //        {
    //            AddMass(eatable.Mass);
    //            eatable.Eaten();
    //        }
    //    }
    //}

    public void AddMass(float mass)
    {
        if (Mass + mass > MASS_MAX)
        {
            Mass = MASS_MAX;
        }
        else if (Mass + mass < initialMass)
        {
            Mass = initialMass;
        }
        else
        {
            Mass += mass;
        }
        Debug.Log(ParentName + " mass " + mass + " / MASS " + Mass);
        float radius = Radius();
        transform.localScale = new Vector3(radius, radius, 0);
    }

    public override void Eaten()
    {
        Destroy(this.gameObject);
    }

    public override bool CanBeEatenBy(Eatable eater)
    {
        return OverlapForEating(eater) && eater.ParentName != this.ParentName;
    }

    private IEnumerator MassDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(INTERVAL_DECREASE);
            if (Mass > initialMass)
            {
                AddMass(-Mass * DECREASE_RATE);
            }
        }
    }
}
