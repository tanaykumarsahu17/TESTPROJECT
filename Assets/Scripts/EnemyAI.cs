using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AI interface ensures that all AI classes implement a TakeTurn method
public interface IAI
{
    void TakeTurn(int targetX, int targetY, Pathfinding pathfinding);
}

[RequireComponent(typeof(EnemyMovement))] //ensures EnemyMovement is always attached
public class EnemyAI : MonoBehaviour, IAI
{
    private EnemyMovement movement; //reference to the enemy movement script

    public bool IsMoving => movement != null && movement.IsMoving; //Property to check if the enemy is currently moving

    private void Awake()
    {
        movement =GetComponent<EnemyMovement>(); //Get the EnemyMovement component attached to this GameObject
    }

    public void MoveTowardsPlayer(int playerX, int playerY, Pathfinding pathfinding) //Makes the enemy move one step closer to the player's position
    {
        //Get enemy's current position on the grid (rounded to nearest int)
        int enemyX =Mathf.RoundToInt(transform.position.x);
        int enemyY =Mathf.RoundToInt(transform.position.z);

        // Find path from enemy to player
        List<PathNode> path =pathfinding.FindPath(enemyX, enemyY, playerX, playerY);

        if (path != null && path.Count > 1)
        {
            List<PathNode> step =new List<PathNode> { path[0], path[1] }; //Move only one step closer per turn
            StartCoroutine(movement.MoveAlongPath(step));
        }
        else
        {
            Debug.Log("Enemy cannot find a valid path!");
        }
    }

    public void TakeTurn(int targetX, int targetY, Pathfinding pathfinding) //Required by IAI, but we already have MoveTowardsPlayer
    {
        MoveTowardsPlayer(targetX, targetY, pathfinding);
    }
}
