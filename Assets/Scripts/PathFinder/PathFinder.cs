using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private PathManager pathManager;
    public int width { get { return pathManager.width; } }
    public int height { get { return pathManager.height; } }

    protected Vector2Int nodeOrigen;
    protected Vector2Int nodeTarget;

    protected List<Vector2Int> path;
    protected List<Vector2Int> dots;

    private bool[] wallMap;

    public PathFinder(PathManager pathManager)
    {
        this.pathManager = pathManager;
    }

    public PathData Run(Vector2Int nodeOrigen, Vector2Int nodeTarget)
    {
        this.nodeOrigen = nodeOrigen;
        this.nodeTarget = nodeTarget;
        this.path = new List<Vector2Int>();
        this.dots = new List<Vector2Int>();

        wallMap = new bool[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int node = new Vector2Int(x, y);
                wallMap[NodeIndex(node)] = pathManager.GetWall(node);
            }
        }

        RunPathfinder();

        return new PathData(path, dots);
    }

    protected virtual void RunPathfinder() { }

    protected List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        List<Vector2Int> nodeNeighbors = new List<Vector2Int>();

        Vector2Int nodeNorth = new Vector2Int(node.x, node.y + 1);
        Vector2Int nodeSouth = new Vector2Int(node.x, node.y - 1);
        Vector2Int nodeEast = new Vector2Int(node.x + 1, node.y);
        Vector2Int nodeWest = new Vector2Int(node.x - 1, node.y);

        Vector2Int nodeNorthEast = new Vector2Int(node.x + 1, node.y + 1);
        Vector2Int nodeNorthWest = new Vector2Int(node.x - 1, node.y + 1);
        Vector2Int nodeSouthEast = new Vector2Int(node.x + 1, node.y - 1);
        Vector2Int nodeSouthWest = new Vector2Int(node.x - 1, node.y - 1);

        bool nodeEastValid = IsNodePathable(nodeEast);
        bool nodeWestValid = IsNodePathable(nodeWest);

        if (nodeEastValid)
        {
            nodeNeighbors.Add(nodeEast);
        }
        if (nodeWestValid)
        {
            nodeNeighbors.Add(nodeWest);
        }
        if (IsNodePathable(nodeNorth))
        {
            nodeNeighbors.Add(nodeNorth);
            if (IsNodePathable(nodeNorthEast) && nodeEastValid) nodeNeighbors.Add(nodeNorthEast);
            if (IsNodePathable(nodeNorthWest) && nodeWestValid) nodeNeighbors.Add(nodeNorthWest);
        }
        if (IsNodePathable(nodeSouth))
        {
            nodeNeighbors.Add(nodeSouth);
            if (IsNodePathable(nodeSouthEast) && nodeEastValid) nodeNeighbors.Add(nodeSouthEast);
            if (IsNodePathable(nodeSouthWest) && nodeWestValid) nodeNeighbors.Add(nodeSouthWest);
        }

        return nodeNeighbors;
    }

    protected List<Vector2Int> GetNeighborsSimple(Vector2Int node)
    {
        List<Vector2Int> nodeNeighbors = new List<Vector2Int>();

        Vector2Int nodeNorth = new Vector2Int(node.x, node.y + 1);
        Vector2Int nodeSouth = new Vector2Int(node.x, node.y - 1);
        Vector2Int nodeEast = new Vector2Int(node.x + 1, node.y);
        Vector2Int nodeWest = new Vector2Int(node.x - 1, node.y);

        if (IsNodePathable(nodeWest)) nodeNeighbors.Add(nodeWest);
        if (IsNodePathable(nodeEast)) nodeNeighbors.Add(nodeEast);
        if (IsNodePathable(nodeSouth)) nodeNeighbors.Add(nodeSouth);
        if (IsNodePathable(nodeNorth)) nodeNeighbors.Add(nodeNorth);

        return nodeNeighbors;
    }

    protected bool IsNodePathable(Vector2Int node)
    {
        if (IsNodeInBounds(node) == false) return false;
        if (IsWall(node)) return false;
        return true;
    }

    private bool IsNodeInBounds(Vector2Int node)
    {
        if (node.x < 0 || node.x >= width || node.y < 0 || node.y >= height) return false;
        return true;
    }

    private bool IsWall(Vector2Int node)
    {
        return wallMap[NodeIndex(node)];
    }

    protected int NodeIndex(Vector2Int node)
    {
        return (width * node.y) + node.x;
    }

    protected bool CompareNodes(Vector2Int node1, Vector2Int node2)
    {
        return node1.x == node2.x && node1.y == node2.y;
    }

    protected float Distance(Vector2Int node1, Vector2Int node2)
    {
        return Vector2.Distance(node1, node2);
    }

    protected List<Vector2Int> ReconstructPath(Vector2Int[] cameFrom, Vector2Int nodeCurrent, Vector2Int nodeOrigen)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(nodeCurrent);

        while (CompareNodes(nodeCurrent, nodeOrigen) == false)
        {
            nodeCurrent = cameFrom[NodeIndex(nodeCurrent)];
            path.Insert(0, nodeCurrent);
        }

        return path;
    }
}

public struct PathData
{
    public List<Vector2Int> path;
    public List<Vector2Int> dots;

    public PathData(List<Vector2Int> path, List<Vector2Int> dots)
    {
        this.path = path;
        this.dots = dots;
    }
}
