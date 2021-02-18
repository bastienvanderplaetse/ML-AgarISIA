using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInteractionManager : MonoBehaviour
{
    public Cell cell;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Eatable")
        {
            Eatable eatable = collision.gameObject.GetComponent<Eatable>();
            if (eatable.CanBeEatenBy(cell))
            {
                //Debug.Log(cell.ParentName + ": I can eat " + eatable.ParentName + "!!!");
                cell.AddMass(eatable.Mass);
                eatable.Eaten();
            } else
            {
                //Debug.Log(cell.ParentName + ": I can't eat " + eatable.ParentName + " :'(");
            }
        }
    }
}
