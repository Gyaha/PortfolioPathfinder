using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderPrims : MazeBuilderCells
{
    public MazeBuilderPrims(MazeManager mazeManager) : base(mazeManager) { }

    public override void Run()
    {
        base.Run();

        List<Vector2Int> queue = new List<Vector2Int>();

        foreach (var wall in GetCellWallNodes(cellOrigen))
        {
            queue.Add(wall);
        }

        SetCell(cellOrigen, false);

        while (queue.Count > 0)
        {
            queue.Shuffle();

            Vector2Int wall = queue[0];
            queue.RemoveAt(0);

            List<Vector2Int> wallCells = GetWallCells(wall);

            Vector2Int cell1 = wallCells[0];
            Vector2Int cell2 = wallCells[1];

            if (GetCell(cell1) || GetCell(cell2))
            {
                if (GetCell(cell1))
                {
                    queue = AddCellWallsToQueue(queue, cell1);
                }
                if (GetCell(cell2))
                {
                    queue = AddCellWallsToQueue(queue, cell2);
                }

                SetCell(cell1, false);
                SetCell(cell2, false);
                SetNode(wall, false);
            }
        }
    }

    private List<Vector2Int> AddCellWallsToQueue(List<Vector2Int> queue, Vector2Int cell)
    {
        foreach (var wall in GetCellWallNodes(cell))
        {
            queue.Add(wall);
        }

        return queue;
    }
}