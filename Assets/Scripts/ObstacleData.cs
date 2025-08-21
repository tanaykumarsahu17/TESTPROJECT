using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// stores obstacle data for a square grid using a 1D bool array
/// provides methods to get/set obstacle states at specific coordinates
[CreateAssetMenu(fileName = "ObstacleData", menuName = "Custom/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public int gSize = 10;
    [SerializeField] private bool[] obsGrid; //flattened 1D boolean array storing obstacle data

    public bool GetObstacle(int x, int z) //ensures the array i properly intitialized and returns whether the given grid cell contains an obstacle
    {
        if (obsGrid == null || obsGrid.Length != gSize * gSize)
            obsGrid = new bool[gSize * gSize];

        return obsGrid[x + z * gSize]; //converts 2D coords into 1D index
    }

    public void SetObstacle(int x, int z, bool value) // sets wheather a given grid cell contains a obstacle
    {
        if (obsGrid == null || obsGrid.Length != gSize * gSize) //initialization or reinitialization if size changed
            obsGrid = new bool[gSize * gSize];

        obsGrid[x + z * gSize] = value; //converts 2D coords into 1D index and assign
    }
}