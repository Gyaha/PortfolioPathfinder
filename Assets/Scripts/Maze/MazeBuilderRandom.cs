using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderRandom : MazeBuilder
{
    public MazeBuilderRandom(MazeManager mazeManager) : base(mazeManager) { }

    public override void Run()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int node = new Vector2Int(x, y);
                if (CompareNodes(node, nodeOrigen) == false && CompareNodes(node, nodeTarget) == false)
                {
                    bool wall = System.Convert.ToBoolean(Random.Range(0, 2));
                    if (wall)
                    {
                        SetNode(node, true);
                    }
                }
            }
        }
    }
}