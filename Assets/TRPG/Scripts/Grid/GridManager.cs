using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GridManager : M_Singleton<GridManager>
{
    private const string GRID_NAME_FORMAT = "Grid ({0}, {1})";
    [SerializeField] private VisualGrid gridPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Vector2Int gridArea = new Vector2Int(100, 100);

    private VisualGrid[,] visualGrid2dArray;
    private Vector2Int configGridArea;


    protected override void Awake()
    {
        base.Awake();
        configGridArea = gridArea;
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        visualGrid2dArray = new VisualGrid[configGridArea.x, configGridArea.y];
        for (int y = 0; y < configGridArea.y; y++)
        {
            for (int x = 0; x < configGridArea.x; x++)
            {
                VisualGrid grid = Instantiate(gridPrefab, new Vector3(x, 0.1f, y), Quaternion.identity);
                grid.transform.SetParent(parent, true);
                grid.transform.name = string.Format(GRID_NAME_FORMAT, x, y);
                visualGrid2dArray[x, y] = grid;
                visualGrid2dArray[x, y].Deselect();
            }
        }
    }

    public VisualGrid GetGridAtPosition(int x, int y)
    {
        return visualGrid2dArray[x, y]; 
    }

    public List<VisualGrid> GetSurroundingCells(Vector2Int centerPos, int range)
    {
        List<VisualGrid> surroundingCells = new List<VisualGrid>();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                // Skip the center cell
                if (x == 0 && y == 0) continue;

                // Calculate the neighbor's grid position
                int neighborX = centerPos.x + x;
                int neighborY = centerPos.y + y;

                // Check if within grid bounds
                if (neighborX >= 0 && neighborX < configGridArea.x && neighborY >= 0 && neighborY < configGridArea.y)
                {
                    // Add the visual cell at this position
                    Debug.Log($"Cell=[{neighborX} : {neighborY}]");
                    surroundingCells.Add(visualGrid2dArray[neighborX, neighborY]);
                }
            }
        }
        return surroundingCells;
    }

    public void EnableSurroundingCells(Vector2Int centerPos, int range)
    {
        List<VisualGrid> gridCells = GetSurroundingCells(centerPos, range);
        gridCells.ForEach(cell => cell.Select());
    }

    public void DisableAllCells()
    {
        for (int y = 0; y < visualGrid2dArray.GetLength(1); y++)
        {
            for (int x = 0; x < visualGrid2dArray.GetLength(0); x++)
            {
                if (visualGrid2dArray[x, y].IsActive)
                    visualGrid2dArray[x, y].Deselect();
            }
        }
    }
}
