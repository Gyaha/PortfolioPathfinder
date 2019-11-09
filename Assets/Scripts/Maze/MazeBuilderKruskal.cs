using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderKruskal : MazeBuilderCells
{
    public MazeBuilderKruskal(MazeManager mazeManager) : base(mazeManager) { }

    public override void Run()
    {
        base.Run();

        List<Vector2Int> allWalls = GetAllWalls();

        List<Vector2Int> allCells = GetAllCells();

        List<List<Vector2Int>> cellGroups = new List<List<Vector2Int>>();
        foreach (var cell in allCells)
        {
            List<Vector2Int> cellGroup = new List<Vector2Int>();
            cellGroup.Add(cell);
            cellGroups.Add(cellGroup);
        }

        allWalls.Shuffle();

        foreach (var wall in allWalls)
        {
            List<Vector2Int> cells = GetWallCells(wall);

            Vector2Int cell1 = cells[0];
            Vector2Int cell2 = cells[1];

            int cellGroup1 = GetCellGroup(cellGroups, cell1);
            int cellGroup2 = GetCellGroup(cellGroups, cell2);

            if (cellGroup1 != cellGroup2)
            {
                SetCell(cell1, false);
                SetCell(cell2, false);
                SetCellWall(cell1, GetCellDirection(cell1, cell2), false);

                cellGroups = CombineCellGroups(cellGroups, cellGroup1, cellGroup2);
            }
        }

    }

    private List<List<Vector2Int>> CombineCellGroups(List<List<Vector2Int>> cellGroups, int group1, int group2)
    {
        int groupMin = Mathf.Min(group1, group2);
        int groupMax = Mathf.Max(group1, group2);

        List<Vector2Int> groupTemp = cellGroups[groupMax];
        cellGroups.RemoveAt(groupMax);

        groupTemp.ForEach(p => cellGroups[groupMin].Add(p));

        return cellGroups;
    }

    private int GetCellGroup(List<List<Vector2Int>> cellGroups, Vector2Int cell)
    {
        Debug.Log("search cells");

        for (int i = 0; i < cellGroups.Count; i++)
        {
            List<Vector2Int> cellGroup = cellGroups[i];
            if (cellGroup.Contains(cell))
            {
                return i;
            }
        }

        Debug.LogError("cell group not found");
        return 0;
    }
}