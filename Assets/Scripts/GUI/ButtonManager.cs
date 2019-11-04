using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PathManager pathManager = null;
    [SerializeField] private TMPro.TMP_Dropdown pathSelector = null;

    [SerializeField] private MazeManager mazeManager = null;
    [SerializeField] private TMPro.TMP_Dropdown mazeSelector = null;

    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private TMPro.TMP_Dropdown gridSelector = null;

    [SerializeField] private MouseManager mouseManager = null;

    [SerializeField] private UnityEngine.UI.Image buttonWall = null;
    [SerializeField] private UnityEngine.UI.Image buttonOrigen = null;
    [SerializeField] private UnityEngine.UI.Image buttonTarget = null;

    [SerializeField] private Color buttonDefaultColor = Color.white;
    [SerializeField] private Color buttonSelectedColor = Color.green;

    public void CreateGrid()
    {
        GridSize gridSize = (GridSize)gridSelector.value;

        gridManager.CreateGrid(gridSize);
    }

    public void ClearGrid()
    {
        gridManager.ClearWalls();
    }

    public void ChangeMouseSelection(int selection)
    {
        MouseSelection mouseSelection = (MouseSelection)selection;

        SelectButton(mouseSelection);

        mouseManager.SetSelection(mouseSelection);
    }

    private void SelectButton(MouseSelection mouseSelection)
    {
        SetButtonColor(buttonWall, buttonDefaultColor);
        SetButtonColor(buttonOrigen, buttonDefaultColor);
        SetButtonColor(buttonTarget, buttonDefaultColor);

        switch (mouseSelection)
        {
            case MouseSelection.Wall:
                SetButtonColor(buttonWall, buttonSelectedColor);
                break;
            case MouseSelection.Origen:
                SetButtonColor(buttonOrigen, buttonSelectedColor);
                break;
            case MouseSelection.Target:
                SetButtonColor(buttonTarget, buttonSelectedColor);
                break;
            default:
                Debug.LogError("Unknown mouse selection");
                break;
        }
    }

    private void SetButtonColor(UnityEngine.UI.Image button, Color color)
    {
        button.color = color;
    }

    public void StartPathfinding()
    {
        PathMode pathMode = (PathMode)pathSelector.value;

        pathManager.StartPathfinder(pathMode);
    }

    public void StartMazeBuilding()
    {
        MazeMode mazeMode = (MazeMode)mazeSelector.value;

        mazeManager.StartMazeBuilder(mazeMode);
    }

}
