using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Light globalLight = null;
    [SerializeField] private UnityEngine.UI.Toggle toggleShadow = null;
    [SerializeField] private UnityEngine.UI.Toggle toggleAA = null;
    [SerializeField] private UnityEngine.UI.Toggle toggleAO = null;
    [SerializeField] private GameObject buttonQuit = null;

    [SerializeField] private UnityEngine.PostProcessing.PostProcessingProfile postProcessingProfile = null;

    [SerializeField] private bool shadow = true;
    [SerializeField] private bool aA = true;
    [SerializeField] private bool aO = true;
    [SerializeField] private bool quit = true;

    private void Start()
    {
        toggleShadow.isOn = shadow;
        toggleAA.isOn = aA;
        toggleAO.isOn = aO;

        UpdateShadows();
        UpdateAA();
        UpdateAO();

        buttonQuit.SetActive(quit);
    }


    public void UpdateShadows()
    {
        if (toggleShadow.isOn)
        {
            globalLight.shadows = LightShadows.Soft;
        }
        else
        {
            globalLight.shadows = LightShadows.None;
        }
    }

    public void UpdateAA()
    {
        postProcessingProfile.antialiasing.enabled = toggleAA.isOn;
    }

    public void UpdateAO()
    {
        postProcessingProfile.ambientOcclusion.enabled = toggleAO.isOn;
    }
}
