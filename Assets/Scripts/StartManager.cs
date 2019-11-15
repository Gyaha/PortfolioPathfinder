using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonManager = null;

    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private GridSize gridSize = GridSize.Medium;

    void Start()
    {
        gridManager.CreateGrid(gridSize);
        buttonManager.ChangeMouseSelection(0);
    }

}
