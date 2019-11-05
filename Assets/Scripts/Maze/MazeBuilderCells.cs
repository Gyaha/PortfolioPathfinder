using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderCells : MazeBuilder
{
    protected enum CellWall
    {
        North,
        South,
        East,
        West
    }

    public MazeBuilderCells(MazeManager mazeManager) : base(mazeManager) { }

    protected int widthInCells;
    protected int heightInCells;

    public override void RunMazeBuilder()
    {
        SetMapStart(true);

        widthInCells = Mathf.FloorToInt((width - 1) / 2);
        heightInCells = Mathf.FloorToInt((height - 1) / 2);

        nodeOrigen = CellToNode(new Vector2Int(0, 0));
        nodeTarget = CellToNode(new Vector2Int(widthInCells - 1, heightInCells - 1));

        for (int x = 0; x < widthInCells; x++)
        {
            for (int y = 0; y < heightInCells; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                SetCell(cell, false);
                CellWall cellWall = (CellWall)Random.Range(0, 5);
                if (IsCellInBounds(CellBehindCellWall(cell, cellWall)))
                {
                    SetCellWall(cell, cellWall, false);
                }
            }
        }
    }

    protected Vector2Int CellBehindCellWall(Vector2Int cell, CellWall cellWall)
    {
        switch (cellWall)
        {
            case CellWall.North:
                cell.y += 1;
                break;
            case CellWall.South:
                cell.y -= 1;
                break;
            case CellWall.East:
                cell.x += 1;
                break;
            case CellWall.West:
                cell.x -= 1;
                break;
            default:
                break;
        }
        return cell;
    }

    protected bool GetCell(Vector2Int cell)
    {
        return GetWall(CellToNode(cell));
    }

    protected void SetCell(Vector2Int cell, bool wall)
    {
        SetWall(CellToNode(cell), wall);
    }

    protected bool GetCellWall(Vector2Int cell, CellWall cellWall)
    {
        return GetWall(CellWallToNode(cell, cellWall));
    }

    protected void SetCellWall(Vector2Int cell, CellWall cellWall, bool wall)
    {
        SetWall(CellWallToNode(cell, cellWall), wall);
    }

    protected Vector2Int CellWallToNode(Vector2Int cell, CellWall cellWall)
    {
        Vector2Int node = CellToNode(cell);
        switch (cellWall)
        {
            case CellWall.North:
                node.y += 1;
                break;
            case CellWall.South:
                node.y -= 1;
                break;
            case CellWall.East:
                node.x += 1;
                break;
            case CellWall.West:
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

    protected bool IsCellInBounds(Vector2Int cell)
    {
        if (cell.x < 0 || cell.x >= widthInCells || cell.y < 0 || cell.y >= heightInCells) return false;
        return true;
    }
}