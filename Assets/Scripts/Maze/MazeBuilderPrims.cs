using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderPrims : MazeBuilderCells
{
    public MazeBuilderPrims(MazeManager mazeManager) : base(mazeManager) { }

    public override void RunMazeBuilder()
    {
        base.RunMazeBuilder();

        List<Vector2Int> wallQueue = new List<Vector2Int>();

        foreach (var wall in GetCellWallNodes(cellOrigen))
        {
            wallQueue.Add(wall);
        }

        SetCellWall(cellOrigen, false);

        while (wallQueue.Count > 0)
        {
            wallQueue.Shuffle();

            Vector2Int wall = wallQueue[0];
            wallQueue.RemoveAt(0);

            List<Vector2Int> wallCells = GetWallCells(wall);

            Vector2Int cell1 = wallCells[0];
            Vector2Int cell2 = wallCells[1];

            if (GetCellWall(cell1) || GetCellWall(cell2))
            {
                if (GetCellWall(cell1))
                {
                    wallQueue = AddCellWallsToQueue(wallQueue, cell1);
                }
                if (GetCellWall(cell2))
                {
                    wallQueue = AddCellWallsToQueue(wallQueue, cell2);
                }

                SetCellWall(cell1, false);
                SetCellWall(cell2, false);
                SetWall(wall, false);
            }
        }
    }

    private List<Vector2Int> AddCellWallsToQueue(List<Vector2Int> wallQueue, Vector2Int cell)
    {
        foreach (var wall in GetCellWallNodes(cell))
        {
            wallQueue.Add(wall);
        }

        return wallQueue;
    }
}