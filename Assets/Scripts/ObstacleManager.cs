using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour //handles spawing andtracking obstacles in the scene based on the data stored in an ObstaccleData Scriptable Object
{
    public ObstacleData obsData;
    public GameObject obsPrefab;

    void Start()
    {
        GenObstacles(); //generated obstacles at the start of the game
    }

    void GenObstacles()
    {
        if (obsData == null || obsPrefab == null) //validate references
        {
            Debug.LogError("Missing ObstacleData or Prefab!");
            return;
        }

        for (int x = 0; x < obsData.gSize; x++) //loop through every cell in the grid
        {
            for (int z = 0; z < obsData.gSize; z++)
            {
                if (obsData.GetObstacle(x, z)) //if this cell is marked as an obstale then spawn prefab
                {
                    Vector3 pos = new Vector3(x, 1f, z);
                    Instantiate(obsPrefab, pos, Quaternion.identity, transform);
                    Debug.Log($"Spawned obstacle at {x},{z}");
                }
            }
        }
    }

    public bool IsObstacle(int x, int z) //checks if the given grid cell is marked as an obstacle
    {
        if (obsData == null) return false;
        return obsData.GetObstacle(x, z);
    }
}