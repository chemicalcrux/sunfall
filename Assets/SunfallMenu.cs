using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class SunfallMenu : MonoBehaviour
{
    public enum CurrentMenu
    {
        Main,
        Settings
    }

    public GameStateHolder state;
    public CurrentMenu currentMenu;
    public CanvasGroup canvasGroup;
    public CinemachineVirtualCamera mainMenuCam;

    public TMP_Dropdown postProcessDropdown;
    public TMP_Dropdown dynamicResolutionDropdown;
    public Slider musicVolumeSlider;

    private float alphaVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Post Processing")) {
            PlayerPrefs.SetInt("Post Processing", 2);
        }
        if (!PlayerPrefs.HasKey("Dynamic Resolution")) {
            PlayerPrefs.SetInt("Dynamic Resolution", 0);
        }
        if (!PlayerPrefs.HasKey("Music Volume")) {
            PlayerPrefs.SetFloat("Music Volume", 0.5f);
        }

        postProcessDropdown.onValueChanged.AddListener((choice) => {
            PlayerPrefs.SetInt("Post Processing", choice);
        });
        dynamicResolutionDropdown.onValueChanged.AddListener((choice) => {
            PlayerPrefs.SetInt("Dynamic Resolution", choice);
        });
        musicVolumeSlider.onValueChanged.AddListener((value) => {
            PlayerPrefs.SetFloat("Music Volume", value);
        });

        postProcessDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("Post Processing"));
        dynamicResolutionDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("Dynamic Resolution"));
        musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Music Volume"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.up * (state.pivot.radius + 100);
        float targetAlpha = state.state == GameState.Menu ? 1f : 0f;
        canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, targetAlpha, ref alphaVelocity, 1.0f);

        mainMenuCam.enabled = false;

        if (state.state == GameState.Menu && currentMenu == CurrentMenu.Main)
            mainMenuCam.enabled = true;
    }

    public void StartGame()
    {
        state.pivot.StartGame();
        state.state = GameState.Playing;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public static float VFXFactor()
    {
        switch(PlayerPrefs.GetInt("Post Processing")) {
            case 0: return 0f;
            case 1: return 0.5f;
            case 2: return 1f;
            case 3: return 2f;
            default: return 0f;
        }
    }
}
