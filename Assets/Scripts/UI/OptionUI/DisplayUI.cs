using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DisplayUI : MonoBehaviour
{
    public TMP_Text resolutionText;
    public TMP_Dropdown resolutionDropdown;

    public Button leftResolutionButton;
    public Button rightResolutionButton;

    public TMP_Text windowModeText; // â��� �ؽ�Ʈ
    public TMP_Dropdown windowModeDropdown; // â��� ��Ӵٿ�

    public Button leftWindowModeButton; // â��� ���� ��ư
    public Button rightWindowModeButton; // â��� ������ ��ư

    private List<Vector2Int> resolutions = new List<Vector2Int>() {
        new Vector2Int(1920, 1080),
        new Vector2Int(1280, 720),
        new Vector2Int(800, 600)
    };
    private int currentResolutionIndex = 0;

    private List<string> windowModes = new List<string> { "��", "��" }; // â��� �ɼ� ����Ʈ
    private int currentWindowModeIndex = 0;

    void Start()
    {
        // ���� ����� ���� �ҷ�����
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        currentWindowModeIndex = PlayerPrefs.GetInt("WindowModeIndex", 0);

        resolutionDropdown.interactable = false;
        windowModeDropdown.interactable = false;

        resolutionDropdown.value = currentResolutionIndex;
        windowModeDropdown.value = currentWindowModeIndex;

        UpdateResolutionText();
        UpdateWindowModeText();
        ApplySettings();
    }

    void UpdateResolutionText()
    {
        resolutionText.text = resolutions[currentResolutionIndex].x + " * " + resolutions[currentResolutionIndex].y;
    }

    void UpdateWindowModeText()
    {
        windowModeText.text = windowModes[currentWindowModeIndex];
    }

    public void PreviousResolution()
    {
        currentResolutionIndex = (currentResolutionIndex > 0) ? currentResolutionIndex - 1 : resolutions.Count - 1;
        UpdateDropdownAndApply();
    }

    public void NextResolution()
    {
        currentResolutionIndex = (currentResolutionIndex < resolutions.Count - 1) ? currentResolutionIndex + 1 : 0;
        UpdateDropdownAndApply();
    }

    public void PreviousWindowMode()
    {
        currentWindowModeIndex = (currentWindowModeIndex > 0) ? currentWindowModeIndex - 1 : windowModes.Count - 1;
        UpdateDropdownAndApply();
    }

    public void NextWindowMode()
    {
        currentWindowModeIndex = (currentWindowModeIndex < windowModes.Count - 1) ? currentWindowModeIndex + 1 : 0;
        UpdateDropdownAndApply();
    }

    void UpdateDropdownAndApply()
    {
        resolutionDropdown.value = currentResolutionIndex;
        windowModeDropdown.value = currentWindowModeIndex;
        ApplySettings();
    }

    void ApplySettings()
    {
        UpdateResolutionText();
        UpdateWindowModeText();

        Vector2Int selectedResolution = resolutions[currentResolutionIndex];
        FullScreenMode screenMode = (currentWindowModeIndex == 0) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution(selectedResolution.x, selectedResolution.y, screenMode);

        // ���� ���� ����
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.SetInt("WindowModeIndex", currentWindowModeIndex);
        PlayerPrefs.Save();

        Debug.Log("Resolution: " + selectedResolution.x + " * " + selectedResolution.y + ", Window Mode: " + windowModes[currentWindowModeIndex]);
    }
}
