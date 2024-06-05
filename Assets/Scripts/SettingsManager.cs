using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI mouseSenceText;
    [SerializeField] TextMeshProUGUI sfxText;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] TMP_Dropdown resolutionDropdpwn;

    Resolution[] resolutions;

    float currentRefreshRate;
    int currentResolutionIndex = 0;

    public static float mouseSense;
    public static float sfxVol;
    public static float musicVol;

    private void Start()
    {
        settingsMenu.SetActive(false);

        resolutions = Screen.resolutions;

        resolutionDropdpwn.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdpwn.AddOptions(options);
        resolutionDropdpwn.value = currentResolutionIndex;
        resolutionDropdpwn.RefreshShownValue();
    }

    private void Update()
    {
        mouseSense = mouseSlider.value * 100f;
        mouseSenceText.text = "Mouse sensitivity: " + (int)mouseSense;
        sfxVol = sfxSlider.value;
        sfxText.text = "SFX: " + (int)(sfxVol * 100);
        musicVol = musicSlider.value;
        musicText.text = "Music Volume: " + (int)(musicVol * 100);
    }

    public void SetResolution(int resolutionINdex) 
    { 
        Resolution resolution = resolutions[resolutionINdex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen) 
    { 
        Screen.fullScreen = isFullScreen;
    }

    public void ShowSettingMenu(bool doShow) 
    { 
        settingsMenu.SetActive(doShow);
    }

    public void SetResolution(int width, int height, bool isFullscreen)
    {
        Screen.SetResolution(width, height, isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }
}
