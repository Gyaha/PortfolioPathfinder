using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderDepth : MazeBuilderCells
{
    public MazeBuilderDepth(MazeManager mazeManager) : base(mazeManager) { }

    public override void RunMazeBuilder()
    {
        base.RunMazeBuilder();

        List<Vector2Int> openList = new List<Vector2Int>();
        openList.Add(cellOrigen);


        while (openList.Count > 0)
        {
            Vector2Int cellCurrent = openList[0];

            List<Direction> openDirections = GetNonVisitedNeighbors(cellCurrent);

            if (openDirections.Count > 0)
            {
                openDirections.Shuffle();

                Direction direction = openDirections[0];

                Vector2Int cellTarget = GetCellInDirection(cellCurrent, direction);

                SetCellDirectionWall(cellCurrent, direction, false);

                SetCellWall(cellTarget, false);

                openList.Insert(0, cellTarget);
            }
            else
            {
                openList.RemoveAt(0);
            }
        }
    }
}