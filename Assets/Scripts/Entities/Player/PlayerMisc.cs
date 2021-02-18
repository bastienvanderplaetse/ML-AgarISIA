using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour
{
    public Transform cells;
    public Transform canvas;
    public Transform camera;

    public Color PlayerColor { get; private set; }

    private void Awake()
    {
        PlayerColor = cells.GetComponent<CellManager>().color;
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = transform.name;
    }

    private void Update()
    {
        Vector3 average = AveragePosition();
        canvas.position = average;
        average.z = -20;
        camera.position = average;
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

            return average;
        }

        return average;
    }

    public float Score()
    {
        float score = 0f;

        for (int i = 0; i < cells.childCount; i++)
        {
            score += cells.GetChild(i).GetComponent<Cell>().Mass;
        }

        return score;
    }
}
