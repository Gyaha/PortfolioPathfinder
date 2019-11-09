using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderCells : MazeBuilder
{
    protected enum WallDirection
    {
        North,
        South,
        East,
        West
    }

    public MazeBuilderCells(MazeManager mazeManager) : base(mazeManager) { }

    protected int widthInCells;
    protected int heightInCells;

    protected Vector2Int cellOrigen;
    protected Vector2Int cellTarget;

    public override void Run()
    {
        SetMapStart(true);

        widthInCells = Mathf.FloorToInt((width - 1) / 2);
        heightInCells = Mathf.FloorToInt((height - 1) / 2);

        SetOrigenCell(new Vector2Int(0, 0));
        SetCell(cellOrigen, false);
        SetTargetCell(new Vector2Int(widthInCells - 1, heightInCells - 1));
    }

    protected void SetOrigenCell(Vector2Int cell)
    {
        cellOrigen = cell;
        nodeOrigen = CellToNode(cell);
    }

    protected void SetTargetCell(Vector2Int cell)
    {
        cellTarget = cell;
        nodeTarget = CellToNode(cell);
    }

    protected Vector2Int GetCellInDirection(Vector2Int cell, WallDirection wallDir)
    {
        switch (wallDir)
        {
            case WallDirection.North:
                cell.y += 1;
                break;
            case WallDirection.South:
                cell.y -= 1;
                break;
            case WallDirection.East:
                cell.x += 1;
                break;
            case WallDirection.West:
                cell.x -= 1;
                break;
            default:
                break;
        }
        return cell;
    }

    protected bool GetCell(Vector2Int cell)
    {
        return GetNode(CellToNode(cell));
    }

    protected void SetCell(Vector2Int cell, bool wall)
    {
        SetNode(CellToNode(cell), wall);
    }

    protected bool GetCellWall(Vector2Int cell, WallDirection wallDir)
    {
        return GetNode(GetCellWallNode(cell, wallDir));
    }

    protected void SetCellWall(Vector2Int cell, WallDirection wallDir, bool wall)
    {
        if (IsCellInBounds(GetCellInDirection(cell, wallDir)) == false)
        {
            Debug.LogError("Trying to set wall to out of bounds cell");
            return;
        }
        SetNode(GetCellWallNode(cell, wallDir), wall);
    }

    protected List<Vector2Int> GetCellWallNodes(Vector2Int cell)
    {
        List<Vector2Int> nodeWalls = new List<Vector2Int>();

        List<WallDirection> neighbors = GetCellNeighbors(cell);

        foreach (var neighbor in neighbors)
        {
            nodeWalls.Add(GetCellWallNode(cell, neighbor));
        }

        return nodeWalls;
    }

    protected Vector2Int GetCellWallNode(Vector2Int cell, WallDirection wallDir)
    {
        Vector2Int nodeWall = CellToNode(cell);
        switch (wallDir)
        {
            case WallDirection.North:
                nodeWall.y += 1;
                break;
            case WallDirection.South:
                nodeWall.y -= 1;
                break;
            case WallDirection.East:
                nodeWall.x += 1;
                break;
            case WallDirection.West:
                nodeWall.x -= 1;
                break;
            default:
                break;
        }
        return nodeWall;
    }

    protected Vector2Int CellToNode(Vector2Int cell)
    {
        Vector2Int node = new Vector2Int();
        node.x = 1 + (cell.x * 2);
        node.y = 1 + (cell.y * 2);
        return node;
    }

    protected Vector2Int NodeToCell(Vector2Int node)
    {
        Vector2Int cell = new Vector2Int();
        cell.x = Mathf.FloorToInt((node.x - 1) / 2);
        cell.y = Mathf.FloorToInt((node.y - 1) / 2);
        return cell;
    }

    protected bool CompareCells(Vector2Int cell1, Vector2Int cell2)
    {
        return cell1 == cell2;
    }

    protected int CellIndex(Vector2Int cell)
    {
        return (cell.y * width) + cell.x;
    }

    protected bool IsCellInBounds(Vector2Int cell)
    {
        return !(cell.x < 0 || cell.x >= widthInCells || cell.y < 0 || cell.y >= heightInCells);
    }

    protected List<WallDirection> GetCellOpenNeighbors(Vector2Int cell)
    {
        List<WallDirection> neighbors = GetCellNeighbors(cell);

        List<WallDirection> openNeighbors = new List<WallDirection>();

        foreach (WallDirection neighbor in neighbors)
        {
            if (GetCell(GetCellInDirection(cell, neighbor)))
            {
                openNeighbors.Add(neighbor);
            }
        }

        return openNeighbors;
    }

    protected List<WallDirection> GetCellNeighbors(Vector2Int cell)
    {
        List<WallDirection> neighbors = new List<WallDirection>();

        Vector2Int cellNorth = new Vector2Int(cell.x, cell.y + 1);
        Vector2Int cellSouth = new Vector2Int(cell.x, cell.y - 1);
        Vector2Int cellEast = new Vector2Int(cell.x + 1, cell.y);
        Vector2Int cellWest = new Vector2Int(cell.x - 1, cell.y);

        if (IsCellInBounds(cellNorth)) neighbors.Add(WallDirection.North);
        if (IsCellInBounds(cellSouth)) neighbors.Add(WallDirection.South);
        if (IsCellInBounds(cellEast)) neighbors.Add(WallDirection.East);
        if (IsCellInBounds(cellWest)) neighbors.Add(WallDirection.West);

        return neighbors;
    }

    protected List<Vector2Int> GetAllWalls()
    {
        List<Vector2Int> nodeWalls = new List<Vector2Int>();

        for (int x = 0; x < widthInCells; x++)
        {
            for (int y = 0; y < heightInCells; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                Vector2Int node = CellToNode(cell);

                if (x > 0)
                {
                    Vector2Int nodeWallWest = new Vector2Int(node.x - 1, node.y);
                    nodeWalls.Add(nodeWallWest);
                }
                if (y > 0)
                {
                    Vector2Int nodeWallSouth = new Vector2Int(node.x, node.y - 1);
                    nodeWalls.Add(nodeWallSouth);
                }
            }
        }

        return nodeWalls;
    }

    protected List<Vector2Int> GetAllCells()
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        for (int x = 0; x < widthInCells; x++)
        {
            for (int y = 0; y < heightInCells; y++)
            {
                cells.Add(new Vector2Int(x, y));
            }
        }

        return cells;
    }

    protected List<Vector2Int> GetWallCells(Vector2Int nodeWall)
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        if (nodeWall.x % 2 == 0)
        {
            Vector2Int cellEast = NodeToCell(new Vector2Int(nodeWall.x + 1, nodeWall.y));
            Vector2Int cellWest = NodeToCell(new Vector2Int(nodeWall.x - 1, nodeWall.y));

            cells.Add(cellEast);
            cells.Add(cellWest);
        }
        else
        {
            Vector2Int cellNorth = NodeToCell(new Vector2Int(nodeWall.x, nodeWall.y + 1));
            Vector2Int cellSouth = NodeToCell(new Vector2Int(nodeWall.x, nodeWall.y - 1));

            cells.Add(cellNorth);
            cells.Add(cellSouth);
        }

        return cells;
    }

    protected WallDirection GetCellDirection(Vector2Int cell1, Vector2Int cell2)
    {
        if (cell1.y < cell2.y)
        {
            return WallDirection.North;
        }
        if (cell1.y > cell2.y)
        {
            return WallDirection.South;
        }
        if (cell1.x < cell2.x)
        {
            return WallDirection.East;
        }
        if (cell1.x > cell2.x)
        {
            return WallDirection.West;
        }
        Debug.LogError("Unknown direction");
        return WallDirection.North;
    }

}