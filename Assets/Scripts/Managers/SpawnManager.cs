using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static float minHorizontalSpawn = -6200f;
    public static float maxHorizontalSpawn = 6200f;
    public static float minVerticalSpawn = -6200f;
    public static float maxVerticalSpawn = 6200f;

    public GameObject prefabPellet;
    public float intervalSpawnPellet;
    public int maxPelletInstances;
    public Transform pellets;

    private void Start()
    {
        StartCoroutine(SpawnPellet());
    }

    private IEnumerator SpawnPellet()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSpawnPellet);

            if (pellets.childCount < maxPelletInstances)
            {
                (Vector3, bool) result = ValidSpawnPosition(prefabPellet.transform.localScale.x / 2, 5);
                
                if (result.Item2)
                {
                    //Instantiate(prefabPellet, new Vector3(0,result.Item1.y,0), Quaternion.identity, pellets);
                    Instantiate(prefabPellet, result.Item1, Quaternion.identity, pellets);
                }
            }
        }
    }

    public static (Vector3, bool) ValidSpawnPosition(float radiusToSpawn, float maxAttempts)
    {
        Vector3 pose = Vector3.zero;
        int attempts = 0;
        
        while (attempts < maxAttempts)
        {
            pose = new Vector3(Random.Range(minHorizontalSpawn, maxHorizontalSpawn), Random.Range(minVerticalSpawn, maxVerticalSpawn), 0.0f);
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(pose, radiusToSpawn, Vector2.zero);

            if (raycastHit2D.collider == null)
            {
                break;
            }
            attempts++;
        }

        if (attempts < maxAttempts)
        {
            return (pose, true);
        }
        else
        {
            return (Vector3.zero, false);
        }
    }
}
