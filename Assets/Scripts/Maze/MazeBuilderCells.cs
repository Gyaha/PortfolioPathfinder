using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderCells : MazeBuilder
{
    protected enum Direction
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

    public override void RunMazeBuilder()
    {
        SetMapStart(true);

        widthInCells = Mathf.FloorToInt((width - 1) / 2);
        heightInCells = Mathf.FloorToInt((height - 1) / 2);

        SetOrigen(new Vector2Int(0, 0));
        SetCellWall(cellOrigen, false);
        SetTarget(new Vector2Int(widthInCells - 1, heightInCells - 1));
    }

    protected void SetOrigen(Vector2Int cell)
    {
        cellOrigen = cell;
        nodeOrigen = CellToNode(cell);

    }

    protected void SetTarget(Vector2Int cell)
    {
        cellTarget = cell;
        nodeTarget = CellToNode(cell);
    }

    protected Vector2Int GetCellInDirection(Vector2Int cell, Direction cellWall)
    {
        switch (cellWall)
        {
            case Direction.North:
                cell.y += 1;
                break;
            case Direction.South:
                cell.y -= 1;
                break;
            case Direction.East:
                cell.x += 1;
                break;
            case Direction.West:
                cell.x -= 1;
                break;
            default:
                break;
        }
        return cell;
    }

    protected bool GetCellWall(Vector2Int cell)
    {
        return GetWall(CellToNode(cell));
    }

    protected void SetCellWall(Vector2Int cell, bool wall)
    {
        SetWall(CellToNode(cell), wall);
    }

    protected bool GetCellDirectionWall(Vector2Int cell, Direction cellWall)
    {
        return GetWall(CellWallToNode(cell, cellWall));
    }

    protected void SetCellDirectionWall(Vector2Int cell, Direction cellWall, bool wall)
    {
        if (IsCellInBounds(GetCellInDirection(cell, cellWall)) == false)
        {
            Debug.LogError("Trying to set wall to out of bounds cell");
            return;
        }
        SetWall(CellWallToNode(cell, cellWall), wall);
    }

    protected Vector2Int CellWallToNode(Vector2Int cell, Direction cellWall)
    {
        Vector2Int node = CellToNode(cell);
        switch (cellWall)
        {
            case Direction.North:
                node.y += 1;
                break;
            case Direction.South:
                node.y -= 1;
                break;
            case Direction.East:
                node.x += 1;
                break;
            case Direction.West:
                node.x -= 1;
                break;
            default:
                break;
        }
        return node;
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
        if (cell.x < 0 || cell.x >= widthInCells || cell.y < 0 || cell.y >= heightInCells) return false;
        return true;
    }

    protected List<Direction> GetNonVisitedNeighbors(Vector2Int cell)
    {
        List<Direction> neighbors = GetCellNeighbors(cell);

        List<Direction> nonVisited = new List<Direction>();

        foreach (Direction neighbor in neighbors)
        {
            if (GetCellWall(GetCellInDirection(cell, neighbor)))
            {
                nonVisited.Add(neighbor);
            }
        }

        return nonVisited;
    }

    protected List<Direction> GetCellNeighbors(Vector2Int cell)
    {
        List<Direction> neighbors = new List<Direction>();

        Vector2Int cellNorth = new Vector2Int(cell.x, cell.y + 1);
        Vector2Int cellSouth = new Vector2Int(cell.x, cell.y - 1);
        Vector2Int cellEast = new Vector2Int(cell.x + 1, cell.y);
        Vector2Int cellWest = new Vector2Int(cell.x - 1, cell.y);

        if (IsCellInBounds(cellNorth)) neighbors.Add(Direction.North);
        if (IsCellInBounds(cellSouth)) neighbors.Add(Direction.South);
        if (IsCellInBounds(cellEast)) neighbors.Add(Direction.East);
        if (IsCellInBounds(cellWest)) neighbors.Add(Direction.West);

        return neighbors;
    }

    protected List<Vector2Int> GetAllWalls()
    {
        List<Vector2Int> walls = new List<Vector2Int>();

        for (int x = 0; x < widthInCells; x++)
        {
            for (int y = 0; y < heightInCells; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                Vector2Int node = CellToNode(cell);

                if (x > 0)
                {
                    Vector2Int wallWest = new Vector2Int(node.x - 1, node.y);
                    walls.Add(wallWest);
                }
                if (y > 0)
                {
                    Vector2Int wallSouth = new Vector2Int(node.x, node.y - 1);
                    walls.Add(wallSouth);
                }
            }
        }

        return walls;
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

    protected List<Vector2Int> GetWallCells(Vector2Int wall)
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        if (wall.x % 2 == 0)
        {
            Vector2Int cellEast = NodeToCell(new Vector2Int(wall.x + 1, wall.y));
            Vector2Int cellWest = NodeToCell(new Vector2Int(wall.x - 1, wall.y));

            cells.Add(cellEast);
            cells.Add(cellWest);
        }
        else
        {
            Vector2Int cellNorth = NodeToCell(new Vector2Int(wall.x, wall.y + 1));
            Vector2Int cellSouth = NodeToCell(new Vector2Int(wall.x, wall.y - 1));

            cells.Add(cellNorth);
            cells.Add(cellSouth);
        }

        return cells;
    }

    protected Direction GetCellDirection(Vector2Int cell1, Vector2Int cell2)
    {
        if (cell1.y < cell2.y)
        {
            return Direction.North;
        }
        if (cell1.y > cell2.y)
        {
            return Direction.South;
        }
        if (cell1.x < cell2.x)
        {
            return Direction.East;
        }
        if (cell1.x > cell2.x)
        {
            return Direction.West;
        }
        Debug.LogError("Unknown direction");
        return Direction.North;
    }
}