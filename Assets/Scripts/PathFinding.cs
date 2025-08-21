using System.Collections.Generic;
using UnityEngine;

// Simple A* Pathfinding implementation
public class Pathfinding
 {
    private int width, height;  // Dimensions of the grid
    private PathNode[,] grid;  // 2D array of nodes representing the grid

    // Constructor: Initializes grid and marks walkable/obstacles
    public Pathfinding(int width, int height, bool[,] obstacleMap)
    {
        this.width = width;
        this.height = height;
        grid = new PathNode[width, height];

        for (int x = 0; x<width; x++) //Create nodes for every tile in the grid
        {
            for (int y = 0; y<height; y++)
            {
                bool isWalkable = !obstacleMap[x, y]; // node is walkable if NOT an obstacle
                grid[x, y] = new PathNode(x, y, isWalkable);
            }
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) // Main A* pathfinding method
    {
        PathNode startNode =grid[startX, startY];
        PathNode endNode =grid[endX, endY];

        List<PathNode> openList =new List<PathNode> { startNode }; // nodes to be evaluated
        HashSet<PathNode> closedList =new HashSet<PathNode>(); // nodes already evaluated

        // Reset all nodes before search
        foreach (var node in grid) 
        {
            node.gCost = int.MaxValue; // unknown cost = infinity
            node.parent = null;
        }

        // Initialize start node
        startNode.gCost =0;
        startNode.hCost =GetDistance(startNode, endNode);

        // Search loop
        while (openList.Count > 0) 
        {
            PathNode currentNode =GetLowestFCost(openList); // node with lowest fCost
            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode); // Path found
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbor in GetNeighbors(currentNode))  //Check all valid neighbors
            {
                if (!neighbor.isWalkable || closedList.Contains(neighbor)) continue;

                int tentativeGCost =currentNode.gCost + GetDistance(currentNode, neighbor);

                if (tentativeGCost < neighbor.gCost) //If this path is better than the previous one
                {
                    neighbor.parent = currentNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = GetDistance(neighbor, endNode);

                    if (!openList.Contains(neighbor)) 
                        openList.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    private List<PathNode> RetracePath(PathNode start, PathNode end) //Backtrack from end to start to build final path
    {
        List<PathNode> path =new List<PathNode>();
        PathNode current =end;

        while (current != start) {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse(); // reverse to get path from start toend
        return path;
    }

    private int GetDistance(PathNode a, PathNode b) 
    // Manhattan Distance heuristic (grid-based movement)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private PathNode GetLowestFCost(List<PathNode> list) //Find node with lowest F cost (g + h)
    {
        PathNode lowest = list[0];
        foreach (PathNode node in list) {
            if (node.fCost < lowest.fCost) lowest = node;
        }
        return lowest;
    }

    private List<PathNode> GetNeighbors(PathNode node) //Get all valid 4-directional neighbors
    {
        List<PathNode> neighbors = new List<PathNode>();
        int[,] directions ={ { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; // Right, Left, Up, Down

        for (int i = 0; i < directions.GetLength(0); i++) 
        {
            int nx = node.x + directions[i, 0];
            int ny = node.y + directions[i, 1];

            // Make sure neighbor is inside grid bounds
            if (nx >= 0 && ny >= 0 && nx < width && ny < height) 
            {
                neighbors.Add(grid[nx, ny]);
            }
        }

        return neighbors;
    }
}
