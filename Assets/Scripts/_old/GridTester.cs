using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTester : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private float timer = 1;

    [SerializeField] private bool randomPut = false;
    [SerializeField] private bool randomWalls = false;
    [SerializeField] private bool randomDots = false;
    [SerializeField] private bool randomPath = false;
    [SerializeField] private bool randomTarget = false;
    [SerializeField] private bool randomOrigen = false;

    [SerializeField] private Vector2Int gridSize = new Vector2Int(16, 16);

    [SerializeField] private bool remakeGrid = false;
    [SerializeField] private float gridRemakeTimer = 1;

    [SerializeField] private bool resetGrid = false;
    [SerializeField] private float gridResetTimer = 1;


    private IEnumerator putRandom;
    private IEnumerator IRemakeGrid;
    private IEnumerator IResetGrid;


    private CellController getRandomCell()
    {
        int x = Random.Range(0, gridManager.width);
        int y = Random.Range(0, gridManager.height);
        return gridManager.GetCell(x, y);
    }

    private IEnumerator PutRandom()
    {
        while (true)
        {
            CellController cell = getRandomCell();
            if (cell)
            {
                switch (Random.Range(0, 5))
                {
                    case 0:
                        if (randomWalls) cell.wall = !cell.wall;
                        break;
                    case 1:
                        if (randomDots) cell.dot = !cell.dot;
                        break;
                    case 2:
                        if (randomPath) cell.path = !cell.path;
                        break;
                    case 3:
                        if (randomOrigen) cell.origen = !cell.origen;
                        break;
                    case 4:
                        if (randomTarget) cell.target = !cell.target;
                        break;
                    default:
                        Debug.LogError("asdf");
                        break;
                }


            }
            yield return new WaitForSeconds(timer);
        }
    }

    private IEnumerator CreateGrid()
    {
        while (true)
        {
            yield return new WaitForSeconds(gridRemakeTimer);
            //gridManager.CreateGrid(gridSize.x, gridSize.y);
        }
    }

    private IEnumerator ResetGrid()
    {
        while (true)
        {
            yield return new WaitForSeconds(gridResetTimer);
            gridManager.ClearWalls();
        }
    }

    private void Start()
    {
        putRandom = PutRandom();
        IRemakeGrid = CreateGrid();
        IResetGrid = ResetGrid();

        //gridManager.CreateGrid(gridSize.x, gridSize.y);

        if (remakeGrid)
        {
            StartCoroutine(IRemakeGrid);
        }
        if (resetGrid)
        {
            StartCoroutine(IResetGrid);
        }
        if (randomPut)
        {
            StartCoroutine(putRandom);
        }
    }
}
