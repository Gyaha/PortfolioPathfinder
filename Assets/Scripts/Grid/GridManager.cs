using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridController gridController = null;
    [SerializeField] private VisualizationManager visualizationManager = null;
    [SerializeField] private CameraManager cameraManager = null;

    [SerializeField] private int gridSizeSmall = 16;
    [SerializeField] private int gridSizeMedium = 32;
    [SerializeField] private int gridSizeLarge = 64;

    public int height { get { return gridController.height; } }
    public int width { get { return gridController.width; } }

    private CellController _cellOrigen;
    public CellController cellOrigen
    {
        get
        {
            return _cellOrigen;
        }
    }
    private CellController _cellTarget;
    public CellController cellTarget
    {
        get
        {
            return _cellTarget;
        }
    }

    public void ClearOrigenAndTarget()
    {
        if (cellOrigen)
        {
            cellOrigen.origen = false;
        }
        _cellOrigen = null;

        if (cellTarget)
        {
            cellTarget.target = false;
        }
        _cellTarget = null;
    }

    public void SetOrigenAndTarget(int origenX, int origenY, int targetX, int targetY)
    {
        ClearPathAndDots();
        if (cellOrigen)
        {
            cellOrigen.origen = false;
        }
        _cellOrigen = GetCell(origenX, origenY);
        cellOrigen.wall = false;
        cellOrigen.origen = true;

        if (cellTarget)
        {
            cellTarget.target = false;
        }
        _cellTarget = GetCell(targetX, targetY);
        cellTarget.wall = false;
        cellTarget.target = true;
    }

    private GridSize gridSize;

    public void CreateGrid(GridSize gridSize)
    {
        this.gridSize = gridSize;
        if (gridSize == GridSize.Small)
        {
            CreateGrid(gridSizeSmall, gridSizeSmall);
        }
        else if (gridSize == GridSize.Medium)
        {
            CreateGrid(gridSizeMedium, gridSizeMedium);
        }
        else if (gridSize == GridSize.Large)
        {
            CreateGrid(gridSizeLarge, gridSizeLarge);
        }
        else
        {
            Debug.LogError("Unknown grid size: " + gridSize.ToString());
        }

        cameraManager.SetSize(width, height);
    }

    private void CreateGrid(int width, int height)
    {
        gridController.CreateGrid(width, height);
        _cellOrigen = null;
        _cellTarget = null;

        int offset = Mathf.FloorToInt(height / 2);

        SetOrigen(1, offset);
        SetTarget(width - 2, offset);
    }

    public CellController GetCell(int x, int y)
    {
        return gridController.GetCell(x, y);
    }

    public void FillWalls()
    {
        gridController.Fill();
    }

    public void ClearWalls()
    {
        gridController.Clear(true, false, false);
    }

    public void ClearPathAndDots()
    {
        gridController.Clear(false, true, true);
    }

    public void SetOrigen(int x, int y)
    {
        if (cellTarget && cellTarget.x == x && cellTarget.y == y)
        {
            return;
        }
        if (cellOrigen)
        {
            cellOrigen.origen = false;
        }
        visualizationManager.StopV();// TODO: Move this?
        ClearPathAndDots();
        _cellOrigen = GetCell(x, y);
        cellOrigen.wall = false;
        cellOrigen.origen = true;
    }

    public void SetTarget(int x, int y)
    {
        if (cellOrigen && cellOrigen.x == x && cellOrigen.y == y)
        {
            return;
        }
        if (cellTarget)
        {
            cellTarget.target = false;
        }
        visualizationManager.StopV();
        ClearPathAndDots();
        _cellTarget = GetCell(x, y);
        cellTarget.wall = false;
        cellTarget.target = true;
    }

    public void SetPath(int x, int y)
    {
        CellController cell = GetCell(x, y);
        cell.path = true;
    }

    public void SetPathTo(int fromX, int fromY, int toX, int toY)
    {
        CellController fromCell = GetCell(fromX, fromY);
        CellController toCell = GetCell(toX, toY);

        if (fromCell.y > toCell.y)
        {
            if (fromCell.x > toCell.x)
            {
                toCell.pathNorthEast = true;
            }
            else if (fromCell.x < toCell.x)
            {
                toCell.pathNorthWest = true;
            }
            else
            {
                toCell.pathNorth = true;
            }
        }
        else if (fromCell.y < toCell.y)
        {
            if (fromCell.x > toCell.x)
            {
                fromCell.pathNorthWest = true;
            }
            else if (fromCell.x < toCell.x)
            {
                fromCell.pathNorthEast = true;
            }
            else
            {
                fromCell.pathNorth = true;
            }
        }
        else
        {
            if (fromCell.x > toCell.x)
            {
                toCell.pathEast = true;
            }
            else
            {
                fromCell.pathEast = true;
            }
        }

        toCell.path = true;
    }

    public void SetDot(int x, int y)
    {
        if (cellOrigen && cellOrigen.x == x && cellOrigen.y == y)
        {
            return;
        }
        if (cellTarget && cellTarget.x == x && cellTarget.y == y)
        {
            return;
        }
        CellController cell = GetCell(x, y);
        cell.dot = true;
    }

    public bool GetWall(int x, int y)
    {
        CellController cell = GetCell(x, y);
        return cell.wall;
    }

    public void SetWall(int x, int y, bool wall)
    {
        if (cellOrigen && cellOrigen.x == x && cellOrigen.y == y)
        {
            return;
        }
        if (cellTarget && cellTarget.x == x && cellTarget.y == y)
        {
            return;
        }
        CellController cell = GetCell(x, y);
        cell.wall = wall;
    }

    public void OnHover(int x, int y, bool enter)
    {
        CellController cell = GetCell(x, y);
        if (enter)
        {
            cell.OnEnter();
        }
        else
        {
            cell.OnExit();
        }
    }

}


public enum GridSize
{
    Small,
    Medium,
    Large
}