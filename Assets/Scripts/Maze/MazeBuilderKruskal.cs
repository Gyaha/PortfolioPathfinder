using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderKruskal : MazeBuilderCells
{
    public MazeBuilderKruskal(MazeManager mazeManager) : base(mazeManager) { }

    public override void RunMazeBuilder()
    {
        base.RunMazeBuilder();

        List<Vector2Int> walls = GetAllWalls();

        List<Vector2Int> cells = GetAllCells();

        List<List<Vector2Int>> cellGroups = new List<List<Vector2Int>>();
        foreach (var cell in cells)
        {
            List<Vector2Int> cellGroup = new List<Vector2Int>();
            cellGroup.Add(cell);
            cellGroups.Add(cellGroup);
        }



    }
}