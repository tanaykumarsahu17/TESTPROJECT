using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public IsoGridGenerator3D gridGenerator; // Reference to grid generator (creates tiles)
    public ObstacleManager obstacleManager; // Reference to obstacle manager (handles blocked tiles)
    public GameObject playerPrefab; // Player prefab to spawn
    public EnemyAI enemyPrefab; // Enemy prefab to spawn (assign in Inspector)

    private PlayerController player; // Player controller reference
    private EnemyAI enemy; // Enemy AI reference
    private Pathfinding pathfinding; // Pathfinding system (A* implementation)
    private bool enemyTurn = false; // Keeps track of whose turn it is

    void Start() 
    {
        //Spawn Player
        GameObject p = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        player = p.GetComponent<PlayerController>();

        //Spawn Enemy
        enemy = Instantiate(enemyPrefab, new Vector3(5, 0.5f, 5), Quaternion.identity);

        //Build Obstacle Map
        bool[,] obstacleMap = new bool[gridGenerator.gridWidth, gridGenerator.gridHeight];
        for (int x = 0; x < gridGenerator.gridWidth; x++) 
        {
            for (int y = 0; y < gridGenerator.gridHeight; y++)
            {
                obstacleMap[x, y] = obstacleManager.IsObstacle(x, y); // true if blocked
            }
        }

        //Initialize Pathfinding
        pathfinding = new Pathfinding(gridGenerator.gridWidth, gridGenerator.gridHeight, obstacleMap);
    }

    void Update() 
    { 
        if (!enemyTurn && !player.IsMoving) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                // Shoot a ray from camera to detect clicked tile
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) 
                {
                    TileInfo tile = hit.collider.GetComponent<TileInfo>();

                    if (tile != null) 
                    {
                        // Calculate path from player to clicked tile
                        List<PathNode> path = pathfinding.FindPath(
                            Mathf.RoundToInt(player.transform.position.x),
                            Mathf.RoundToInt(player.transform.position.z),
                            tile.x, tile.y);

                        
                        if (path != null) //If valid path exists, move player
                        {
                            player.MoveAlongPath(path);
                            enemyTurn = true; //After player moves, enemy gets turn
                        }
                    }
                }
            }
        }

        if (enemyTurn && !player.IsMoving && !enemy.IsMoving)
        {
            StartCoroutine(EnemyTurnRoutine()); //Handle enemy turn
        }
    }

    private IEnumerator EnemyTurnRoutine() 
    {
        enemyTurn = false; //Lock turn system while enemy moves

        // Get player's grid position
        int playerX = Mathf.RoundToInt(player.transform.position.x);
        int playerY = Mathf.RoundToInt(player.transform.position.z);

        // Tell enemy to move towards player
        enemy.TakeTurn(playerX, playerY, pathfinding);

        // Wait until enemy finishes moving
        while (enemy.IsMoving)
        {
            yield return null;
        }
    }
}
