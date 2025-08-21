using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents a single cell (node) in the pathfinding grid\
//Stores walkabilty, cost values, and parent link for path reconstruction.
public class PathNode 
{
    public int x, y; //grud cordinates of this node
    public bool isWalkable; //whether this node can be walked on(false =obstacle)
    public int gCost, hCost; //cost from the start node to this node(g) and heuristic cost estimae from this node to th end node (h)
    public int fCost => gCost + hCost; // total cost
    public PathNode parent; // parent node in the path

    public PathNode(int x, int y, bool isWalkable) //constructor to initialize a grid node
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
}
