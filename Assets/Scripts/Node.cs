using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3Int position;
    public bool walkable;
    public Vector2Int gridPosition;

    public Node pathFindingPredecessor;
    public int gCost;
    public int hCost;

    public int fCost
    {
        get {
            return gCost + hCost;
        }
    }

    public Node(Vector3Int position, bool walkable, Vector2Int gridPosition)
    {
        this.position = position;
        this.walkable = walkable;
        this.gridPosition = gridPosition;
    }
}
