using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private VisualizationManager visualizationManager = null;

    public int width { get { return gridManager.width; } }
    public int height { get { return gridManager.height; } }
    public Vector2Int nodeOrigen { get { return new Vector2Int(gridManager.cellOrigen.x, gridManager.cellOrigen.y); } }
    public Vector2Int nodeTarget { get { return new Vector2Int(gridManager.cellTarget.x, gridManager.cellTarget.y); } }

    private MazeMode mazeMode = MazeMode.Random;

    private MazeBuilder mazeBuilderRandom;
    private MazeBuilder mazebuilderPerlin;
    private MazeBuilder mazebuilderPerlin2;
    private MazeBuilder mazebuilderCells;
    private MazeBuilder mazebuilderDepth;
    private MazeBuilder mazeBuilderKruskal;
    private MazeBuilder mazeBuilderPrims;

    private void Awake()
    {
        mazeBuilderRandom = new MazeBuilderRandom(this);
        mazebuilderPerlin = new MazeBuilderPerlin(this);
        mazebuilderPerlin2 = new MazeBuilderPerlin2(this);
        mazebuilderCells = new MazeBuilderCells(this);
        mazebuilderDepth = new MazeBuilderDepth(this);
        mazeBuilderKruskal = new MazeBuilderKruskal(this);
        mazeBuilderPrims = new MazeBuilderPrims(this);
    }

    public void StartMazeBuilder(MazeMode mazeMode)
    {
        this.mazeMode = mazeMode;

        MazeData mazeData;

        switch (mazeMode)
        {
            case MazeMode.Random:
                mazeData = mazeBuilderRandom.Run();
                break;
            case MazeMode.Perlin:
                mazeData = mazebuilderPerlin.Run();
                break;
            case MazeMode.Perlin2:
                mazeData = mazebuilderPerlin2.Run();
                break;
            case MazeMode.Cells:
                mazeData = mazebuilderCells.Run();
                break;
            case MazeMode.Depth:
                mazeData = mazebuilderDepth.Run();
                break;
            case MazeMode.Kruskal:
                mazeData = mazeBuilderKruskal.Run();
                break;
            case MazeMode.Prims:
                mazeData = mazeBuilderPrims.Run();
                break;
            default:
                mazeData = new MazeData(false, new Vector2Int(0, 0), new Vector2Int(width - 1, height - 1), new List<WallInfo>());
                break;
        }

        visualizationManager.StartVMaze(mazeData);
    }

}

public enum MazeMode
{
    Random,
    Perlin,
    Perlin2,
    Cells,
    Depth,
    Kruskal,
    Prims
}