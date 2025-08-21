using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; //Movement speed of the enemy
    public bool IsMoving { get; private set; } //Tracks if the enemy is currently moving

    public IEnumerator MoveAlongPath(List<PathNode> path)  //Coroutine that moves the enemy along a given path
    {
        IsMoving =true; //Mark enemy as moving

        for (int i = 1; i < path.Count; i++) //Start from index 1 because index 0 is the enemy's current tile
        {
            Vector3 targetPos = new Vector3(path[i].x, 0.5f, path[i].y); //Convert grid coordinates to world position

            while (Vector3.Distance(transform.position, targetPos) > 0.05f) //Keep moving until the enemy reaches the target position
            {
                transform.position = Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime); //Move smoothly towards the next path node
                yield return null; //Wait until the next frame before continuing
            }
        }

        IsMoving = false; //Once finished moving along the path
    }
}
