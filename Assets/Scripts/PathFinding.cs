using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    MapGrid mapGrid;

    public void Start()
    {
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        mapGrid = mapGenerator.mapGrid;
    }

    public Stack<Node> FindPath(Vector3Int sourcePosition, Vector3Int destinationPosition)
    {
        Node source = GetNodeByPosition(sourcePosition);
        Node destination = GetNodeByPosition(destinationPosition);

        CheckIfNodeIsNull(source);
        CheckIfNodeIsNull(destination);

        List<Node> openSet = new List<Node>();
        ISet<Node> closedSet = new HashSet<Node>();

        openSet.Add(source);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            foreach (Node node in openSet)
            {
                if (node.fCost < currentNode.fCost || (node.fCost == currentNode.fCost && node.hCost < currentNode.hCost))
                {
                    currentNode = node;
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == destination)
            {
                return BuildPath(source, destination);
            }

            foreach (Node neighbour in mapGrid.GetNodeNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newDistanceToNeighbourNode = currentNode.gCost + GetDistanceBetweenNodes(currentNode, neighbour);

                if (!openSet.Contains(neighbour) || newDistanceToNeighbourNode < neighbour.gCost)
                {
                    neighbour.gCost = newDistanceToNeighbourNode;
                    neighbour.hCost = GetDistanceBetweenNodes(neighbour, destination);
                    neighbour.pathFindingPredecessor = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private Node GetNodeByPosition(Vector3Int position)
    {
        foreach (Node node in mapGrid.nodes)
        {
            if (node.position == position)
            {
                return node;
            }
        }

        return null;
    }

    private int GetDistanceBetweenNodes(Node firstNode, Node secondNode)
    {
        int distanceX = Mathf.Abs(firstNode.gridPosition.x - secondNode.gridPosition.x);
        int distanceY = Mathf.Abs(firstNode.gridPosition.y - secondNode.gridPosition.y);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

    private Stack<Node> BuildPath(Node source, Node destination)
    {
        var path = new Stack<Node>();

        Node currentNode = destination;

        while (currentNode != source)
        {
            path.Push(currentNode);
            currentNode = currentNode.pathFindingPredecessor;
        }

        return path;
    }

    private bool CheckIfNodeIsNull(Node node)
    {
        if (node == null)
        {
            Debug.LogError("Node is null!");
            return true;
        }

        return false;
    }
}
