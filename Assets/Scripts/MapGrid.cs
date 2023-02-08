using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{
    public Node[,] nodes;
    public int width;
    public int height;

    public MapGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        nodes = new Node[height, width];
    }

    public List<Node> GetNodeNeighbours(Node node)
    {
        var neighbours = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int x = node.gridPosition.x + j;
                int y = node.gridPosition.y + i;

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    neighbours.Add(nodes[y, x]);
                }
            }
        }

        return neighbours;
    }

    public Node GetNodeByPosition(Vector3Int position)
    {
        foreach (var node in nodes)
        {
            if (node.position == position)
            {
                return node;
            }
        }

        return null;
    }
}
