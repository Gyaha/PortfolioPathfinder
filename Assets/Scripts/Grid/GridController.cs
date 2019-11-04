using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private CellController cellPrefab = null;

    private CellController[] cells = new CellController[0];

    private int _width;
    public int width { get { return _width; } }
    private int _height;
    public int height { get { return _height; } }

    public void CreateGrid(int width, int height)
    {
        DestroyCells();
        this._width = width;
        this._height = height;
        cells = new CellController[height * width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cellObject = Instantiate(cellPrefab.gameObject, transform);
                CellController cell = cellObject.GetComponent<CellController>();
                cells[XyToIndex(x, y)] = cell;

                cell.Init(this, x, y);

                cell.transform.localPosition = new Vector3(x, 0, y);
            }
        }
    }

    public CellController GetCell(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return cells[XyToIndex(x, y)];
    }

    private int XyToIndex(int x, int y)
    {
        return (y * width) + x;
    }

    private void DestroyCells()
    {
        foreach (var cell in cells)
        {
            Destroy(cell.gameObject);
        }
    }

    public void Clear(bool walls, bool dots, bool paths, bool origens = false, bool targets = false)
    {
        foreach (CellController cell in cells)
        {
            if (walls) cell.wall = false;
            if (dots) cell.dot = false;
            if (paths) ClearPaths(cell);
            if (origens) cell.origen = false;
            if (targets) cell.target = false;
        }
    }

    private void ClearPaths(CellController cell)
    {
        cell.path = false;
        cell.pathEast = false;
        cell.pathNorth = false;
        cell.pathNorthEast = false;
        cell.pathNorthWest = false;
    }

    public void Fill()
    {
        foreach (CellController cell in cells)
        {
            cell.wall = true;
        }
    }
}
