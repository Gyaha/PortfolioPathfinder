using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private SimpleAnimation centerPosition = null;
    [SerializeField] private SimpleAnimation wallScale = null;
    [SerializeField] private SimpleAnimation dotScale = null;
    [SerializeField] private SimpleAnimation pathScale = null;
    [SerializeField] private SimpleAnimation pathNorthScale = null;
    [SerializeField] private SimpleAnimation pathEastScale = null;
    [SerializeField] private SimpleAnimation pathNorthEastScale = null;
    [SerializeField] private SimpleAnimation pathNorthWestScale = null;
    [SerializeField] private SimpleAnimation targetScale = null;
    [SerializeField] private SimpleAnimation origenScale = null;

    private GridController gridController;
    private int _x;
    public int x { get { return _x; } }
    private int _y;
    public int y { get { return _y; } }

    private bool _wall;
    public bool wall
    {
        get
        {
            return _wall;
        }
        set
        {
            _wall = value;
            wallScale.Trigger(_wall);
        }
    }

    private bool _dot;
    public bool dot
    {
        get
        {
            return _dot;
        }
        set
        {
            _dot = value;
            dotScale.Trigger(_dot);
        }
    }

    private bool _path;
    public bool path
    {
        get
        {
            return _path;
        }
        set
        {
            _path = value;
            pathScale.Trigger(_path);
        }
    }

    private bool _pathNorth;
    public bool pathNorth
    {
        get
        {
            return _pathNorth;
        }
        set
        {
            _pathNorth = value;
            pathNorthScale.Trigger(_pathNorth);
        }
    }

    private bool _pathEast;
    public bool pathEast
    {
        get
        {
            return _pathEast;
        }
        set
        {
            _pathEast = value;
            pathEastScale.Trigger(_pathEast);
        }
    }

    private bool _pathNorthEast;
    public bool pathNorthEast
    {
        get
        {
            return _pathNorthEast;
        }
        set
        {
            _pathNorthEast = value;
            pathNorthEastScale.Trigger(_pathNorthEast);
        }
    }

    private bool _pathNorthWest;
    public bool pathNorthWest
    {
        get
        {
            return _pathNorthWest;
        }
        set
        {
            _pathNorthWest = value;
            pathNorthWestScale.Trigger(_pathNorthWest);
        }
    }

    private bool _target;
    public bool target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
            targetScale.Trigger(_target);
        }
    }

    private bool _origen;
    public bool origen
    {
        get
        {
            return _origen;
        }
        set
        {
            _origen = value;
            origenScale.Trigger(_origen);
        }
    }

    public void Init(GridController gridController, int x, int y)
    {
        this.gridController = gridController;
        this._x = x;
        this._y = y;
    }

    public void OnEnter()
    {
        centerPosition.Trigger(true);
    }

    public void OnExit()
    {
        centerPosition.Trigger(false);
    }

}

/*
    private void UpdateConnectors()
    {
        UpdateConnectorsLocal();

        CellController cellSouth = gridController.GetCell(x, y - 1);
        if (cellSouth)
        {
            cellSouth.UpdateConnectorsLocal();
        }
        CellController cellWest = gridController.GetCell(x - 1, y);
        if (cellWest)
        {
            cellWest.UpdateConnectorsLocal();
        }
    }

    public void UpdateConnectorsLocal()
    {
        if (path)
        {
            CellController cellNorth = gridController.GetCell(x, y + 1);
            if (cellNorth)
            {
                pathNorth = cellNorth.path;
            }
            CellController cellEast = gridController.GetCell(x + 1, y);
            if (cellEast)
            {
                pathEast = cellEast.path;
            }
        }
        else
        {
            pathNorth = false;
            pathEast = false;
        }
    }
    */
