using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private VisualizationManager visualizationManager = null;

    private PathFinder breathFirst;
    private PathFinder aStar;
    private PathFinder depthFirst;

    public int width { get { return gridManager.width; } }
    public int height { get { return gridManager.height; } }

    private PathMode pathMode = PathMode.BreathFirst;

    private void Awake()
    {
        breathFirst = new PathFinderBreathFirst(this);
        aStar = new PathFinderAStar(this);
        depthFirst = new PathFinderDepthFirst(this);
    }

    public void StartPathfinder(PathMode pathMode)
    {
        this.pathMode = pathMode;

        Vector2Int nodeOrigen = new Vector2Int(gridManager.cellOrigen.x, gridManager.cellOrigen.y);
        Vector2Int nodeTarget = new Vector2Int(gridManager.cellTarget.x, gridManager.cellTarget.y);

        PathData pathData;

        if (pathMode == PathMode.BreathFirst)
        {
            pathData = breathFirst.Run(nodeOrigen, nodeTarget);
        }
        else if (pathMode == PathMode.AStar)
        {
            pathData = aStar.Run(nodeOrigen, nodeTarget);
        }
        else if (pathMode == PathMode.DepthFirst)
        {
            pathData = depthFirst.Run(nodeOrigen, nodeTarget);
        }
        else
        {
            Debug.LogError("Unknown pathfinder: " + pathMode.ToString());
            pathData = new PathData(new List<Vector2Int>(), new List<Vector2Int>());
        }

        visualizationManager.StartVPath(pathData);
    }

    public bool GetWall(Vector2Int node)
    {
        return gridManager.GetWall(node.x, node.y);
    }
}

public enum PathMode
{
    AStar,
    BreathFirst,
    DepthFirst
}