using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //handles player movement
{
    public float moveSpeed =3f; //movement speed

    private bool isMoving =false; //check is player moving

    public bool IsMoving => isMoving; //Public property so GameManager/EnemyAI can check

    public void MoveAlongPath(List<PathNode> path) // starts moving the player along the given path of node
    {
        if (!isMoving) StartCoroutine(MovePath(path));
    }

    private IEnumerator MovePath(List<PathNode> path) //Coroutine moves player smoothly along the sequence of path nodes.Stops at each tile center before moving to the next. 
    {

        isMoving = true;

        foreach (PathNode node in path)
        {
        Vector3 targetPos =new Vector3(node.x, 0.5f, node.y); // adjust Y if needed

        while (Vector3.Distance(transform.position, targetPos) > 0.05f) //Move towards each node
        {
            transform.position =Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position =targetPos; //Snap to exact tile after reaching
        yield return null;
        }

        isMoving =false; //done moving
    }
}


