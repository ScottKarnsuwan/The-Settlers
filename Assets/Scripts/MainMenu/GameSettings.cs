using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSettings : MonoBehaviour
{
    //Variables
    [SerializeField] private TMP_Dropdown resDropDown; //Reference to the dropdown UI component in the scene
    private Resolution[] resolutions; //Array of available resolutions
    private List<Resolution> filteredResolutions; //List of resolutions that meet the filter criteria
    private int currentResolutionIndex = 0; //Index of the current resolution in the filteredResolutions list

    void Start()
    {
        // Get available screen resolutions
        resolutions = Screen.resolutions;
        // Create a new list for the filtered resolutions
        filteredResolutions = new List<Resolution>();
        // Clear the options in the dropdown list
        resDropDown.ClearOptions();

        // Add all available resolutions to the filteredResolutions list
        for (int i = 0; i < resolutions.Length; i++)
        {
            filteredResolutions.Add(resolutions[i]);
        }

        // Create a list of string options for the dropdown list
        List<string> settings = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            // Create a string option for each resolution in the filteredResolutions list
            string resOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            // Add the string option to the list of dropdown options
            settings.Add(resOption);
            // Set the current resolution index if it matches the current screen resolution
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        // Add the list of string options to the dropdown list
        resDropDown.AddOptions(settings);
        // Set the initial value of the dropdown list to the current resolution index
        resDropDown.value = currentResolutionIndex;
    }

    // Method to set the screen resolution when a new resolution is selected from the dropdown list
    public void SetRez(int resIndex)
    {
        // Get the resolution corresponding to the selected index in the filteredResolutions list
        Resolution resolution = filteredResolutions[resIndex];
        // Set the screen resolution to the selected resolution
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
