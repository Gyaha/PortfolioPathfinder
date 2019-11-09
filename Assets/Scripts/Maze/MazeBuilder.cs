using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder
{
    private MazeManager mazeManager;

    protected int width { get { return mazeManager.width; } }
    protected int height { get { return mazeManager.height; } }

    protected Vector2Int nodeOrigen;
    protected Vector2Int nodeTarget;

    private bool startsFilled;

    private List<WallInfo> walls;
    private bool[] wallMap;

    public MazeBuilder(MazeManager mazeManager)
    {
        this.mazeManager = mazeManager;
    }

    public MazeData RunMazeBuilder()
    {
        nodeOrigen = mazeManager.nodeOrigen;
        nodeTarget = mazeManager.nodeTarget;

        walls = new List<WallInfo>();
        wallMap = new bool[width * height];
        SetMapStart(false);

        Run();

        return new MazeData(startsFilled, nodeOrigen, nodeTarget, walls);
    }

    public virtual void Run() { }

    protected bool CompareNodes(Vector2Int node1, Vector2Int node2)
    {
        return node1 == node2;
    }

    protected void SetMapStart(bool startsFilled)
    {
        this.startsFilled = startsFilled;
        for (int i = 0; i < wallMap.Length; i++) wallMap[i] = startsFilled;
    }

    protected bool GetNode(Vector2Int node)
    {
        return wallMap[NodeToIndex(node)];
    }

    protected void SetNode(Vector2Int node, bool wall)
    {
        wallMap[NodeToIndex(node)] = wall;
        walls.Add(new WallInfo(node, wall));
    }

    protected bool IsNodeInBounds(Vector2Int node)
    {
        return !(node.x < 0 || node.x >= width || node.y < 0 || node.y >= height);
    }

    private int NodeToIndex(Vector2Int node)
    {
        return (node.y * width) + node.x;
    }
}

public struct MazeData
{
    public bool startsFilled;

    public Vector2Int nodeOrigen;
    public Vector2Int nodeTarget;

    public List<WallInfo> wallInfo;

    public MazeData(bool startsFilled, Vector2Int nodeOrigen, Vector2Int nodeTarget, List<WallInfo> wallInfo)
    {
        this.startsFilled = startsFilled;
        this.nodeOrigen = nodeOrigen;
        this.nodeTarget = nodeTarget;
        this.wallInfo = wallInfo;
    }
}

public struct WallInfo
{
    public Vector2Int node;
    public bool wall;

    public WallInfo(Vector2Int node, bool wall)
    {
        this.node = node;
        this.wall = wall;
    }
}