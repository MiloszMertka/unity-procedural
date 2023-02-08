using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Building building;
    private Grid grid;
    private MapGrid mapGrid;

    private const int PRIMARY_MOUSE_BUTTON_CODE = 0;

    public void Start()
    {
        grid = FindObjectOfType<Grid>();
        var mapGenerator = FindObjectOfType<MapGenerator>();
        mapGrid = mapGenerator.mapGrid;
    }

    public void Update()
    {
        if (building != null)
        {
            ToggleBuildingPreview();

            if (Input.GetMouseButtonDown(PRIMARY_MOUSE_BUTTON_CODE) && CheckIfBuildingCanBeBuilt())
            {
                Build();
                ExitBuildingMode();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitBuildingMode();
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (grid != null && building != null && mapGrid != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCellPosition = grid.WorldToCell(mousePosition);

            for (int i = 0; i < building.size.height; i++)
            {
                for (int j = 0; j < building.size.width; j++)
                {
                    Vector3Int cellPosition = new Vector3Int(mouseCellPosition.x + j, mouseCellPosition.y + i);
                    Node node = mapGrid.GetNodeByPosition(cellPosition);

                    Vector3 gizmoPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f);
                    if (node == null || !node.walkable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(gizmoPosition, Vector3.one);
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawCube(gizmoPosition, Vector3.one);
                    }
                }
            }
        }
    }

    private void ToggleBuildingPreview()
    {
        Cursor.visible = !Cursor.visible;
    }

    private bool CheckIfBuildingCanBeBuilt()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPosition = grid.WorldToCell(mousePosition);

        for (int i = 0; i < building.size.height; i++)
        {
            for (int j = 0; j < building.size.width; j++)
            {
                Vector3Int cellPosition = new Vector3Int(mouseCellPosition.x + j, mouseCellPosition.y + i);
                Node node = mapGrid.GetNodeByPosition(cellPosition);

                if (node == null || !node.walkable)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void Build()
    {
        // TODO
    }

    private void ExitBuildingMode()
    {
        ToggleBuildingPreview();
        building = null;
    }
}
