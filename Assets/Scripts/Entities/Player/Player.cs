using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Eatable
{
    public static float MASS_MAX = 30000;
    public static int INTERVAL_RESPAWN = 1;

    private static int INTERVAL_DECREASE = 1;
    private static float DECREASE_RATE = 0.2f;

    private void Awake()
    {
        Mass = initialMass;
        Spawn(0, true);
        Debug.Log("AWAKE");
    }

    private void Start()
    {
        StartCoroutine(MassDecrease());
    }

    public override void Eaten()
    {
        Debug.Log("I'm eaten !!!");
        Mass = initialMass;
        transform.parent.transform.position = new Vector2(10000, 10000);
        StartCoroutine(Spawn(INTERVAL_RESPAWN, false));
    }

    private IEnumerator Spawn(int interval, bool infiniteTry)
    {
        yield return new WaitForSeconds(interval);
        Debug.Log("Spawn");
        (Vector3, bool) result;
        if (infiniteTry)
        {
            result = SpawnManager.ValidSpawnPosition(transform.localScale.x / 2, Mathf.Infinity);
        }
        else
        {
            result = SpawnManager.ValidSpawnPosition(transform.localScale.x / 2, 10);
        }
        
        //transform.parent.transform.position = result.Item1;
        transform.parent.transform.position = new Vector3(0, result.Item1.y, 0);
    }

    public override bool CanBeEatenBy(Eatable eater)
    {
        return OverlapForEating(eater) && eater.ParentName != this.ParentName;
    }

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

    public void SpeedUp(Vector3 direction, float bonusSpeed, Transform _parent)
    {
        //Rigidbody2D rb2d = new Rigidbody2D();
        Debug.Log("SPEED UP");
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * bonusSpeed;
        //SpeedUpStop(_parent);
        StartCoroutine(SpeedUpStop(_parent));
        //yield return new WaitForSeconds(1);
        //Destroy(gameObject.GetComponent<Rigidbody2D>());
        //transform.parent = _parent;
        //Debug.Log("END");
        //Debug.Log("END");
    }

    private IEnumerator SpeedUpStop(Transform _parent)
    {
        Debug.Log("TEST STOP");
        yield return new WaitForSeconds(1);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        transform.parent = _parent;
        Debug.Log("END");
    }
}
