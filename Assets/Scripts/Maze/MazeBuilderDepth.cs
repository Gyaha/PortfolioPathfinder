using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderDepth : MazeBuilderCells
{
    public MazeBuilderDepth(MazeManager mazeManager) : base(mazeManager) { }

    public override void Run()
    {
        base.Run();

        List<Vector2Int> queue = new List<Vector2Int>();
        queue.Add(cellOrigen);

        while (queue.Count > 0)
        {
            Vector2Int cellCurrent = queue[0];

            List<WallDirection> openDirections = GetCellOpenNeighbors(cellCurrent);

            if (openDirections.Count > 0)
            {
                openDirections.Shuffle();

                WallDirection direction = openDirections[0];

                Vector2Int cellTarget = GetCellInDirection(cellCurrent, direction);

                SetCellWall(cellCurrent, direction, false);

                SetCell(cellTarget, false);

                queue.Insert(0, cellTarget);
            }
            else
            {
                queue.RemoveAt(0);
            }
        }
    }
}