using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour
{
    public Transform cells;
    public Transform canvas;
    public Transform camera;
    private Camera cameraComponent;
    private float initialRadius;

    public Color PlayerColor { get; private set; }

    private void Awake()
    {
        PlayerColor = cells.GetComponent<CellManager>().color;
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = transform.name;
        cameraComponent = camera.GetComponent<Camera>();
        initialRadius = -1f;
    }

    private void Update()
    {
        Zoom();
        Vector3 average = AveragePosition();
        canvas.position = average;
        average.z = -20;
        camera.position = average;
    }

    private void Zoom()
    {
        float maxDist = 0f;
        float distance;
        float maxRadius = 0f;
        float radius = 0f;
        for (int i = 0; i < cells.childCount; i++)
        {
            radius = cells.GetChild(i).transform.localScale.x/2;
            if (radius > maxRadius)
            {
                maxRadius = radius;
            }
            for (int j = i; j < cells.childCount; j++)
            {
                distance = Vector3.Distance(cells.GetChild(i).transform.position, cells.GetChild(j).transform.position);
                if (distance > maxDist)
                {
                    maxDist = distance;
                }
            }
        }

        if (initialRadius < 0)
        {
            initialRadius = cells.GetComponent<CellManager>().InitialRadius;
        }

        maxRadius = maxRadius - initialRadius;

        cameraComponent.orthographicSize = 100 + Mathf.Max(maxRadius, maxDist);
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
