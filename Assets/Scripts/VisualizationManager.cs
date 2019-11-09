using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private float dotTime = 0;
    [SerializeField] private float pathTime = 0;
    [SerializeField] private float wallTime = 0;

    private bool vDone = false;

    public void StartVPath(PathData pathData)
    {
        StopV();
        gridManager.ClearPathAndDots();

        IEnumerator vBoth = VBoth(pathData.dots, pathData.path);
        StartCoroutine(vBoth);
    }

    public void StartVMaze(MazeData mazeData)
    {
        StopV();

        gridManager.ClearOrigenAndTarget();

        gridManager.ClearWalls();

        if (mazeData.startsFilled)
        {
            gridManager.FillWalls();
        }

        gridManager.SetOrigenAndTarget(mazeData.nodeOrigen.x, mazeData.nodeOrigen.y, mazeData.nodeTarget.x, mazeData.nodeTarget.y);

        IEnumerator vWalls = VWalls(mazeData.wallInfo);
        StartCoroutine(vWalls);
    }

    public void StopV()
    {
        StopAllCoroutines();
    }

    private IEnumerator VBoth(List<Vector2Int> dots, List<Vector2Int> path)
    {
        IEnumerator vDots = VDots(dots);
        IEnumerator vPath = VPath(path);
        StartCoroutine(vDots);
        vDone = false;
        while (!vDone)
        {
            yield return null;
        }
        StartCoroutine(vPath);
        vDone = false;
        while (!vDone)
        {
            yield return null;
        }
    }

    private IEnumerator VDots(List<Vector2Int> cells)
    {
        yield return null;
        while (cells.Count > 0)
        {
            Vector2Int cell = cells[0];
            cells.RemoveAt(0);

            gridManager.SetDot(cell.x, cell.y);

            yield return new WaitForSeconds(dotTime);
        }
        vDone = true;
    }

    private IEnumerator VPath(List<Vector2Int> cells)
    {
        yield return null;

        Vector2Int cellPrev;
        Vector2Int cellCurrent;

        if (cells.Count > 0)
        {
            cellCurrent = cells[0];
            cells.RemoveAt(0);

            gridManager.SetPath(cellCurrent.x, cellCurrent.y);

            yield return new WaitForSeconds(pathTime);

            while (cells.Count > 0)
            {
                cellPrev = cellCurrent;

                cellCurrent = cells[0];
                cells.RemoveAt(0);

                gridManager.SetPathTo(cellPrev.x, cellPrev.y, cellCurrent.x, cellCurrent.y);

                yield return new WaitForSeconds(pathTime);
            }
        }
        vDone = true;
    }

    private IEnumerator VWalls(List<WallInfo> cells)
    {
        yield return null;

        while (cells.Count > 0)
        {
            WallInfo cell = cells[0];
            cells.RemoveAt(0);

            gridManager.SetWall(cell.node.x, cell.node.y, cell.wall);

            yield return new WaitForSeconds(wallTime);
        }
    }
}
