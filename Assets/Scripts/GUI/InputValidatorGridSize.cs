using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValidatorGridSize : MonoBehaviour
{
    [SerializeField] private int min = 0;
    [SerializeField] private int max = 0;

    private TMPro.TMP_InputField textMesh = null;

    private void Start()
    {
        textMesh = GetComponent<TMPro.TMP_InputField>();
    }

    public void ValidateInput()
    {
        int inputInt;

        if (int.TryParse(textMesh.text, out inputInt))
        {
            if (inputInt < min)
            {
                inputInt = min;
            }
            if (inputInt > max)
            {
                inputInt = max;
            }
        }
        else
        {
            inputInt = min;
        }

        textMesh.text = inputInt.ToString();
    }
}
