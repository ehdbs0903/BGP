using UnityEngine;
using System.Collections.Generic;

public class InitialSettings : MonoBehaviour
{
    private List<Vector2Int> resolutions = new List<Vector2Int>() {
        new Vector2Int(1920, 1080),
        new Vector2Int(1280, 720),
        new Vector2Int(800, 600)
    };

    void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("ResolutionIndex", 0));
        Debug.Log(PlayerPrefs.GetInt("WindowModeIndex", 0));
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        int windowModeIndex = PlayerPrefs.GetInt("WindowModeIndex", 0);

        Vector2Int selectedResolution = resolutions[resolutionIndex];
        FullScreenMode screenMode = (windowModeIndex == 0) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution(selectedResolution.x, selectedResolution.y, screenMode);
        Debug.Log("Initial Settings Applied: Resolution - " + selectedResolution.x + " * " + selectedResolution.y + ", Window Mode - " + (windowModeIndex == 0 ? "Fullscreen" : "Windowed"));
    }
}
