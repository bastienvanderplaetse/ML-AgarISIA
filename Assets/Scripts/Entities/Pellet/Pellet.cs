using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Eatable
{
    public GameObject pellet;
    //protected new static float EATABLE_RATIO = 0.2f;

    public override bool CanBeEatenBy(Eatable eater)
    {
        return OverlapForEating(eater);
    }

    public override void Eaten()
    {
        Destroy(pellet);
    }

    //protected override float Radius()
    //{
    //    return 10.0f;
    //}
}
