using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Eatable : MonoBehaviour
{
    protected static float EATABLE_RATIO = 0.7f;

    public float Mass { get; set; }

    public float initialMass;

    private void Start()
    {
        Mass = initialMass;
        float radius = Radius();
        transform.localScale = new Vector3(radius, radius, 0);
    }

    private void FixedUpdate()
    {
        float radius = Radius();
        transform.localScale = new Vector3(radius, radius, 0);
    }

    protected virtual float Radius()
    {
        return 4 + Mathf.Sqrt(Mass) * 3;
    }

    public abstract void Eaten();
    public abstract bool CanBeEatenBy(Eatable eater);

    protected bool OverlapForEating(Eatable eater)
    {
        float r1 = eater.transform.localScale.x / 2;
        float r2 = transform.localScale.x / 2;

        if (r1 > r2)
        {
            float A_inter = AreaOverlap(eater.transform.position, transform.position, r1, r2);
            float area = Mathf.PI * r2 * r2;
            //Debug.Log("Area :" + area + " / area ratio : " + (area * EATABLE_RATIO) + " / A_inter:" + A_inter);
            return (A_inter >= area * EATABLE_RATIO);
        }

        return false;
    }

    protected float AreaOverlap(Vector3 c1, Vector3 c2, float r1, float r2)
    {
        float d = (c1 - c2).magnitude;
        float d1 = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
        float d2 = d - d1;

        float A_inter = r1 * r1 * Mathf.Acos(d1 / r1) - d1 * Mathf.Sqrt(r1 * r1 - d1 * d1)
            + r2 * r2 * Mathf.Acos(d2 / r2) - d2 * Mathf.Sqrt(r2 * r2 - d2 * d2);

        return A_inter;
    }
}
