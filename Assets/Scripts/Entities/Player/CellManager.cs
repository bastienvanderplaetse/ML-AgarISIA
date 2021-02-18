using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CellManager : MonoBehaviour
{
    public static int INTERVAL_RESPAWN = 1;
    public static int MAX_SPLIT = 16;

    public GameObject cellPrefab;
    public Transform target;
    public Color color;

    private float initScale;
    private Cell initCell;

    private bool spawning;
    private float initialMass;
    public float InitialRadius { get; private set; }

    private void Awake()
    {
        //GameObject cell = Instantiate(cellPrefab, transform);
        //cell.GetComponent<SpriteRenderer>().color = color;
        //initCell = cell.GetComponent<Cell>();
        //initCell.target = target;
        //StartCoroutine(Spawn(0, true));
    }

    private void Start()
    {
        spawning = false;
        initialMass = -1f;
        //transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
        //transform.GetChild(0).GetC
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && !spawning)
        {
            //Debug.Log("Need to respawn !!!!");
            //GameObject cell = Instantiate(cellPrefab, transform);
            //Debug.Log("SCALE INIT : " + cell.transform.localScale.x);
            spawning = true;
            StartCoroutine(Spawn(INTERVAL_RESPAWN, false));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Split();
        }
    }

    private IEnumerator Spawn(int interval, bool infiniteTry)
    {
        Debug.Log("SPAWN");
        yield return new WaitForSeconds(interval);
        (Vector3, bool) result;
        GameObject cell = Instantiate(cellPrefab, transform);
        if (initialMass < 0)
        {
            initialMass = cell.GetComponent<Cell>().initialMass;
            InitialRadius = cell.transform.localScale.x / 2;
        }
        //cell.GetComponent<Cell>().target = target;
        if (infiniteTry)
        {
            Debug.Log(cell.transform.localScale.x);
            result = SpawnManager.ValidSpawnPosition(cell.transform.localScale.x / 2, Mathf.Infinity);
        }
        else
        {
            result = SpawnManager.ValidSpawnPosition(cell.transform.localScale.x / 2, 10);
        }
        //cell.transform.position = result.Item1;
        cell.transform.position = new Vector3(0, result.Item1.y, 0);
        spawning = false;
    }

    private void Split()
    {
        // if #cells > 8 => split only #cells-8 biggest cells
        // if #cells = 16 => do nothing
        // if #cells <= 8 => split all cells with correct Mass > 2*initMass
        Cell[] toSplit = new Cell[transform.childCount];
        int pos = 0;
        if (transform.childCount > 0 && transform.childCount <= MAX_SPLIT/2)
        {
            Cell cell;
            // Split all cells with Mass > 2*initMass
            for (int i = 0; i < transform.childCount; i++)
            {
                cell = transform.GetChild(i).GetComponent<Cell>();
                if (cell.Mass >= 2 * initialMass)
                {
                    toSplit[pos] = cell;
                    pos++;
                }
            }
        }
        else if (transform.childCount < MAX_SPLIT)
        {
            int maxToSplit = MAX_SPLIT - transform.childCount;
            Cell[] allCells = new Cell[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                allCells[i] = transform.GetChild(i).GetComponent<Cell>();
            }

            allCells = allCells.OrderBy(cell => -cell.Mass).ToArray();

            for (int i = 0; i < maxToSplit; i++)
            {
                if (allCells[i].Mass >= 2 * initialMass)
                {
                    toSplit[pos] = allCells[i];
                    pos++;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 0; i < pos; i++)
        {
            SplitCell(toSplit[i]);
        }
        
        
        //if (CanSplit())
        //{
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
                
        //    }
        //}
    }

    private void SplitCell(Cell cell)
    {
        cell.AddMass(-cell.Mass / 2);
        Transform clone = Instantiate(cell.transform, transform);
        clone.GetComponent<Cell>().Mass = cell.Mass;
        Debug.Log(cell.Mass + " / " + clone.GetComponent<Cell>().Mass);
        cell.SpeedUp();
    }

    private bool CanSplit()
    {
        float totalMass = 0f;

        for (int i = 0; i < transform.childCount; i++)
        {
            totalMass += transform.GetChild(i).GetComponent<Cell>().Mass;
        }

        return totalMass > 2 * initialMass && transform.childCount < 16;
        
    }
}
