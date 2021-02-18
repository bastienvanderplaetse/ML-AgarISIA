using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cells;
    public Transform canvas;

    private void Update()
    {
        transform.position = AveragePosition();
    }

    private Vector3 AveragePosition()
    {
        Vector3 average = Vector3.zero;
        if (cells.childCount > 0)
        {
            for (int i = 0; i < cells.childCount; i++)
            {
                average += cells.GetChild(i).position;
            }

            average = average / cells.childCount;

            average.z = -20;

            return average;
        }

        average.z = -20;
        return average;
    }
}
