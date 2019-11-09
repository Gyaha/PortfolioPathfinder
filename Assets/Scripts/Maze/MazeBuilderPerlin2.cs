using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderPerlin2 : MazeBuilder
{
    public MazeBuilderPerlin2(MazeManager mazeManager) : base(mazeManager) { }

    private float perlinMultiplier = 0.25F;
    private float perlinCut = 0.5F;

    public override void Run()
    {
        SetMapStart(true);

        int perlinOffset = Random.Range(0, 10000);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int node = new Vector2Int(x, y);
                if (CompareNodes(node, nodeOrigen) == false && CompareNodes(node, nodeTarget) == false)
                {
                    float perlin = Mathf.PerlinNoise((perlinOffset + x) * perlinMultiplier, (perlinOffset + y) * perlinMultiplier);

                    if (perlin < perlinCut)
                    {
                        SetNode(node, false);
                    }
                }
            }
        }
    }
}